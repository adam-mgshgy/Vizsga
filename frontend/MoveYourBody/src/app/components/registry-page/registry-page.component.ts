import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocationService } from 'src/app/services/location.service';
import { UserService } from 'src/app/services/user.service';
import { LocationModel } from 'src/app/models/location-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-registry-page',
  templateUrl: './registry-page.component.html',
  styleUrls: ['./registry-page.component.css'],
})
export class RegistryPageComponent implements OnInit {
  constructor(
    private locationService: LocationService,
    private userService: UserService,
    private router: Router
  ) {}
  mobile: boolean = false;
  locations: LocationModel[] = [];
  counties: LocationModel[] = [];
  cities: LocationModel[] = [];
  selectedCounty: string;
  selectedCity: string;
  newUser: UserModel = new UserModel();
  errorMessage = '';
  password2 = '';

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
  Cancel() {
    this.newUser = new UserModel();
    this.selectedCounty = '';
    this.selectedCity = '';
    this.password2 = '';
  }
  errorCheck(): boolean {
    if (this.newUser.full_name == '') {
      this.errorMessage = 'K??rem adja meg a nev??t!';
      return false;
    }
    if (this.newUser.email == '') {
      this.errorMessage = 'K??rem adja meg az e-mail c??m??t!';
      return false;
    }
    if (this.newUser.password == '') {
      this.errorMessage = 'K??rem adjon meg egy jelsz??t!';
      return false;
    }
    if (this.newUser.password != this.password2) {
      this.errorMessage = 'Nem egyezik a k??t jelsz??!';
      return false;
    }
    if (this.newUser.phone_number == '') {
      this.errorMessage = 'K??rem adja meg telefonsz??m??t!';
      return false;
    }
    if (this.selectedCounty == null) {
      this.errorMessage = 'K??rem v??lasszon megy??t!';
      return false;
    }
    if (this.selectedCity == null) {
      this.errorMessage = 'K??rem v??lasszon v??rost!';
      return false;
    }

    return true;
  }
  Register() {
    if (!this.newUser.role) this.newUser.role = 'User';
    if (this.errorCheck()) {
      this.userService.emailExists(this.newUser.email).subscribe((result) => {
        if (result == true) {
          this.errorMessage =
            'Ezzel az E-mail c??mmel m??r l??tezik felhaszn??l??i fi??k';
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
            (error) => (this.errorMessage = 'V??ratlan hiba t??rt??nt!')
          );
        }
      });
    }
  }
  
}
