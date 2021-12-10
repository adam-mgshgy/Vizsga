import { Component, OnInit } from '@angular/core';
import { ResizedEvent } from 'angular-resize-event';
import { ActivatedRoute } from '@angular/router';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-my-trainings-page',
  templateUrl: './my-trainings-page.component.html',
  styleUrls: ['./my-trainings-page.component.css']
})
export class MyTrainingsPageComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }

  category: string | null = null;
  imgSrc = './assets/images/categoryPageImages/profile_rock.png';
  imgBckgSrc = './assets/images/categoryPageImages/index.jpg';

  mobile: boolean = false;

  counter = 0;

  
  public myTrainings: TrainingModel[] = [
    {
      name: 'Nagyon hosszú nevű edzés',
      description: 'Zenés TRX edzés Bana city központjában',
      category: 'TRX',
      id: 1,
      min_member: 3,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Egyéni Teri trx',
      description: 'Zenés TRX edzés személyi edzés keretein belül',
      category: 'TRX',
      id: 2,
      min_member: 1,
      max_member: 1,
      trainer_id: 0,
      contact_phone: '06701234567'
    }
  ];
  public users: UserModel[] = [
    {
      id: 1,
      email: 'tesztelek@gmail.com',
      full_name: 'Tesztelek Károlyné Elekfalvi Károly',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 2,
      email: 'jelentelek@gmail.com',
      full_name: 'Jelentkező Elek',
      trainer: false,
      phone_number: '+36301234678',
      location_id: 1,
    }
  ];
  public allTags: TagModel[] = [
    { id: 0, name: 'csoportos'},
    { id: 1, name: 'erőnléti'},
    { id: 2, name: 'saját testsúlyos'},
    { id: 3, name: 'edzőterem'},
    { id: 4, name: 'zsírégető'},
    { id: 5, name: 'személyi edzés'}
  ];


  ngOnInit(): void {
    if (window.innerWidth <= 991) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.tagsOnMobile();

    //Lekérdezés a back-end-ről
  }
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

}
