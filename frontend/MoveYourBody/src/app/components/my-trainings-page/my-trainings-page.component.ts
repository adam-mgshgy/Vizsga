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

@Component({
  selector: 'app-my-trainings-page',
  templateUrl: './my-trainings-page.component.html',
  styleUrls: ['./my-trainings-page.component.css'],
})
export class MyTrainingsPageComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private trainingService: TrainingService,
    private userService: UserService,
    private modalService: NgbModal,
    private loginService: LoginService,
    private tagTrainingService: TagTrainingService,
    private tagService: TagService,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(
      (x) => (this.user = x)
    );
    this.ordered_session = Object.values(
      this.groupByDate(this.training_session, 'date')
    ); //date alapján rendez
  }
  closeResult = '';

  category: string | null = null;
  imgSrc = './assets/images/categoryPageImages/profile_rock.png';
  imgBckgSrc = './assets/images/categoryPageImages/index.jpg';

  mobile: boolean = false;

  counter = 0;

  date = '';

  public myTrainings: TrainingModel[] = [
    // {
    //   name: 'Nagyon hosszú nevű edzés',
    //   description: 'Zenés TRX edzés Bana city központjában',
    //   category_id: 4,
    //   id: 1,
    //   min_member: 3,
    //   max_member: 8,
    //   trainer_id: 1,
    //   contact_phone: '06701234567'
    // },
    // {
    //   name: 'Egyéni Teri trx',
    //   description: 'Zenés TRX edzés személyi edzés keretein belül',
    //   category_id: 4,
    //   id: 2,
    //   min_member: 1,
    //   max_member: 1,
    //   trainer_id: 1,
    //   contact_phone: '06701234567'
    // },
  ];
  public tagTraining: TagTrainingModel[] = [];

  user: UserModel;


  public users: UserModel[] = [
    //TODO from backend
    {
      id: 1,
      email: 'elekgmail',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36301234678',
      password: 'pwd',
      location_id: 2,
      role:'Trainer',
      token : ''
    },
    {
      id: 2,
      email: 'jelentelek@gmail.com',
      full_name: 'Jelentkező Elek',
      trainer: false,
      phone_number: '+36301234678',
      password: 'pwd',
      location_id: 2,
      role:'Trainer',
      token : ''
    },
    {
      id: 3,
      email: 'jelentelek@gmail.com',
      password: 'pwd',
      full_name: 'Jelentkező Károly',
      trainer: false,
      phone_number: '+36301234678',
      location_id: 1,
      role:'Trainer',
      token : ''
    },
    {
      id: 4,
      email: 'jelentelek@gmail.com',
      full_name: 'Jelentkező Erika',
      password: 'pwd',
      trainer: false,
      phone_number: '+36301234678',
      location_id: 1,
      role:'Trainer',
      token : ''
    },
    {
      id: 5,
      email: 'jelentelek@gmail.com',
      password: 'pwd',
      full_name: 'Jelentkező Zsolt',
      trainer: false,
      phone_number: '+36301234678',
      location_id: 2,
      role:'Trainer',
      token : ''
    },
  ];

  public tags: TagModel[] = [];

  public applicant = [
    {
      training_session_id: 1,
      user_id: 2,
    },
  ];

  public training_session: TrainingSessionModel[] = [
    {
      id: 1,
      date: '2021.12.23 09:00',
      price: 4000,
      minutes: 60,
      location_id: 1,
      address_name: '',
      place_name: '',
      max_member: 8,
      min_member: 5,
      training_id: 1,
      numberOfApplicants: 0,
    },
    {
      id: 2,
      date: '2021.12.24 15:00',
      price: 5000,
      minutes: 70,
      location_id: 1,
      address_name: '',
      place_name: '',
      max_member: 8,
      min_member: 5,
      numberOfApplicants: 0,
      training_id: 1,
    },
    {
      id: 3,
      date: '2021.12.25 18:00',
      price: 5000,
      minutes: 70,
      location_id: 1,
      numberOfApplicants: 0,
      address_name: '',
      max_member: 8,
      min_member: 5,
      place_name: '',
      training_id: 1,
    },
    {
      id: 4,
      date: '2021.12.25 19:00',
      price: 5000,
      minutes: 70,
      location_id: 1,
      numberOfApplicants: 0,
      max_member: 8,
      min_member: 5,
      address_name: '',
      place_name: '',
      training_id: 1,
    },
  ];

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

    console.log(this.date);
    if (window.innerWidth <= 991) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.tagsOnMobile();

    this.tagService.getTags().subscribe(
      (result) => (this.tags = result),
      (error) => console.log(error)
    );

    //Lekérdezés a back-end-ről
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
  open(content: any) {
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
