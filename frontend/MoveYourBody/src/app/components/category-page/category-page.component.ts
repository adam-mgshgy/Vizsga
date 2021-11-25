import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  public trainings: TrainingModel[] = [];
  public allTrainings: TrainingModel[] = [
    {
      name: 'Nagyon hosszú nevű edzés',
      description: 'Zenés TRX edzés Bana city központjában',
      category: 'TRX',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Teri trx',
      description: 'Zenés TRX edzés Bana city központjában',
      category: 'TRX',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Teri trx',
      description: 'Zenés TRX edzés Bana city központjában',
      category: 'TRX',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Kiütünk mindenkit',
      description: 'Box edzés Pistivel',
      category: 'Box',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Lovaglás nagyoknak',
      description: 'Zenés TRX edzés Bana city központjában',
      category: 'Lovaglás',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Légy hal',
      description: 'Ússz a víz alatt',
      category: 'Úszás',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: '300 helyett 8an spartan edzés',
      description: 'Thermöpula helyett Bana city központjában',
      category: 'Spartan',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Foci Ferivel',
      description: 'Kígyós edzés',
      category: 'Labdarúgás',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Ripi röpi Rebekával',
      description: 'Röplabda antireptetése',
      category: 'Röplabda',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Crossfit edzés',
      description: 'Crossmotor helyett rendes edzés',
      category: 'Crossfit',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Foci Viktorral',
      description: 'Minden edzés után GYŐZÜNK!',
      category: 'Labdarúgás',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Tenisz a salgótarjáni Federerrel',
      description: 'Fejleszd magad az új lyukas hálós ütőkkel!!',
      category: 'Tenisz',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
    {
      name: 'Kosárlabda focilabdával',
      description: 'Nincs pénz kosárlabdára, gyakorolni jó lesz focilabdával is.',
      category: 'Kosárlabda',
      id: 0,
      max_member: 8,
      trainer_id: 0,
    },
  ];
  public users: UserModel[] = [
    {
      id: 1,
      email: 'tesztelek@gmail.com',
      full_name: 'Tesztelek Károlyné Elekfalvi Károly',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Győr',
    },
    {
      id: 2,
      email: 'tesztelek@gmail.com',
      full_name: 'Tóth Sándor',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Budapest',
    },
    {
      id: 3,
      email: 'tesztelek@gmail.com',
      full_name: 'Kandisz Nóra',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Miskolc',
    },
    {
      id: 4,
      email: 'tesztelek@gmail.com',
      full_name: 'Kovács Ákos',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Tatabanya',
    },
    {
      id: 5,
      email: 'tesztelek@gmail.com',
      full_name: 'Futty Imre',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Balatonfured',
    },
    {
      id: 6,
      email: 'tesztelek@gmail.com',
      full_name: 'Mittomen Karoly',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Szeged',
    },
    {
      id: 7,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Győr',
    },
    {
      id: 8,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Győr',
    },
    {
      id: 9,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Győr',
    },
    {
      id: 10,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Győr',
    },
    {
      id: 11,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Győr',
    },
  ];
  public tags: TagModel[] = [
    { id: 0, name: 'csoportos', colour: '#6610f2' },
    { id: 1, name: 'erőnléti', colour: 'black' },
    { id: 2, name: 'saját testsúlyos', colour: '#fd7e14' },
    { id: 3, name: 'edzőterem', colour: 'red' },
    { id: 4, name: 'zsírégető', colour: '#0dcaf0' },
  ];

  ngOnInit(): void {
    if (window.innerWidth <= 991) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.route.paramMap.subscribe((params) => {
      this.category = params.get('category');
      this.trainings = this.allTrainings.filter(
        (t) => t.category == this.category
      );
    });

    //Lekérdezés a back-end-ről
  }
}
