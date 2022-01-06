import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationService } from 'src/app/services/location.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
//import { resourceUsage } from 'process';
import { UserService } from 'src/app/services/user.service';

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
  selectedCounty: string;
  selectedCity: string;
  newUser: UserModel = new UserModel();
  public messageBox = '';
  public messageTitle = '';
  constructor(
    private locationService: LocationService,
    private modalService: NgbModal,
    private userService: UserService
    ) {}

  TrainerChecked(event: any) {
    this.newUser.trainer = event.target.checked;
  }

  CountyChanged(value) {
    for (const item of this.counties) {
      if (item.county_name == value) {
        this.newUser.location_id = item.id;
        this.selectedCounty = item.county_name;
        console.log(this.selectedCounty);
      } else if (value == '') {
        this.newUser.location_id = null;
      } else if (value == item.id) {
        this.selectedCounty = item.county_name;
      }
    }
    this.locationService.getCities(this.selectedCounty).subscribe(
      result => this.cities = result,
      error => console.log(error)
    );
  }
  CityChanged(value) {
    for (const item of this.cities) {
      if (item.city_name == value) {
        this.newUser.location_id = item.id;
        this.selectedCity = item.city_name;
        console.log(this.selectedCity);
      } else if (value == '') {
        this.newUser.location_id = null;
      } else if (value == item.id) {
        this.selectedCity = item.city_name;
      }
    }
  }
  Register() {
    this.locationService.getLocationId(this.selectedCity).subscribe(
      result => this.newUser.location_id = result,
      error => console.log(error)
    )
    this.newUser.id = 0;
    this.errorCheck();
    this.userService.Register(this.newUser).subscribe(
      (result) => {
        console.log(this.newUser)
      },
      (error) => console.log(error)
    )
  }
  Cancel() {
    this.newUser = new UserModel;
    this.selectedCounty = "";
    this.selectedCity = "";
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

  errorCheck() {
    this.messageTitle = 'Hiba';
    if (this.newUser.full_name == '') {
      this.messageBox = 'Kérem adja meg a nevét!';
    } else if (this.newUser.email == '') {
      this.messageBox = 'Kérem adja meg az email-címét!';
    } else if (this.newUser.password == '') {
      this.messageBox = 'Kérem adjon meg egy jelszót!';
    } else if (this.newUser.phone_number == '') {
      this.messageBox = 'Kérem adja meg telefonszámát!';
    } else if (this.selectedCounty == null) {
      this.messageBox = 'Kérem válasszon megyét!';
    } else if (this.selectedCity == null) {
      this.messageBox = 'Kérem válasszon várost!';
    } else {
      this.messageTitle = 'Siker';
      this.messageBox = 'Sikeres regisztráció!';
    }
  }
  closeResult = '';
  open(content: any) {
    this.modalService.open(content).result.then(
      (result) => {
        this.closeResult = `Closed with: ${result}`;
      },
      (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }
  close() {
    this.modalService.dismissAll();
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  
}
