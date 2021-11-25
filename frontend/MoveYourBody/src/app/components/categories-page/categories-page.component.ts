import { Component, OnInit } from '@angular/core';

import { CategoryModel } from 'src/app/models/category-model';

@Component({
  selector: 'app-categories-page',
  templateUrl: './categories-page.component.html',
  styleUrls: ['./categories-page.component.css'],
})
export class CategoriesPageComponent implements OnInit {
  constructor() {}
  imgPrefix = './assets/images/';

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

  ngOnInit(): void {}
}
