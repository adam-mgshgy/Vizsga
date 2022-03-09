import { Component, OnInit } from '@angular/core';
import { LocationModel } from 'src/app/models/location-model';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { LocationService } from 'src/app/services/location.service';
import { TrainingSessionService } from 'src/app/services/training-session.service';
import { Time } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingService } from 'src/app/services/training.service';
import { TrainingModel } from 'src/app/models/training-model';

@Component({
  selector: 'app-add-session-page',
  templateUrl: './add-session-page.component.html',
  styleUrls: ['./add-session-page.component.css'],
})
export class AddSessionPageComponent implements OnInit {
  training_id: number;
  sessionId: number;

  mobile: boolean = false;
  counties: LocationModel[] = [];
  cities: LocationModel[] = [];
  selectedCounty: string;
  selectedCity: string;
  newSession: TrainingSessionModel = new TrainingSessionModel();
  training: TrainingModel = new TrainingModel();
  date: Date;
  time: Time;

  errorMessage = '';
  constructor(
    private locationService: LocationService,
    private trainingService: TrainingService,
    private trainingSessionService: TrainingSessionService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  TimeChanged() {
    this.newSession.date = new Date(this.date + ' ' + this.time).toISOString();
  }
  CountyChanged(value) {
    for (const item of this.counties) {
      if (item.county_name == value) {
        this.newSession.location_id = item.id;
        this.selectedCounty = item.county_name;
      } else if (value == '') {
        this.newSession.location_id = null;
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
        this.newSession.location_id = item.id;
        this.selectedCity = item.city_name;
      } else if (value == '') {
        this.newSession.location_id = null;
      } else if (value == item.id) {
        this.selectedCity = item.city_name;
      }
    }
  }
  SaveSession() {
    if (this.errorCheck()) {
      this.locationService.getLocationId(this.selectedCity).subscribe(
        (result) => {
          this.newSession.location_id = result[0].id;
          this.newSession.id = 1;
          this.newSession.training_id = this.training_id;
          this.newSession.min_member = Number(this.newSession.min_member);
          this.newSession.max_member = Number(this.newSession.max_member);
          this.trainingSessionService
            .createTrainingSession(this.newSession)
            .subscribe(
              (result) => {
                this.errorMessage = 'Sikeres mentés!';
                this.router.navigateByUrl('/mytrainings/trainer');
              },
              (error) => console.log(error)
            );
        },
        (error) => console.log(error)
      );
    }
  }
  Cancel() {
    this.newSession = new TrainingSessionModel();
    this.selectedCity = '';
    this.selectedCounty = '';
    this.date = null;
    this.time = null;
    this.router.navigateByUrl('/mytrainings/trainer');
  }
  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    this.locationService.getCounties().subscribe(
      (result) => {
        this.counties = result;
      },
      (error) => console.log(error)
    );

    this.route.paramMap.subscribe((params) => {
      this.training_id = Number(params.get('training_id'));
      this.sessionId = Number(params.get('sessionId'));
      if (this.sessionId) {
        this.trainingSessionService.getById(this.sessionId).subscribe(
          (result) => {
            this.newSession = result.session;
            this.newSession.date = '';
            this.selectedCounty = result.location.county_name;
            this.CountyChanged(this.selectedCounty);
            this.selectedCity = result.location.city_name;
            this.training = result.training;
          },
          (error) => console.log(error)
        );
      } else {
        this.newSession.id = 1;
        this.trainingService.listBytraining_id(this.training_id).subscribe(
          (result) => {
            this.selectedCounty = result.location.county_name;
            this.CountyChanged(this.selectedCounty);
            this.selectedCity = result.location.city_name;
            this.training = result.training;
          },
          (error) => console.log(error)
        );
      }
    });
  }
  errorCheck(): boolean {
    if (this.newSession.date == '') {
      this.errorMessage = 'Kérem adjon meg dátumot!';
      return false;
    }
    if (this.newSession.minutes == null) {
      this.errorMessage = 'Kérem adja meg az alkalom hosszát percben!';
      return false;
    }
    if (this.newSession.price == null) {
      this.errorMessage = 'Kérem adja meg az alkalom árát!';
      return false;
    }
    if (this.newSession.price < 0 || this.newSession.minutes < 0) {
      this.errorMessage = 'Az érték nem lehet negatív!';
      return false;
    }
    if (this.newSession.max_member == 0) {
      this.errorMessage =
        'Kérem adja meg az edzés résztvevőinek maximum számát!';
      return false;
    }
    if (this.newSession.min_member == 0) {
      this.errorMessage =
        'Kérem adja meg az edzés résztvevőinek minimum számát!';
      return false;
    }
    if (
      Number(this.newSession.min_member) > Number(this.newSession.max_member)
    ) {
      this.errorMessage =
        'Az edzéshez tartozó minimum résztvevők száma nagyobb, mint a maximum!';
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
    if (this.newSession.address_name == '') {
      this.errorMessage = 'Kérem adja meg az alkalom címét!';
      return false;
    }
    if (this.newSession.place_name == '') {
      this.errorMessage = 'Kérem adja meg a létesítmény nevét!';
      return false;
    }
    return true;
  }
}
