import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.css']
})
export class CategoryPageComponent implements OnInit {

  constructor() { }
  imgSrc = "./assets/images/profile_rock.PNG";
  imgBckgSrc = "./assets/images/trx.jpg";
  ngOnInit(): void {
  }

}
