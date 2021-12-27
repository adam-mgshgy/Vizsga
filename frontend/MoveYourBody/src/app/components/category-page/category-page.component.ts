import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResizedEvent } from 'angular-resize-event';
import { LocationModel } from 'src/app/models/location-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.css'],
})
export class CategoryPageComponent implements OnInit {
  category: string | null = null;

  constructor(private route: ActivatedRoute) {}
  imgSrc = './assets/images/categoryPageImages/profile_rock.png';
  imgBckgSrc = './assets/images/categoryPageImages/index.jpg';

  mobile: boolean = false;

  counter = 0;

  public trainings: TrainingModel[] = [];
  public allTrainings: TrainingModel[] = [
    {
      name: 'Nagyon hosszú nevű edzés',
      description: 'Zenés TRX edzés Bana city központjában',
      category_id: 1,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Teri trx',
      description: 'Zenés TRX edzés Bana city központjában',
      category_id: 1,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Teri trx',
      description: 'Zenés TRX edzés Bana city központjában',
      category_id: 1,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Kiütünk mindenkit',
      description: 'Box edzés Pistivel',
      category_id:2,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Lovaglás nagyoknak',
      description: 'Zenés TRX edzés Bana city központjában',
      category_id: 3,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Légy hal',
      description: 'Ússz a víz alatt',
      category_id: 4,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: '300 helyett 8an spartan edzés',
      description: 'Thermöpula helyett Bana city központjában',
      category_id: 5,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Foci Ferivel',
      description: 'Kígyós edzés',
      category_id:61,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Ripi röpi Rebekával',
      description: 'Röplabda antireptetése',
      category_id: 6,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Crossfit edzés',
      description: 'Crossmotor helyett rendes edzés',
      category_id: 6,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Foci Viktorral',
      description: 'Minden edzés után GYŐZÜNK!',
      category_id: 7,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Tenisz a salgótarjáni Federerrel',
      description: 'Fejleszd magad az új lyukas hálós ütőkkel!!',
      category_id: 8,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
    {
      name: 'Kosárlabda focilabdával',
      description:
        'Nincs pénz kosárlabdára, gyakorolni jó lesz focilabdával is.',
        category_id: 8,
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
      contact_phone: '06701234567'
    },
  ];
  public users: UserModel[] = [
    {
      id: 1,
      email: 'tesztelek@gmail.com',
      full_name: 'Tesztelek Károlyné Elekfalvi Károly',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 2,
      email: 'tesztelek@gmail.com',
      full_name: 'Tóth Sándor',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 3,
      email: 'tesztelek@gmail.com',
      full_name: 'Kandisz Nóra',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 4,
      email: 'tesztelek@gmail.com',
      full_name: 'Kovács Ákos',
      trainer: true,
      phone_number: '+36701234678',
      password: "pwd",
      location_id: 1
    },
    {
      id: 5,
      email: 'tesztelek@gmail.com',
      full_name: 'Futty Imre',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 6,
      email: 'tesztelek@gmail.com',
      password: "pwd",
      full_name: 'Mittomen Karoly',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 7,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 8,
      password: "pwd",
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 9,
      password: "pwd",
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 10,
      password: "pwd",
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 11,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
  ];
  public allTags: TagModel[] = [
    { id: 0, name: 'csoportos', colour: '#6610f2' },
    { id: 1, name: 'erőnléti', colour: 'black' },
    { id: 2, name: 'saját testsúlyos', colour: '#fd7e14' },
    { id: 3, name: 'edzőterem', colour: 'red' },
    { id: 4, name: 'zsírégető', colour: '#0dcaf0' },
    { id: 5, name: 'személyi edzés', colour: 'green' }
  ];
  public locations: LocationModel[] = [
    {
      id: 1,
      county_name: "Komárom-Esztergom megye",
      city_name: "Bana",
      address_name: "Kis Károly utca 11."
    }, 
    {
      id: 2,
      county_name: "Komárom-Esztergom megye",
      city_name: "Bana",
      address_name: "Kis Károly utca 12."
    }];


  ngOnInit(): void {
    if (window.innerWidth <= 991) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.tagsOnMobile();

    // this.route.paramMap.subscribe((params) => {
    //   this.category = params.get('category');
    //   this.trainings = this.allTrainings.filter(
    //     (t) => t.category == this.category
    //   );
    // });

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
