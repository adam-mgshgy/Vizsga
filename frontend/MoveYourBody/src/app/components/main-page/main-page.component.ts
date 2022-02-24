import { Component, OnInit } from '@angular/core';
import { CategoryModel } from 'src/app/models/category-model';
import { ImagesModel } from 'src/app/models/images-model';
import { CategoriesService } from 'src/app/services/categories.service';
@Component({
  selector: 'app-mainpage',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainpageComponent implements OnInit {
  constructor(private categoryService: CategoriesService) {}
  images: ImagesModel[];
  public categories: CategoryModel[] = [];

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(
      (result) => {
        this.categories = result.categories;
        this.images = result.images;
      },
      (error) => console.log(error)
    );
  }

}
