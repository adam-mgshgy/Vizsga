import { Component, OnInit } from '@angular/core';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationModel } from 'src/app/models/location-model';
import { ActivatedRoute } from '@angular/router';
import { CategoryModel } from 'src/app/models/category-model';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { TrainingSessionService } from 'src/app/services/training-session.service';
import { LocationService } from 'src/app/services/location.service';
import { ApplicantModel } from 'src/app/models/applicant-model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ApplicantService } from 'src/app/services/applicant.service';
import {Sort} from '@angular/material/sort';
@Component({
  selector: 'app-training-page',
  templateUrl: './training-page.component.html',
  styleUrls: ['./training-page.component.css'],
})
export class TrainingPageComponent implements OnInit {
  id: Number;
  user: UserModel = {
    email: '',
    full_name: '',
    id: 0,
    location_id: 0,
    password: '',
    phone_number: '',
    role: 'User',
    token: '',
  };

  errorMessage = '';
  training: TrainingModel = {
    category_id: 0,
    contact_phone: '',
    description: '',
    id: 0,
    name: '',
    trainer_id: 0,
  };
  trainerName: '';
  trainer: UserModel = {
    email: '',
    full_name: '',
    id: 0,
    location_id: 0,
    password: '',
    phone_number: '',
    role: 'User',
    token: '',
  };
  category: CategoryModel = {
    id: 0,
    img_src: '',
    name: ''
  };
  sessions: TrainingSessionModel[] = [];
  sortedSessions: TrainingSessionModel[]= [];
  locations: LocationModel[];
  tagTraining: TagTrainingModel[] = [];
  tags: TagModel[] = [];
  // sort: Sort;

  usersSessions: ApplicantModel[] = [];
  mobile: boolean = false;
  constructor(
    private trainingSessionService: TrainingSessionService,
    private route: ActivatedRoute,
    private locationService: LocationService,
    private applicantService: ApplicantService,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe((x) => (this.user = x));
  }
  sortData(sort: Sort) {
    const data = this.sessions.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedSessions = data;
      return;
    }
    this.sortedSessions = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'date':
          return this.compare(a.date, b.date, isAsc);
        case 'price':
          return this.compare(a.price, b.price, isAsc);
        case 'minutes':
          return this.compare(a.minutes, b.minutes, isAsc);
        default:
          return 0;
      }
    });
  }
  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
  
  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.route.paramMap.subscribe((params) => {
      this.id = Number(params.get('id'));
    });
    this.applicantService.listByUserId(this.user.id).subscribe(
      (result) => {
        this.usersSessions = result;
      },
      (error) => console.log(error)
    );
    this.trainingSessionService.listByTrainingId(this.id).subscribe(
      (result) => {
        this.sessions = result.sessions;
        this.sortedSessions = this.sessions.slice();
        this.training = result.training;
        this.trainerName = result.trainer;
        this.category = result.category;
        this.tags = result.tags;
      },
      (error) => console.log(error)
    );
    this.locationService.getLocations().subscribe(
      (result) => {
        this.locations = result;
      },
      (error) => console.log(error)
    );
  }

  Apply(sessionId: number) {
    var newApplicant = new ApplicantModel();
    newApplicant.user_id = this.user.id;
    newApplicant.id = 1;
    if (this.usersSessions.find((x) => x.training_session_id == sessionId) == undefined) {
      newApplicant.training_session_id = sessionId;
      this.applicantService.newApplicant(newApplicant).subscribe(
        (result) => {
          this.usersSessions.push(newApplicant);
        },
        (error) => {
          this.errorMessage = error.message;
        }
      );
    } else {
      this.errorMessage = 'Erre az edzésre már jelentkezett!';
    }
  }
}
