import { Component, OnInit } from '@angular/core';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-training-page',
  templateUrl: './training-page.component.html',
  styleUrls: ['./training-page.component.css']
})
export class TrainingPageComponent implements OnInit {

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
      place: 'Bana, Kis Károly utca 8.',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    },
    {
      id: 1,
      date: '2021.12.12. 15:30',
      place: 'Bana, Kis Károly utca 8.',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    },
    {
      id: 1,
      date: '2021.12.12. 15:30',
      place: 'Bana, Kis Károly utca 8.',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    },
    {
      id: 2,
      date: '2021.12.12. 16:30',
      place: 'Bana, Kis Károly utca 8.',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    },
    {
      id: 3,
      date: '2021.12.12. 17:30',
      place: 'Bana, Kis Károly utca 8.',
      price: 1500,
      minutes: 60,
      training_id: 1,
      location_id: 1
    }
  ]
    public tags: TagModel[] = [
      { id: 0, name: 'csoportos'},
      { id: 1, name: 'erőnléti'},
      { id: 2, name: 'saját testsúlyos'},
      { id: 3, name: 'edzőterem'},
      { id: 4, name: 'zsírégető'},
      { id: 5, name: 'személyi edzés'}
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
