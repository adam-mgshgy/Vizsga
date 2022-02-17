import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationService } from 'src/app/services/location.service';
import { UserService } from 'src/app/services/user.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';
import { ImagesModel } from 'src/app/models/images-model';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.css'],
})
export class ProfileSettingsComponent implements OnInit {
  errorMessage = '';
  password2 = '';
  user: UserModel;
  userModify: UserModel;

  constructor(
    private locationService: LocationService,
    private userService: UserService,
    private authenticationService: AuthenticationService,
    private router: Router
  ) {
    this.authenticationService.currentUser.subscribe((x) => (this.user = x));
  }

  locations: LocationModel[] = [];
  counties: LocationModel[] = [];
  cities: LocationModel[] = [];
  selectedCounty: string;
  selectedCity: string;
  mobile: boolean = false;
  profileImage: ImagesModel = new ImagesModel();
  isImageSaved: boolean;
  cardImageBase64: string;
  image: string[] = [];
  selected = false;
  pwdChange = false;

  ngOnInit(): void {
    this.profileImage = null;
    this.userService.getUserById(this.user.id).subscribe(
      (result) => {
        this.user = result;
        if (this.user.imageId != 0) {
          this.userService.getImageById(this.user.imageId).subscribe(
            (result) => {
              this.profileImage = result;
            },
            (error) => console.log(error)
          );
        }
      },
      (error) => console.log(error)
    );
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.userModify = JSON.parse(JSON.stringify(this.user));
    this.userModify.password = '';
    this.locationService.getLocations().subscribe(
      (result) => {
        this.locations = result;
      },
      (error) => console.log(error)
    );

    this.locationService.getCounties().subscribe(
      (result) => {
        this.counties = result;
        this.selectedCounty =
          this.locations[this.user.location_id - 1].county_name;
        this.locationService.getCities(this.selectedCounty).subscribe(
          (result) => {
            this.cities = result;
            this.selectedCity =
              this.locations[this.user.location_id - 1].city_name;
          },
          (error) => console.log(error)
        );
      },
      (error) => console.log(error)
    );
  }

  fileChangeEvent(fileInput: any) {
    //TODO max image size, input text change
    if (fileInput.target.files && fileInput.target.files[0]) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const imgBase64Path = e.target.result;
        this.cardImageBase64 = imgBase64Path;
        this.isImageSaved = true;
        this.image.push(this.cardImageBase64);

        this.saveImage();
      };
      reader.readAsDataURL(fileInput.target.files[0]);
    }
  }
  saveImage() {
    console.log(this.image);
    this.userService.saveImage(this.image, this.user.id).subscribe(
      (result) => {console.log(result);
        this.userService.getUserById(this.user.id).subscribe(
          (result) => {
            this.user = result;
            if (this.user.imageId != 0) {
              this.userService.getImageById(this.user.imageId).subscribe(
                (result) => {
                  this.profileImage = result;
                },
                (error) => console.log(error)
              );
            }
          },
          (error) => console.log(error)
        );
      },
      (error) => console.log(error)
    );
  }
  deleteImage() {
    if (this.user.imageId != 0) {
      console.log(this.profileImage);
      this.userService.deleteImage(this.profileImage.id).subscribe(
        (result) => {
          console.log(result),
            this.userService.getUserById(this.user.id).subscribe(
              (result) => (this.user = result),
              (error) => console.log(error)
            );
          this.profileImage = null;
          this.selected = false;
        },
        (error) => console.log(error)
      );
    } 
  }
  CountyChanged() {
    this.locationService.getCities(this.selectedCounty).subscribe(
      (result) => (this.cities = result),
      (error) => console.log(error)
    );
  }
  ChangeTrainerValue() {
    if (this.userModify.role == 'Trainer') {
      this.userModify.role = 'User';
      this.user.role = 'User';
    } else {
      this.userModify.role = 'Trainer';
      this.user.role = 'Trainer';
    }
  }
  errorCheck(): boolean {
    if (this.userModify.full_name == '') {
      this.errorMessage = 'Kérem adja meg a nevét!';
      return false;
    }
    if (this.pwdChange && (this.userModify.password == '' || this.password2 == '')) {
      this.errorMessage = 'Kérem adjon meg jelszót!';
      return false;
    }
    if (this.userModify.password != this.password2) {
      this.errorMessage = 'Nem egyezik a két jelszó!';
      return false;
    }
    if (this.userModify.phone_number == '') {
      this.errorMessage = 'Kérem adja meg telefonszámát!';
      return false;
    }
    if (this.selectedCounty == null) {
      this.errorMessage = 'Kérem válasszon megyét!';
      return false;
    }
    if (this.selectedCity == null) {
      this.errorMessage = 'Kérem válasszon várost!';
      return false;
    }
    return true;
  }
  save() {
    if (this.errorCheck()) {
      if (!this.pwdChange) {
        this.userModify.password = "";
      }
      else {
        this.userModify.password = this.password2;
      }
      this.locationService.getLocationId(this.selectedCity).subscribe(
        (result) => {
          this.userModify.location_id = result[0].id;
          this.userModify.id = this.user.id;
          this.userService.modifyUser(this.userModify).subscribe(
            (result) => {
              this.user = this.userModify;
              this.router.navigateByUrl('/home');
            },
            (error) => (this.errorMessage = error)
          );
        },
        (error) => (this.errorMessage = error)
      );
    }
  }
  deleteUser() {
    if (confirm('Biztosan törölni szeretné a fiókját?')) {
      this.userService.deleteUser(this.user).subscribe(
        (result) => {
          console.log(result);
          this.user = new UserModel();
          this.authenticationService.logout();
          this.cancel();
        },
        (error) => console.log(error)
      );
    }
  }
  cancel() {
    this.userModify = null;
    this.router.navigateByUrl('/home');
  }
}
