import { Component, OnInit } from '@angular/core';
import { CategoriesService } from 'src/app/services/categories.service';
import { TagService } from 'src/app/services/tag.service';
import { CategoryModel } from 'src/app/models/category-model';
import { TagModel } from 'src/app/models/tag-model';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css'],
})
export class AdminPageComponent implements OnInit {
  constructor(
    private tagService: TagService,
    private categoriesService: CategoriesService
  ) {}

  mobile: boolean = false;
  errorMessage = '';
  newTag: TagModel = new TagModel();
  newCategory: CategoryModel = new CategoryModel();
  cardImageBase64: string;
  isImageSaved: boolean;
  image: string[] = [];

  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
  }
 
  fileChangeEvent(fileInput: any) {
    if (fileInput.target.files && fileInput.target.files[0]) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const imgBase64Path = e.target.result;
        this.cardImageBase64 = imgBase64Path;
        this.isImageSaved = true;
        this.image.push(this.cardImageBase64);
      };
      reader.readAsDataURL(fileInput.target.files[0]);
    }
  }
  CancelTag() {
    this.newTag = new TagModel();
  }
  CancelCategory() {
    this.newCategory = new CategoryModel();
    this.image = [];
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
    this.categoriesService.newImage(this.image).subscribe(
      result => {
        this.newCategory.image_id = result.id;
        this.categoriesService.newCategory(this.newCategory).subscribe(
          (result) => console.log(result),
          (error) => console.log(error)
        );

      }
    );
  }
  deleteImage() {
    this.image = [];
    this.isImageSaved = false;
  }
}
