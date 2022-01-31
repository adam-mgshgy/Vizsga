import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationService } from 'src/app/services/location.service';
import { UserService } from 'src/app/services/user.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';

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
  CountyChanged() {
    this.locationService.getCities(this.selectedCounty).subscribe(
      (result) => (this.cities = result),
      (error) => console.log(error)
    );
  }
  ChangeTrainerValue() {
    this.userModify.trainer = !this.userModify.trainer;
    this.userModify.role = 'Trainer';
  }
  errorCheck(): boolean {
    if (this.userModify.full_name == '') {
      this.errorMessage = 'Kérem adja meg a nevét!';
      return false;
    }
    if (this.userModify.password == '' || this.password2 == '') {
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
      this.locationService.getLocationId(this.selectedCity).subscribe(
        (result) => {
          this.userModify.location_id = result[0].id;
          console.log(this.userModify.location_id);
          this.userModify.id = this.user.id;
          this.userService.modifyUser(this.userModify).subscribe(
            (result) => {
              this.user = this.userModify;
              this.router.navigateByUrl('/home');
            },
            (error) => this.errorMessage = error
          );
        },
        (error) => this.errorMessage = error
      );
    }
  }
  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.userModify = this.user;
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
}
