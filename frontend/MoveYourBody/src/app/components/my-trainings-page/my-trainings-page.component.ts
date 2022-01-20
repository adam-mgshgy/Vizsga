import { Component, Inject, OnInit } from '@angular/core';
import { ResizedEvent } from 'angular-resize-event';
import { ActivatedRoute } from '@angular/router';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationModel } from 'src/app/models/location-model';
import { isNull } from '@angular/compiler/src/output/output_ast';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { MatExpansionModule } from '@angular/material/expansion';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { TrainingService } from 'src/app/services/training.service';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { LoginService } from 'src/app/services/login.service';
import { TagTrainingService } from 'src/app/services/tag-training.service';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { TagService } from 'src/app/services/tag.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TrainingSessionService } from 'src/app/services/training-session.service';
import { ApplicantService } from 'src/app/services/applicant.service';
import { ApplicantModel } from 'src/app/models/applicant-model';

@Component({
  selector: 'app-my-trainings-page',
  templateUrl: './my-trainings-page.component.html',
  styleUrls: ['./my-trainings-page.component.css'],
})
export class MyTrainingsPageComponent implements OnInit {
  constructor(
    private trainingService: TrainingService,
    private applicantService: ApplicantService,
    private trainingSessionService: TrainingSessionService,
    private modalService: NgbModal,
    private tagTrainingService: TagTrainingService,
    private tagService: TagService,
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private userService: UserService,
    private loginService: LoginService,

  ) {
    this.authenticationService.currentUser.subscribe(
      (x) => (this.user = x)
    );
    this.ordered_session = Object.values(
      this.groupByDate(this.sessions, 'date')
    ); //date alapján rendez
  }
  closeResult = '';
  category: string | null = null;
  imgSrc = './assets/images/categoryPageImages/profile_rock.png';
  imgBckgSrc = './assets/images/categoryPageImages/index.jpg';
  mobile: boolean = false;
  counter = 0;

  public myTrainings: TrainingModel[] = [];
  public tagTraining: TagTrainingModel[] = [];
  user: UserModel;
  public applicants: ApplicantModel[] = [ ];
  public applicantUsers: UserModel[] = [ ];
  currentTraining : TrainingModel;
  public tags: TagModel[] = [];

  public sessions: TrainingSessionModel[] = [];

  public statuses = ['4/8', '8/8 Betelt', '2/8 Kevés jelentkező', '5/8'];

  public ordered_session: any[] = [];

  groupByDate(array, property) {
    var hash = {},
      props = property.split('.');
    for (var i = 0; i < array.length; i++) {
      var key = props.reduce(function (acc, prop) {
        return acc && acc[prop].substr(0, 10);
      }, array[i]);
      if (!hash[key]) hash[key] = [];
      hash[key].push(array[i]);
    }
    return hash;
  }

  ngOnInit(): void {
    this.trainingService.getByTrainerId(this.user.id).subscribe(
      (result) => {
        this.myTrainings = result;

        for (const item of this.myTrainings) {
          this.tagTrainingService.getByTraining(item.id).subscribe((result) => {
            this.tagTraining.push.apply(this.tagTraining, result);            
          });
        }
      },
      (error) => console.log(error)
    );
    if (window.innerWidth <= 991) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    this.tagsOnMobile();
    this.tagService.getTags().subscribe(
      (result) => (this.tags = result),
      (error) => console.log(error)
    );
  }

  usersList() {}
  onResized(event: ResizedEvent) {
    this.tagsOnMobile();
  }
  tagsOnMobile() {
    if (window.innerWidth < 2250 && window.innerWidth > 1950) {
      this.counter = 5;
    } else if (window.innerWidth <= 1950 && window.innerWidth > 1700) {
      this.counter = 4;
    } else if (window.innerWidth <= 1700 && window.innerWidth > 1399) {
      this.counter = 3;
    } else if (window.innerWidth <= 1399 && window.innerWidth > 1300) {
      this.counter = 5;
    } else if (window.innerWidth <= 1300 && window.innerWidth > 1150) {
      this.counter = 4;
    } else if (window.innerWidth <= 1150 && window.innerWidth > 950) {
      this.counter = 3;
    } else if (window.innerWidth <= 950 && window.innerWidth > 767) {
      this.counter = 2;
    } else if (window.innerWidth <= 767 && window.innerWidth > 650) {
      this.counter = 5;
    } else if (window.innerWidth <= 650 && window.innerWidth > 580) {
      this.counter = 4;
    } else if (window.innerWidth <= 580 && window.innerWidth > 480) {
      this.counter = 3;
    } else if (window.innerWidth <= 480) {
      this.counter = 2;
    } else {
      this.counter = 6;
    }
  }
  open(content: any, trainingId: number) {
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          this.closeResult = `Closed with: ${result}`;
          this.trainingService.getById(trainingId).subscribe(
            (result) => {
            this.currentTraining = result;
              this.trainingSessionService.listByTrainingId(this.currentTraining.id).subscribe(
                (result) => {
                  this.sessions = result;
                  this.sessions.forEach(session => {
                    this.applicantService.listBySessionId(session.id).subscribe(
                      (result) => {
                        session.numberOfApplicants = result.length;
                        this.applicants = result;
                        this.applicants.forEach(applicant => {
                          this.userService.getUserById(applicant.user_id).subscribe(
                            (result) => {
                              this.applicantUsers.push(result);
                            }, (error) => console.log(error)
                          )
                        });
                      }, (error) => console.log(error)
                    );
                  });
                }, (error) => console.log(error)
              );
            }, (error) => console.log(error)
          );
        },
        (reason) => {
          this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
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
