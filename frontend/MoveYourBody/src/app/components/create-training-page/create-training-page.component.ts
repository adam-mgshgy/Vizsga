import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryModel } from 'src/app/models/category-model';
import { LocationModel } from 'src/app/models/location-model';
import { TagModel } from 'src/app/models/tag-model';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';
import { CategoriesService } from 'src/app/services/categories.service';
import { TagTrainingService } from 'src/app/services/tag-training.service';
import { TagService } from 'src/app/services/tag.service';
import { TrainingService } from 'src/app/services/training.service';

@Component({
  selector: 'app-create-training-page',
  templateUrl: './create-training-page.component.html',
  styleUrls: ['./create-training-page.component.css'],
})
export class CreateTrainingPageComponent implements OnInit {
  id: string | null = null;
  isChecked = false;
  selectedCat = '';

  user: UserModel = {
    id: 1,
    email: 'elekgmail',
    full_name: 'Teszt Elek',
    trainer: true,
    phone_number: '+36301112233',
    location_id: 348,
    password: 'pwd',
  };
  public categories: CategoryModel[] = [];
  public tags: TagModel[] = [];
  public training: TrainingModel = new TrainingModel();
  public selectedTags: string[]= [];
  public tagTraining: TagTrainingModel = new TagTrainingModel();

  mobile: boolean = false;
  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoriesService,
    private trainingService: TrainingService,
    private tagService: TagService,
    private tagTrainingService: TagTrainingService
  ) {}

  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.categoryService.getCategories().subscribe(
      (result) => (this.categories = result),
      (error) => console.log(error)
    );
    
    this.tagService.getTags().subscribe(
      (result) => (this.tags = result),
      (error) => console.log(error)
    );

    // this.route.paramMap.subscribe((params) => {
    //   this.id = params.get('id');
    //   if (this.id) {
    //     const filteredTrainings = this.myTrainings.filter(
    //       (t) => t.id == Number(this.id)
    //     );
    //     if (filteredTrainings.length == 1) {
    //       this.training = filteredTrainings[0];
    //     } else {
    //       this.training = new TrainingModel();
    //     }
    //   } else {
    //     this.training = new TrainingModel();
    //   }
    // });
  }
  public min_member: number;
  public max_member: number;
  otherPhoneNumber = false;
  otherPhoneNumberInput: string;

  save() {
    if (this.otherPhoneNumber == true) {
      if (this.otherPhoneNumberInput != null) {
        this.training.id = 0;
        this.training.trainer_id = this.user.id;
        this.training.min_member = Number(this.min_member);
        this.training.max_member = Number(this.max_member);
        this.training.contact_phone = this.otherPhoneNumberInput;
        this.trainingService.newTraining(this.training).subscribe(
          (result) => {},
          (error) => console.log(error)
        );
      }
    } else {
      this.training.id = 0;
      this.training.trainer_id = this.user.id;
      this.training.min_member = Number(this.min_member);
      this.training.max_member = Number(this.max_member);
      this.training.contact_phone = this.user.phone_number;

      this.trainingService.newTraining(this.training).subscribe(
        (result) => {
          this.training.id = result.id;
          this.tagTraining.id = 0;
          this.tagTraining.training_id = this.training.id;
          
          for (const item of this.selectedTags) {
            for (const tag of this.tags) {
              if (item == tag.name) {
                
                this.tagTraining.tag_id = tag.id;                
              }              
            }
            console.log(this.tagTraining);
            this.tagTrainingService.newTagTraining(this.tagTraining).subscribe(
              result => {},
              error => console.log(error)
            );
          }
          this.tagTrainingService.newTagTraining(this.tagTraining);
        },
        (error) => console.log(error)
      );
    }
    //TODO errormessage and success box
    //TODO edit or create
    
    //TODO add tagtraining
  }
  phone() {
    this.otherPhoneNumber = false;
  }
  otherPhone() {
    this.otherPhoneNumber = true;
  }
  onChange(value) {
    for (const item of this.categories) {
      if (item.name == value) {
        this.training.category_id = item.id;
        this.selectedCat = item.name;
      }
    }

    
  }
  onTagChange(value) {
    if (!this.selectedTags.includes(value)) {      
      this.selectedTags.push(value);
    } else {
      const index: number = this.selectedTags.indexOf(value);
      this.selectedTags.splice(index, 1);
    }
  }
}
