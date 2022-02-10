import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  result: boolean = false;
  isSearch = false;
  selectedCounty: string;
  selectedCity: string;
  trainingName: string;

  constructor(
    private route: ActivatedRoute,
    private trainingService: TrainingService,
    private categoryService: CategoriesService,
    private tagService: TagService,
    private locationService: LocationService,
    private router: Router
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
      this.trainingName = params.get('name');
      this.selectedCounty = params.get('county');
      this.selectedCity = params.get('city');
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
      } else if (this.trainingName) {
        this.trainingService.getByName(this.trainingName).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.selectedCity) {
        this.trainingService.getByCity(this.selectedCity).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.selectedCounty) {
        this.trainingService.getByCounty(this.selectedCounty).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
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
    if (this.selectedCity) {
      this.router.navigateByUrl('trainings/city/' + this.selectedCity);
    } else {
      this.router.navigateByUrl('trainings/county/' + this.selectedCounty);
    }
  }

  CountyChanged(value) {
    for (const item of this.counties) {
      if (item.county_name == value) {
        this.selectedCounty = item.county_name;
        console.log(this.selectedCounty);
      } else if (value == item.id) {
        this.selectedCounty = item.county_name;
      }
    }
    this.locationService.getCities(this.selectedCounty).subscribe(
      (result) => (this.cities = result),
      (error) => console.log(error)
    );
  }
  CityChanged(value) {
    for (const item of this.cities) {
      if (item.city_name == value) {
        this.selectedCity = item.city_name;
        console.log(this.selectedCity);
      } else if (value == item.id) {
        this.selectedCity = item.city_name;
      }
    }
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
