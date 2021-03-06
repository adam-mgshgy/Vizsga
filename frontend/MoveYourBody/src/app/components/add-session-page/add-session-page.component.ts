import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Time } from '@angular/common';
import { LocationService } from 'src/app/services/location.service';
import { TrainingService } from 'src/app/services/training.service';
import { TrainingSessionService } from 'src/app/services/training-session.service';
import { LocationModel } from 'src/app/models/location-model';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { TrainingModel } from 'src/app/models/training-model';

@Component({
  selector: 'app-add-session-page',
  templateUrl: './add-session-page.component.html',
  styleUrls: ['./add-session-page.component.css'],
})
export class AddSessionPageComponent implements OnInit {
  constructor(
    private locationService: LocationService,
    private trainingService: TrainingService,
    private trainingSessionService: TrainingSessionService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

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
 
  Cancel() {
    this.newSession = new TrainingSessionModel();
    this.selectedCity = '';
    this.selectedCounty = '';
    this.date = null;
    this.time = null;
    this.router.navigateByUrl('/mytrainings/trainer');
  }
 
  errorCheck(): boolean {
    if (this.newSession.date == '') {
      this.errorMessage = 'K??rem adjon meg d??tumot!';
      return false;
    }
    if (this.newSession.minutes == null) {
      this.errorMessage = 'K??rem adja meg az alkalom hossz??t percben!';
      return false;
    }
    if (this.newSession.price == null) {
      this.errorMessage = 'K??rem adja meg az alkalom ??r??t!';
      return false;
    }
    if (this.newSession.price < 0 || this.newSession.minutes < 0) {
      this.errorMessage = 'Az ??rt??k nem lehet negat??v!';
      return false;
    }
    if (this.newSession.max_member == 0) {
      this.errorMessage =
        'K??rem adja meg az edz??s r??sztvev??inek maximum sz??m??t!';
      return false;
    }
    if (this.newSession.min_member == 0) {
      this.errorMessage =
        'K??rem adja meg az edz??s r??sztvev??inek minimum sz??m??t!';
      return false;
    }
    if (
      Number(this.newSession.min_member) > Number(this.newSession.max_member)
    ) {
      this.errorMessage =
        'Az edz??shez tartoz?? minimum r??sztvev??k sz??ma nagyobb, mint a maximum!';
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
    if (this.newSession.address_name == '') {
      this.errorMessage = 'K??rem adja meg az alkalom c??m??t!';
      return false;
    }
    if (this.newSession.place_name == '') {
      this.errorMessage = 'K??rem adja meg a l??tes??tm??ny nev??t!';
      return false;
    }
    return true;
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
                this.errorMessage = 'Sikeres ment??s!';
                this.router.navigateByUrl('/mytrainings/trainer');
              },
              (error) => console.log(error)
            );
        },
        (error) => console.log(error)
      );
    }
  }
}
