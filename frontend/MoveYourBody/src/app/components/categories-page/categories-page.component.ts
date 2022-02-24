import { Component, OnInit } from '@angular/core';
import { CategoryModel } from 'src/app/models/category-model';
import { ImagesModel } from 'src/app/models/images-model';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-categories-page',
  templateUrl: './categories-page.component.html',
  styleUrls: ['./categories-page.component.css'],
})
export class CategoriesPageComponent implements OnInit {
  constructor(private categoryService: CategoriesService) {}

  images: ImagesModel[];
  public categories: CategoryModel[] = [];

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(
      (result) => {
        //this.categories = result.categories;
        //this.images = result.images;
      },
      (error) => console.log(error)
    );
  }
}
