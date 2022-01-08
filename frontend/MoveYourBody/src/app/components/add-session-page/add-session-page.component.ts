import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { LocationService } from 'src/app/services/location.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TrainingSessionService } from 'src/app/services/training-session.service';
import { Time } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-session-page',
  templateUrl: './add-session-page.component.html',
  styleUrls: ['./add-session-page.component.css']
})
export class AddSessionPageComponent implements OnInit {
  trainingId: number;

  mobile: boolean = false;
  locations: LocationModel[] = [];
  counties: LocationModel[] = [];
  cities: LocationModel[] = [];
  selectedCounty: string;
  selectedCity: string;
  newSession: TrainingSessionModel = new TrainingSessionModel();
  date: Date;
  time: Time;
  
  public messageBox = '';
  public messageTitle = '';
  constructor(
    private locationService: LocationService,
    private modalService: NgbModal,
    private trainingSessionService: TrainingSessionService,
    private route: ActivatedRoute,
    private router: Router

  ) { }
  TimeChanged(){
    this.newSession.date = new Date(this.date + ' ' + this.time).toISOString();
    //console.log("iso:" +this.newSession.date.toISOString());
  }
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
    this.errorCheck();
    this.locationService.getLocationId(this.selectedCity).subscribe(
      result => this.newSession.location_id = result,
      error => console.log(error)
    )
    this.newSession.id = 0;
    this.newSession.training_id = this.trainingId;
    this.errorCheck();
    this.trainingSessionService.createTrainingSession(this.newSession).subscribe(
      (result) => {
        console.log(this.newSession)
      },
      (error) => console.log(error)

    )
  }
  Cancel() {
    this.newSession = new TrainingSessionModel();
    this.selectedCity = '';
    this.selectedCounty = '';
    this.date = null;
    this.time = null;
    this.router.navigateByUrl('/mytrainings');
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
    this.route.paramMap.subscribe((params) => {
      this.trainingId = Number(params.get('id'));
      console.log(this.trainingId);
    }
    );
  }
  errorCheck() {
    this.messageTitle = 'Hiba';
    if (this.newSession.date == null) {
      this.messageBox = 'Kérem adjon meg dátumot!'; //TODO hogyan ellenőrizzem, hogy múltbeli-e?
    } else if (this.newSession.minutes == null) {
      this.messageBox = 'Kérem adja meg az alkalom hosszát percben!'; 
    } else if (this.newSession.price == null) {
      this.messageBox = 'Kérem adja meg az alkalom árát!';
    }else if (this.newSession.price  < 0 || this.newSession.minutes < 0) {
      this.messageBox = 'Az érték nem lehet negatív';
    } else if (this.selectedCounty == null) {
      this.messageBox = 'Kérem válasszon megyét!';
    } else if (this.selectedCity == null) {
      this.messageBox = 'Kérem válasszon várost!';
    } else if (this.newSession.address_name == '') {
      this.messageBox = 'Kérem adja meg az alkalom címét!';
    } else if (this.newSession.place_name == '') {
      this.messageBox = 'Kérem adja meg a létesítmény nevét!';
    }
     else {
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
