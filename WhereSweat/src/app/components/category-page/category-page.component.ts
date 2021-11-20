import { Component, OnInit } from '@angular/core';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.css']
})
export class CategoryPageComponent implements OnInit {  
  constructor() { }
  imgSrc = "./assets/images/profile_rock.PNG";
  imgBckgSrc = "./assets/images/gym.jpg";

  mobile: boolean = false;
  public trainings: TrainingModel[] = [
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
    {name: 'Teri trx', description: 'Zenés TRX edzés Bana city központjában', category:'TRX', id:0, max_member:8, trainer_id:0},
     
  ];
  public users: UserModel[] = [
    {id: 1,email: 'tesztelek@gmail.com',full_name: 'Teszt Elek',trainer: true,phone_number: '+36701234678',city: 'Győr' },
    {id: 2,email: 'tesztelek@gmail.com',full_name: 'Suttyofalvi Csoves Banat',trainer: true,phone_number: '+36701234678',city: 'Budapest' },
    {id: 3,email: 'tesztelek@gmail.com',full_name: 'Kandisz Nóra',trainer: true,phone_number: '+36701234678',city: 'Miskoc' },
    {id: 4,email: 'tesztelek@gmail.com',full_name: 'Kovács Ákos',trainer: true,phone_number: '+36701234678',city: 'Tatabanya' },
    {id: 5,email: 'tesztelek@gmail.com',full_name: 'Futty Imre',trainer: true,phone_number: '+36701234678',city: 'Balatonfured' },
    {id: 6,email: 'tesztelek@gmail.com',full_name: 'Mittomen Karoly',trainer: true,phone_number: '+36701234678',city: 'Szeged' },
    {id: 7,email: 'tesztelek@gmail.com',full_name: 'Teszt Elek',trainer: true,phone_number: '+36701234678',city: 'Győr' },
    {id: 8,email: 'tesztelek@gmail.com',full_name: 'Teszt Elek',trainer: true,phone_number: '+36701234678',city: 'Győr' },
    {id: 9,email: 'tesztelek@gmail.com',full_name: 'Teszt Elek',trainer: true,phone_number: '+36701234678',city: 'Győr' },
    {id: 10,email: 'tesztelek@gmail.com',full_name: 'Teszt Elek',trainer: true,phone_number: '+36701234678',city: 'Győr' },
    {id: 11,email: 'tesztelek@gmail.com',full_name: 'Teszt Elek',trainer: true,phone_number: '+36701234678',city: 'Győr' }
];


  ngOnInit(): void {
    if (window.innerWidth <= 991) { 
      this.mobile = true;
    }
    window.onresize = () => this.mobile = window.innerWidth <= 991;
  }

}
