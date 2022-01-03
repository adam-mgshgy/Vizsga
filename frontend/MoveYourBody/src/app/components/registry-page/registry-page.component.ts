import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationService } from 'src/app/services/location.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { resourceUsage } from 'process';
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
  selectedCounty: '';
  selectedCity: '';
  isTrainer: boolean;
  newUser: UserModel = new UserModel();
  public messageBox = '';
  public messageTitle = '';
  constructor(
    private locationService: LocationService,
    private modalService: NgbModal,
    private userService: UserService
    ) {}
  CountyChanged() {
    this.locationService.getCities(this.selectedCounty).subscribe(
      result => this.cities = result,
      error => console.log(error)
    );
  }
  Register() {
    this.locationService.getLocationId(this.selectedCounty, this.selectedCity).subscribe(
      result => this.newUser.location_id = result,
      error => console.log(error)
    )
    this.newUser.trainer = this.isTrainer;
    this.errorCheck();
    this.userService.Register(this.newUser).subscribe(
      (result) => {
        this.newUser.id = result.id;
        this.newUser.email = result.email;
        this.newUser.full_name = result.full_name;
        this.newUser.location_id = result.location_id;
        this.newUser.password = result.password;
        this.newUser.phone_number = result.phone_number;
        this.newUser.trainer = result.trainer;
      },
      (error) => console.log(error)
    )
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
    } else if (this.newUser.location_id == null) {
      this.messageBox = 'Kérem válasszon megyét és várost!';
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
