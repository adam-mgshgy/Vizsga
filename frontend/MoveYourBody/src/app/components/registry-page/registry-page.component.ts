import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationService } from 'src/app/services/location.service';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registry-page',
  templateUrl: './registry-page.component.html',
  styleUrls: ['./registry-page.component.css'],
})
export class RegistryPageComponent implements OnInit {
  mobile: boolean = false;
  locations: LocationModel[] = [];
  counties: LocationModel[] = [];
  cities: LocationModel[] = [];
  selectedCounty: string;
  selectedCity: string;
  newUser: UserModel = new UserModel();
  errorMessage = '';
  password2 = '';

  constructor(
    private locationService: LocationService,
    private userService: UserService,
    private router: Router
  ) {}

  TrainerChecked(event: any) {
    if (event.target.checked) {
      this.newUser.role = 'Trainer';
    } else {
      this.newUser.role = 'User';
    }
  }

  CountyChanged(value) {
    for (const item of this.counties) {
      if (item.county_name == value) {
        this.newUser.location_id = item.id;
        this.selectedCounty = item.county_name;
      } else if (value == '') {
        this.newUser.location_id = null;
      } else if (value == item.id) {
        this.selectedCounty = item.county_name;
      }
    }
    this.locationService.getCities(this.selectedCounty).subscribe(
      (result) => (this.cities = result),
      (error) => console.log(error)
    );
  }
  CityChanged(value) {
    for (const item of this.cities) {
      if (item.city_name == value) {
        this.newUser.location_id = item.id;
        this.selectedCity = item.city_name;
      } else if (value == '') {
        this.newUser.location_id = null;
      } else if (value == item.id) {
        this.selectedCity = item.city_name;
      }
    }
  }
  Register() {
    if (!this.newUser.role) this.newUser.role = 'User';
    if (this.errorCheck()) {
      this.userService.emailExists(this.newUser.email).subscribe((result) => {
        if (result == true) {
          this.errorMessage =
            'Ezzel az E-mail címmel már létezik felhasználói fiók';
        } else {
          this.locationService.getLocationId(this.selectedCity).subscribe(
            (result) => (this.newUser.location_id = result),
            (error) => console.log(error)
          );
          this.newUser.id = 0;
          this.newUser.image_id = 0;
          this.userService.Register(this.newUser).subscribe(
            (result) => {
              console.log(this.newUser);
              this.router.navigateByUrl('/login');
            },
            (error) => (this.errorMessage = 'Váratlan hiba történt!')
          );
        }
      });
    }
  }
  Cancel() {
    this.newUser = new UserModel();
    this.selectedCounty = '';
    this.selectedCity = '';
    this.password2 = '';
  }
  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    this.locationService.getLocations().subscribe(
      (result) => (this.locations = result),
      (error) => console.log(error)
    );
    this.locationService.getCounties().subscribe(
      (result) => (this.counties = result),
      (error) => console.log(error)
    );
  }

  errorCheck(): boolean {
    if (this.newUser.full_name == '') {
      this.errorMessage = 'Kérem adja meg a nevét!';
      return false;
    }
    if (this.newUser.email == '') {
      this.errorMessage = 'Kérem adja meg az e-mail címét!';
      return false;
    }
    if (this.newUser.password == '') {
      this.errorMessage = 'Kérem adjon meg egy jelszót!';
      return false;
    }
    if (this.newUser.password != this.password2) {
      this.errorMessage = 'Nem egyezik a két jelszó!';
      return false;
    }
    if (this.newUser.phone_number == '') {
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
}
