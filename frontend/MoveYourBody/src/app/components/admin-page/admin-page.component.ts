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
  cardImageBase64: string;
  isImageSaved: boolean;
  image: string[] = [];
  fileChangeEvent(fileInput: any) {
    
    if (fileInput.target.files && fileInput.target.files[0]) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const imgBase64Path = e.target.result;
        this.cardImageBase64 = imgBase64Path;
        this.isImageSaved = true;
        this.image.push(this.cardImageBase64);

        this.save();
      };
      reader.readAsDataURL(fileInput.target.files[0]);
    }
  }
  save(){
    //todo save categoryimage
  }
  deleteImage(){
    this.image = [];
    this.isImageSaved = false;
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
