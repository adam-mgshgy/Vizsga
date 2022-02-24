import { Component, OnInit } from '@angular/core';
import { ResizedEvent } from 'angular-resize-event';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { TrainingService } from 'src/app/services/training.service';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { TagService } from 'src/app/services/tag.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TrainingSessionService } from 'src/app/services/training-session.service';
import { ApplicantService } from 'src/app/services/applicant.service';
import { ImagesModel } from 'src/app/models/images-model';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-trainings-page',
  templateUrl: './my-trainings-page.component.html',
  styleUrls: ['./my-trainings-page.component.css'],
})
export class MyTrainingsPageComponent implements OnInit {
  constructor(
    private trainingService: TrainingService,
    private trainingSessionService: TrainingSessionService,
    private applicantService: ApplicantService,
    private modalService: NgbModal,
    private tagService: TagService,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private route: ActivatedRoute,

  ) {
    this.authenticationService.currentUser.subscribe((x) => (this.user = x));
  }
  closeResult = '';
  category: string | null = null;
  defaultProfile = './assets/images/defaultImages/defaultProfilePicture.png';
  defaultTraining = './assets/images/mainPageImages/logo.png';
  
  mobile: boolean = false;
  counter = 0;
  mode = '';
  myTrainings: TrainingModel[] = [];
  tagTraining: TagTrainingModel[] = [];
  user: UserModel;
  currentTraining: TrainingModel;
  trainers: UserModel[] = [];
  tags: TagModel[] = [];
  sessions: TrainingSessionModel[] = [];
  ordered_session: any[] = [];
  profileImages: ImagesModel[] = [];
  indexImages: ImagesModel[] = [];

  currentDate = new Date();
  deleteSession(session: TrainingSessionModel) {
    if (session.number_of_applicants > 0) {
      alert('Figyelem, az alkalomra már vannak jelentkezők!');
    }
    this.trainingSessionService.deleteTrainingSession(session).subscribe(
      (result) => {
        this.sessions.splice(
          this.sessions.findIndex((x) => x.id == session.id),
          1
        );
      },
      (error) => console.log(error)
    );
  }
  deleteApplication(sessionId: number) {
    this.applicantService
      .deleteApplicantByIds(this.user.id, sessionId)
      .subscribe(
        (result) => {
          this.sessions.splice(
            this.sessions.findIndex((x) => x.id == sessionId),
            1
          );
          
          if (this.sessions.length == 0) {
            this.myTrainings.splice(
              this.myTrainings.findIndex(
                (x) => x.id == this.currentTraining.id
              ),
              1
            );
            this.close();
          }
        },
        (error) => console.log(error)
      );
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.mode = params.get('mode');
    if (this.mode == 'trainer') {
      this.trainingService.getByTrainerId(this.user.id).subscribe(
        (result) => {
          this.myTrainings = result.trainings;
          this.userService.getUserById(this.user.id).subscribe(
            (result) => {
              this.user = result;
              this.userService.getImageById(this.user.imageId).subscribe(
                (result) => {
                  this.profileImages.push(result);
                },
                (error) => console.log(error)
              );                            
              for (const training of this.myTrainings) {                
                this.trainingService.getImageById(training.id).subscribe(
                  (result) => {
                    for (const item of result.images) {
                      if (item.id == training.indexImageId) {
                        this.indexImages.push(item);
                      }
                    }
                  },
                  (error) => console.log(error)
                );
              }
            },
            (error) => console.log(error)
          );
          this.tagTraining = result.tagTrainings;
        },
        (error) => console.log(error)
      );
    } else if (this.mode == 'applied') {
      this.trainingService.getByUserId(this.user.id).subscribe(
        (result) => {
          this.trainers = result.trainers;
          for (const item of this.trainers) {
            this.userService.getImageById(item.imageId).subscribe(
              (result) => {
                if (result != null) {
                  this.profileImages.push(result);
                }
              },
              (error) => console.log(error)
            );
          }
          this.myTrainings = result.trainings;
          for (const training of this.myTrainings) {                
            this.trainingService.getImageById(training.id).subscribe(
              (result) => {
                for (const item of result.images) {
                  if (item.id == training.indexImageId) {
                    this.indexImages.push(item);
                  }
                }
              },
              (error) => console.log(error)
            );
          }
          this.tagTraining.push.apply(this.tagTraining, result.tagTrainingList);
        },
        (error) => console.log(error)
      );
    }
  });
    if (window.innerWidth <= 991) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    this.tagService.getTags().subscribe(
      (result) => (this.tags = result),
      (error) => console.log(error)
    );
  }
  open(content: any, trainingId: number) {
    if (this.mode == 'trainer') {
      this.trainingSessionService.listByTrainingId(trainingId).subscribe(
        (result) => {
          this.sessions = result.sessions;
          this.currentTraining = result.training;
          this.CheckIfPast();
        },
        (error) => console.log(error)
      );
    } else if (this.mode == 'applied'){
      this.trainingSessionService
        .ListAppliedSessions(trainingId, this.user.id)
        .subscribe(
          (result) => {
            this.sessions = result.sessions;
            this.currentTraining = result.training;
            this.CheckIfPast();
          },
          (error) => console.log(error)
        );
    }
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
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
  CheckIfPast() {
    this.sessions.forEach((session) => {
      var year = Number(session.date.substring(0, 4));
      var month = Number(session.date.substring(5, 7));
      var day = Number(session.date.substring(8, 10));
      var hour = Number(session.date.substring(11, 13));
      var minute = Number(session.date.substring(14, 16));
      var sessionDate = new Date(year, month - 1, day, hour, minute);
      if (sessionDate < this.currentDate) {
        session.isPast = true;
      } else {
        session.isPast = false;
      }
    });
  }
}
