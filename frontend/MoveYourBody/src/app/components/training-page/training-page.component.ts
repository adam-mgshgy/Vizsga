import { Component, OnInit } from '@angular/core';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationModel } from 'src/app/models/location-model';

@Component({
  selector: 'app-training-page',
  templateUrl: './training-page.component.html',
  styleUrls: ['./training-page.component.css']
})
export class TrainingPageComponent implements OnInit {

  public location: LocationModel = {
    id: 1,
    county_name: "Komárom-Esztergom megye",
    city_name: "Bana",
    address_name: "Kis Károly utca 11."
  }
  
  public training: TrainingModel =
    {
      id: 0,
      name: 'Nagyon hosszú nevű edzés',
      category: 'TRX',
      trainer_id: 0,
      min_member: 6,
      max_member: 8,
      description: 'Zenés TRX edzés Bana city központjában. Hozz magaddal törölközőt, váltócipőt és vizet! Várunk sok szeretettel!',
      contact_phone: '0670123456'
    }
  public sessions: TrainingSessionModel[] = [
    {
      id: 1,
      date: '2021.12.12. 15:30',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    },
    {
      id: 1,
      date: '2021.12.12. 15:30',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    },
    {
      id: 1,
      date: '2021.12.12. 15:30',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    },
    {
      id: 2,
      date: '2021.12.12. 16:30',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    },
    {
      id: 3,
      date: '2021.12.12. 17:30',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    }
  ]
    public tags: TagModel[] = [
      { id: 0, name: 'csoportos', colour: '#6610f2' },
      { id: 1, name: 'erőnléti', colour: 'black' },
      { id: 2, name: 'saját testsúlyos', colour: '#fd7e14' },
      { id: 3, name: 'edzőterem', colour: 'red' },
      { id: 4, name: 'zsírégető', colour: '#0dcaf0' },
      { id: 5, name: 'személyi edzés', colour: '#0dcaf0' }
    ];
    user: UserModel = {
      id: 1,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1, 
  }
  mobile: boolean = false;
  constructor() { }

  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
  }

}
