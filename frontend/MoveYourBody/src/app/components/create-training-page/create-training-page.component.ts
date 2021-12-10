import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryModel } from 'src/app/models/category-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-create-training-page',
  templateUrl: './create-training-page.component.html',
  styleUrls: ['./create-training-page.component.css']
})
export class CreateTrainingPageComponent implements OnInit {
  id: string | null = null;
  isChecked = false;
  user: UserModel = {
    id: 1,
    email: 'tesztelek@gmail.com',
    full_name: 'Teszt Elek',
    trainer: true,
    phone_number: '+36701234678',
    location_id: 1
  }
  public categories: CategoryModel[] = [
    { name: 'Box', imgSrc: 'box.jpg' },
    { name: 'Crossfit', imgSrc: 'crossFitt.jpg' },
    { name: 'Labdarúgás', imgSrc: 'football.jpg' },
    { name: 'Kosárlabda', imgSrc: 'basketball.jpg' },
    { name: 'Kézilabda', imgSrc: 'handball.jpg' },
    { name: 'Röplabda', imgSrc: 'volleyball.jpg' },
    { name: 'Spartan', imgSrc: 'spartan.jpg' },
    { name: 'Tenisz', imgSrc: 'tennis.jpg' },
    { name: 'TRX', imgSrc: 'trx.jpg' },
    { name: 'Úszás', imgSrc: 'swimming.jpg' },
    { name: 'Lovaglás', imgSrc: 'riding.jpg' },
    { name: 'Jóga', imgSrc: 'yoga.jpg' },
  ];
  public tags: TagModel[] = [
    { id: 0, name: 'csoportos'},
    { id: 1, name: 'erőnléti'},
    { id: 2, name: 'saját testsúlyos'},
    { id: 3, name: 'edzőterem'},
    { id: 4, name: 'zsírégető'},

  ];
  public training: TrainingModel = new TrainingModel();
  public selectedTags: TagModel[] = [];
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
  public OnSelect(tag: TagModel): void {
    console.log("tag");
    this.selectedTags.push(tag);
  }
  mobile: boolean = false;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    this.route.paramMap.subscribe((params) => {
      this.id = params.get('id');
      if (this.id) {
        const filteredTrainings = this.myTrainings.filter(
          (t) => t.id == this.id);
        if (filteredTrainings.length == 1) {
          this.training = filteredTrainings[0];
        }
        else {
          this.training = new TrainingModel();
        }
      } else {
        this.training = new TrainingModel();
      }
    });
  }

}
