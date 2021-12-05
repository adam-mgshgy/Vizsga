import { Component, OnInit } from '@angular/core';
import { CategoryModel } from 'src/app/models/category-model';
import { TagModel } from 'src/app/models/tag-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-create-training-page',
  templateUrl: './create-training-page.component.html',
  styleUrls: ['./create-training-page.component.css']
})
export class CreateTrainingPageComponent implements OnInit {
  isChecked = false;
  user: UserModel = {
    id: 1,
    email: 'tesztelek@gmail.com',
    full_name: 'Teszt Elek',
    trainer: true,
    phone_number: '+36701234678',
    city: 'Győr' 
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
public tags : TagModel[] = [
    { id: 0, name: 'csoportos', colour: '#6610f2' },
    { id: 1, name: 'erőnléti', colour: 'black' },
    { id: 2, name: 'saját testsúlyos', colour: '#fd7e14' },
    { id: 3, name: 'edzőterem', colour: 'red' },
    { id: 4, name: 'zsírégető', colour: '#0dcaf0' },
  
];
public selectedTags: TagModel[] = [];

public OnSelect(tag: TagModel): void {
  console.log("tag");
  this.selectedTags.push(tag);
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
