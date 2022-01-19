import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { LocationModel } from 'src/app/models/location-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationService } from 'src/app/services/location.service';
import { LoginService } from 'src/app/services/login.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'src/app/services/user.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.css']
})
export class ProfileSettingsComponent implements OnInit {
  public messageBox = '';
  public messageTitle = '';
  user: UserModel;
  userModify: UserModel;
  
  constructor(
    private loginService: LoginService, 
    private modalService: NgbModal,
    private locationService: LocationService,
    private userService: UserService,
    private authenticationService: AuthenticationService
    ) { this.authenticationService.currentUser.subscribe(
      (x) => (this.user = x)
    );}
  
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
  ChangeTrainerValue() {
      this.userModify.trainer = !this.userModify.trainer;
      this.userModify.role = "Trainer";
    
  }
  errorCheck() {
    this.messageTitle = 'Hiba';
    if (this.userModify.full_name == '') {
      this.messageBox = 'Kérem adja meg a nevét!';
    } else if (this.userModify.password == '') {
      this.messageBox = 'Kérem adjon meg egy jelszót!';
    } else if (this.userModify.phone_number == '') {
      this.messageBox = 'Kérem adja meg telefonszámát!';
    } else if (this.selectedCounty == null) {
      this.messageBox = 'Kérem válasszon megyét!';
    } else if (this.selectedCity == null) {
      this.messageBox = 'Kérem válasszon várost!';
    } else {
      this.messageTitle = 'Siker';
      this.messageBox = 'Sikeres módosítás!';
    }
  }
  save() {
    this.locationService.getLocationId(this.selectedCity).subscribe(
      result => {
        
          this.userModify.location_id = result[0].id; 
          console.log(this.userModify.location_id);
          this.userModify.id = this.user.id;
          this.errorCheck();
          this.userService.modifyUser(this.userModify).subscribe(
        (result) => {
          console.log(result);
          this.user = this.userModify;
          console.log(this.user.location_id);
          console.log(this.userModify.location_id);
        },
        (error) => console.log(error)
      )},
    error => console.log(error)
  )
    }
    ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    
    this.userModify = this.user;
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
