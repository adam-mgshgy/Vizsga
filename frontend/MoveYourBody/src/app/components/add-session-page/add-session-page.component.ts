import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { LocationService } from 'src/app/services/location.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-session-page',
  templateUrl: './add-session-page.component.html',
  styleUrls: ['./add-session-page.component.css']
})
export class AddSessionPageComponent implements OnInit {
  mobile: boolean = false;
  locations: LocationModel[] = [];
  counties: LocationModel[] = [];
  cities: LocationModel[] = [];
  selectedCounty: string;
  selectedCity: string;
  newSession: TrainingSessionModel = new TrainingSessionModel();
  
  public messageBox = '';
  public messageTitle = '';
  constructor(
    private locationService: LocationService,
    private modalService: NgbModal,
  ) { }
  CountyChanged(value) {
    for (const item of this.counties) {
      if (item.county_name == value) {
        this.newSession.location_id = item.id;
        this.selectedCounty = item.county_name;
        console.log(this.selectedCounty);
      } else if (value == '') {
        this.newSession.location_id = null;
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
        this.newSession.location_id = item.id;
        this.selectedCity = item.city_name;
        console.log(this.selectedCity);
      } else if (value == '') {
        this.newSession.location_id = null;
      } else if (value == item.id) {
        this.selectedCity = item.city_name;
      }
    }
  }
  SaveSession() {

  }
  Cancel() {
    
  }
  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    this.locationService.getCounties().subscribe(
      result => this.counties = result,
      error => console.log(error)
    );
  }
  errorCheck() {
    this.messageTitle = 'Hiba';
    if (this.newSession.date == '') {
      this.messageBox = 'Kérem adjon meg dátumot!';
    } else if (this.newSession.minutes == null) {
      this.messageBox = 'Kérem adja meg az alkalom hosszát percben!';
    } else if (this.newSession.price == null) {
      this.messageBox = 'Kérem adja meg az alkalom árát!';
    } else if (this.selectedCounty == null) {
      this.messageBox = 'Kérem válasszon megyét!';
    } else if (this.selectedCity == null) {
      this.messageBox = 'Kérem válasszon várost!';
    } else {
      this.messageTitle = 'Siker';
      this.messageBox = 'Sikeres mentés!';
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
