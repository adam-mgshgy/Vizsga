import { Component, OnInit } from '@angular/core';
import { CategoriesService } from 'src/app/services/categories.service';
import { CategoryModel } from 'src/app/models/category-model';
import { ImagesModel } from 'src/app/models/images-model';
@Component({
  selector: 'app-mainpage',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainpageComponent implements OnInit {
  constructor(private categoryService: CategoriesService) {}
  images: ImagesModel[];
  categories: CategoryModel[] = [];

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
