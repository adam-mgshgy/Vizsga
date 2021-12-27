import { Component } from '@angular/core';
import { LoginpageComponent } from './components/login-page/login-page.component';
import { CategoryModel } from './models/category-model';
import { UserModel } from './models/user-model';
import { Subscription } from 'rxjs';
import { LoginService } from './services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  user: UserModel;
  subscription: Subscription;
  constructor(private loginService: LoginService) {  }
  ngOnInit() {
    this.subscription = this.loginService.currentUser.subscribe(user => this.user = user)
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  title = 'MoveYourBody';
  public categories: CategoryModel[] = [];
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
      password: "pwd",
      full_name: 'Tóth Sándor',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 3,
      email: 'tesztelek@gmail.com',
      full_name: 'Kandisz Nóra',
      password: "pwd",
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 4,
      email: 'tesztelek@gmail.com',
      full_name: 'Kovács Ákos',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 5,
      email: 'tesztelek@gmail.com',
      full_name: 'Futty Imre',
      password: "pwd",
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 6,
      email: 'tesztelek@gmail.com',
      full_name: 'Mittomen Karoly',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 7,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      password: "pwd",
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 8,
      email: 'tesztelek@gmail.com',
      password: "pwd",
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 9,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      password: "pwd",
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 10,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      password: "pwd",
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
    {
      id: 11,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      password: "pwd",
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1
    },
  ];

  
}
