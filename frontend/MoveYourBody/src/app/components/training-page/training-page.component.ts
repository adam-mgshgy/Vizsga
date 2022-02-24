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
import { ImagesModel } from 'src/app/models/images-model';
import { UserService } from 'src/app/services/user.service';
import { TrainingService } from 'src/app/services/training.service';
@Component({
  selector: 'app-training-page',
  templateUrl: './training-page.component.html',
  styleUrls: ['./training-page.component.css'],
})
export class TrainingPageComponent implements OnInit {
  profileImage: ImagesModel = new ImagesModel();
  Images: ImagesModel[] = [];
  defaultProfile = './assets/images/defaultImages/defaultProfilePicture.png';
  defaultTraining = './assets/images/mainPageImages/logo.png';
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
    imageId: 0
  };

  errorMessage = '';
  successMessage = '';
  training: TrainingModel = {
    category_id: 0,
    contact_phone: '',
    description: '',
    id: 0,
    name: '',
    trainer_id: 0,
    indexImageId: 0
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
    imageId: 0
  };
  category: CategoryModel = {
    id: 0,
    imgId: 1,
    name: ''
  };
  sessions: TrainingSessionModel[] = [];
  sortedSessions: TrainingSessionModel[]= [];
  locations: LocationModel[];
  tagTraining: TagTrainingModel[] = [];
  tags: TagModel[] = [];
  currentDate = new Date();

  usersSessions: ApplicantModel[] = [];
  mobile: boolean = false;
  constructor(
    private trainingSessionService: TrainingSessionService,
    private route: ActivatedRoute,
    private locationService: LocationService,
    private applicantService: ApplicantService,
    private authenticationService: AuthenticationService,
    private trainingService: TrainingService
  ) {
    this.authenticationService.currentUser.subscribe((x) => (this.user = x));
  }
  sortData(sort: Sort) {
    let dataTemp = [];
    this.sessions.forEach(session => {
      var year = Number(session.date.substring(0, 4));
      var month = Number(session.date.substring(5, 7));
      var day = Number(session.date.substring(8, 10));
      var hour = Number(session.date.substring(11, 13));
      var minute = Number(session.date.substring(14, 16));
      var sessionDate = new Date(year, month - 1, day, hour, minute);
      if (sessionDate > this.currentDate) {
        dataTemp.push(session);
      }

    });

    const data = dataTemp;
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
        this.training = result.training;
        this.trainerName = result.trainer;
        this.category = result.category;
        this.tags = result.tags;
        this.profileImage = result.image
        this.trainingService.getImageById(this.training.id).subscribe(
          (result) => {
            for (const item of result.images) {
              this.Images.push(item);
            }
          },
          (error) => console.log(error)
        );

        this.sessions.forEach(session => {
          var year = Number(session.date.substring(0, 4));
          var month = Number(session.date.substring(5, 7));
          var day = Number(session.date.substring(8, 10));
          var hour = Number(session.date.substring(11, 13));
          var minute = Number(session.date.substring(14, 16));
          var sessionDate = new Date(year, month - 1, day, hour, minute);
          if (sessionDate < this.currentDate) {
            session.isPast = true;
          }
          else {
            session.isPast = false;
            this.sortedSessions.push(session)
          }
        });
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
          var idx = this.sessions.findIndex((s) => s.id == sessionId);
          this.sessions[idx].number_of_applicants++;
          // idx = this.sortedSessions.findIndex((s) => s.id == sessionId);
          // this.sortedSessions[idx].number_of_applicants++;
          this.errorMessage = '';
          this.successMessage = 'Sikeres jelentkezés!';
        },
        (error) => {
          this.errorMessage = error.message;
        }
      );
    } else {
      this.errorMessage = 'Erre az edzésre már jelentkezett!';
      this.successMessage = '';
    }
  }
}
