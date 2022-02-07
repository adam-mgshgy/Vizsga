import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryModel } from 'src/app/models/category-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { CategoriesService } from 'src/app/services/categories.service';
import { TagService } from 'src/app/services/tag.service';
import { TrainingService } from 'src/app/services/training.service';
import { ResizedEvent } from 'angular-resize-event';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationService } from 'src/app/services/location.service';
import { LocationModel } from 'src/app/models/location-model';
@Component({
  selector: 'app-trainings',
  templateUrl: './trainings.component.html',
  styleUrls: ['./trainings.component.css'],
})
export class TrainingsComponent implements OnInit {
  categoryId: number;
  tagId: number;
  trainerId: number;
  
  cities: LocationModel[] = [];
  counties: LocationModel[] = [];

  trainings: TrainingModel[] = [];
  categories: CategoryModel[] = [];
  trainer: UserModel;
  trainers: UserModel[] = [];
  tags: TagModel[] = [];
  tagTraining: TagTrainingModel[] = [];

  imgSrc = './assets/images/categoryPageImages/profile_rock.png';
  imgBckgSrc = './assets/images/categoryPageImages/index.jpg';

  mobile: boolean = false;
  isSearchClicked: boolean = true;
  result: boolean = false;

  selectedTags: TagModel[] = [];
  selectedCategories: CategoryModel[] = [];
  selectedTrainers: UserModel[] = [];
  selectedCounty: string;
  selectedCity: string;
  trainerName: '';
  trainingName: '';

  constructor(
    private route: ActivatedRoute,
    private trainingService: TrainingService,
    private categoryService: CategoriesService,
    private tagService: TagService,
    private locationService: LocationService
  ) {}

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(
      (result) => (this.categories = result),
      (error) => console.log(error)
    );
    this.tagService.getTags().subscribe(
      (result) => (this.tags = result),
      (error) => console.log(error)
    );
    this.locationService.getCounties().subscribe(
      (result) => {
        this.counties = result;
      },
      (error) => console.log(error)
    );
    this.route.paramMap.subscribe((params) => {
      this.categoryId = Number(params.get('category'));
      this.tagId = Number(params.get('tag'));
      this.trainerId = Number(params.get('trainer'));
      if (this.categoryId) {
        this.trainingService.getByCategory(this.categoryId).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.tagId) {
        this.trainingService.getByTag(this.tagId).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.trainerId) {
        this.trainingService.getByTrainerId(this.trainerId).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainer = result.trainer;
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else {
        this.trainingService.getAll().subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      }
    });
  }
  Search() {

  }
  Cancel() {
    this.selectedTags= [];
    this.selectedCategories = [];
    this.selectedTrainers = [];
    this.selectedCounty = '';
    this.selectedCity = '';
    this.trainerName = '';
    this.trainingName = '';
  }
  CountyChanged(value) {
    this.locationService.getCities(this.selectedCounty).subscribe(
      (result) => (this.cities = result),
      (error) => console.log(error)
    );
  }

  actualtags: TagModel[] = [];
  actualTags(training_id: number) {}
  onResized(event: ResizedEvent) {
    this.tagsOnMobile();
  }
  tagsOnMobile() {
    if (window.innerWidth < 2250 && window.innerWidth > 1950) {
      //5
    } else if (window.innerWidth <= 1950 && window.innerWidth > 1700) {
      //4
    } else if (window.innerWidth <= 1700 && window.innerWidth > 1399) {
      //3
    } else if (window.innerWidth <= 1399 && window.innerWidth > 1300) {
      //5
    } else if (window.innerWidth <= 1300 && window.innerWidth > 1150) {
      //4
    } else if (window.innerWidth <= 1150 && window.innerWidth > 950) {
      //3
      //kivonni az elozo traininghez tartozo tageket
    } else if (window.innerWidth <= 950 && window.innerWidth > 767) {
      //2
    } else if (window.innerWidth <= 767 && window.innerWidth > 650) {
      //5
    } else if (window.innerWidth <= 650 && window.innerWidth > 580) {
      //4
    } else if (window.innerWidth <= 580 && window.innerWidth > 480) {
      //3
    } else if (window.innerWidth <= 480) {
      //2
    } else {
    }
  }
}
