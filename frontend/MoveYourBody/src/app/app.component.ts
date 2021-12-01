import { Component } from '@angular/core';
import { CategoryModel } from './models/category-model';
import { UserModel } from './models/user-model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'MoveYourBody';

  public categories: CategoryModel[] = [
     {name: 'Box', imgSrc:""},
     {name: 'Crossfit', imgSrc:""},
     {name: 'Jóga', imgSrc:""},
     {name: 'Spartan', imgSrc:""},
     {name: 'Tenisz', imgSrc:""},
     {name: 'TRX', imgSrc:""}
    
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

  
}
