import { Component, OnInit } from '@angular/core';

import { CategoryModel } from 'src/app/models/category-model';

@Component({
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.css']
})
export class CategoryPageComponent implements OnInit {

  constructor() { }
  imgPrefix = "./assets/images/";
  smScreen = false;

  public categories: CategoryModel[] = [
   {name: 'Box', imgSrc: 'box.jpg'},
   {name: 'Crossfit', imgSrc: 'crossFitt.jpg'},
   {name: 'Labdarúgás', imgSrc: 'football.jpg'},
   {name: 'Kosárlabda', imgSrc: 'basketball.jpg'},
   {name: 'Kézilabda', imgSrc: 'handball.jpg'},
   {name: 'Röplabda', imgSrc: 'volleyball.jpg'},
   {name: 'Spartan', imgSrc: 'spartan.jpg'},
   {name: 'Tenisz', imgSrc: 'tennis.jpg'},
   {name: 'TRX', imgSrc: 'trx.jpg'},
   {name: 'Úszás', imgSrc: 'swimming.jpg'},
   {name: 'Lovaglás', imgSrc: 'riding.jpg'},
 
   {name: 'Jóga', imgSrc: 'yoga.jpg'}
    
  ];
  
  ngOnInit(): void {
      
  }

}
