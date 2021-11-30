import { Component, OnInit } from '@angular/core';
import { SessionModel } from 'src/app/models/session-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-training-page',
  templateUrl: './training-page.component.html',
  styleUrls: ['./training-page.component.css']
})
export class TrainingPageComponent implements OnInit {

  constructor() { }
  public training: TrainingModel =
    {
      name: 'Nagyon hosszú nevű edzés',
      description: 'Zenés TRX edzés Bana city központjában Hozz magaddal törcsit, váltócipőt és vizet!',
      category: 'TRX',
      id: 0,
      min_member: 6,
      max_member: 8,
      trainer_id: 0,
    }
  public sessions: SessionModel[] = [
    {
      id: 1,
      date: '2021.12.12. 15:30',
      place: 'Bana, Kis Károly utca 8.',
      price: 1500,
      minutes: 60,
    },
    {
      id: 2,
      date: '2021.12.12. 16:30',
      place: 'Bana, Kis Károly utca 8.',
      price: 1500,
      minutes: 60,
    },
    {
      id: 3,
      date: '2021.12.12. 17:30',
      place: 'Bana, Kis Károly utca 8.',
      price: 1500,
      minutes: 60,
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
      city: 'Győr' 
  }
  
  ngOnInit(): void {
  }

}
