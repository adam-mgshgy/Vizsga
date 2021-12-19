import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { LocationService } from 'src/app/services/location.service';

@Component({
  selector: 'app-registry-page',
  templateUrl: './registry-page.component.html',
  styleUrls: ['./registry-page.component.css']
})
export class RegistryPageComponent implements OnInit {

  mobile: boolean = false;
  locations: LocationModel[] = [];
  counties: LocationModel[] = [];
  cities: LocationModel[] = [];
  selectedCounty: '';
  constructor(private locationService: LocationService) {}
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
    this.locationService.getLocations().subscribe(
      result => this.locations = result,
      error => console.log(error)
    );
    this.locationService.getCounties().subscribe(
      result => this.counties = result,
      error => console.log(error)
    );
    
  }
  
}
