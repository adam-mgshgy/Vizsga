import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.css']
})
export class CategoryPageComponent implements OnInit {  
  constructor() { }
  imgSrc = "./assets/images/profile_rock.png";
  imgBckgSrc = "./assets/images/gym.jpg";

  mobile: boolean = false;

  ngOnInit(): void {
    if (window.innerWidth <= 991) { 
      this.mobile = true;
    }
    window.onresize = () => this.mobile = window.innerWidth <= 991;
  }

}
