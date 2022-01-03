import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { LocationModel } from 'src/app/models/location-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationService } from 'src/app/services/location.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.css']
})
export class ProfileSettingsComponent implements OnInit {

  user: UserModel;
  subscription: Subscription;
  constructor(private loginService: LoginService, private locationService: LocationService) { }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  public locations: LocationModel[] = [];
  counties: LocationModel[] = [];
  cities: LocationModel[] = [];
  selectedCounty: string;
  selectedCity: string;
  mobile: boolean = false;
  CountyChanged() {
    this.locationService.getCities(this.selectedCounty).subscribe(
      result => this.cities = result,
      error => console.log(error)
    );
  }
  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    this.subscription = this.loginService.currentUser.subscribe(user => this.user = user)
    this.locationService.getLocations().subscribe(
      result => {this.locations = result; },
      error => console.log(error)
    );
    this.locationService.getCounties().subscribe(
      result => {
        this.counties = result;
        this.selectedCounty = this.locations[this.user.location_id- 1].county_name;
        this.locationService.getCities(this.selectedCounty).subscribe(
          result => {
            this.cities = result;
            this.selectedCity = this.locations[this.user.location_id-1].city_name;},
          error => console.log(error)
        );
      },
      error => console.log(error)
    );
   
    
    
  }

}
