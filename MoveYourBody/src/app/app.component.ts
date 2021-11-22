import { Component } from '@angular/core';
import { CategoryModel } from './models/category-model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'MoveYourBody';

  public categories: CategoryModel[] = [
     {name: 'Box', imgSrc:""},
     {name: 'Cross Fitt', imgSrc:""},
     {name: 'Jóga', imgSrc:""},
     {name: 'Spartan', imgSrc:""},
     {name: 'Tenisz', imgSrc:""},
     {name: 'TRX', imgSrc:""}
    
  ];

  
}
