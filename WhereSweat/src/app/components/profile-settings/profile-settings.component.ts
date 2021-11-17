import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.css']
})
export class ProfileSettingsComponent implements OnInit {

  //users: UserModel[] = [];
  user: UserModel = {
      id: 1,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      city: 'Győr' 
  }

  constructor() { }

  ngOnInit(): void {
  }

}
