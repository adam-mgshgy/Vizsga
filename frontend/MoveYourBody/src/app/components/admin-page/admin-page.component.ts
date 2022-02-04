import { Component, OnInit } from '@angular/core';
import { CategoryModel } from 'src/app/models/category-model';
import { TagModel } from 'src/app/models/tag-model';
import { CategoriesService } from 'src/app/services/categories.service';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css'],
})
export class AdminPageComponent implements OnInit {
  mobile: boolean = false;
  errorMessage = '';

  newTag: TagModel = new TagModel();
  newCategory: CategoryModel = new CategoryModel();
  constructor(
    private tagService: TagService,
    private categoriesService: CategoriesService
  ) {}

  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
  }

  CancelTag() {
    this.newTag = new TagModel();
  }
  CancelCategory() {
    this.newCategory = new CategoryModel();
  }
  SaveTag() {
    this.newTag.id = 0;
    this.tagService.newTag(this.newTag).subscribe(
      (result) => console.log(result),
      (error) => console.log(error)
    );
  }
  SaveCategory() {
    this.newCategory.id = 0;
    this.categoriesService.newCategory(this.newCategory).subscribe(
      (result) => console.log(result),
      (error) => console.log(error)
    );
  }
}
