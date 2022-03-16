import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriesService } from 'src/app/services/categories.service';
import { TagService } from 'src/app/services/tag.service';
import { TrainingService } from 'src/app/services/training.service';
import { LocationService } from 'src/app/services/location.service';
import { UserService } from 'src/app/services/user.service';
import { CategoryModel } from 'src/app/models/category-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationModel } from 'src/app/models/location-model';
import { ImagesModel } from 'src/app/models/images-model';
@Component({
  selector: 'app-trainings',
  templateUrl: './trainings-page.component.html',
  styleUrls: ['./trainings-page.component.css'],
})
export class TrainingsPageComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private trainingService: TrainingService,
    private categoryService: CategoriesService,
    private tagService: TagService,
    private locationService: LocationService,
    private router: Router,
    private userService: UserService
  ) {}

  categoryId: number;
  categoryName: string;
  tagId: number;
  tagName: string;
  trainerId: number;

  cities: LocationModel[] = [];
  counties: LocationModel[] = [];

  trainings: TrainingModel[] = [];
  categories: CategoryModel[] = [];
  trainer: UserModel;
  trainers: UserModel[] = [];
  tags: TagModel[] = [];
  tagTraining: TagTrainingModel[] = [];

  defaultProfile = './assets/images/defaultImages/defaultProfilePicture.png';
  defaultTraining = './assets/images/mainPageImages/logo.png';

  mobile: boolean = false;
  result: boolean = false;
  isSearch = false;
  selectedCounty: string;
  selectedCity: string;
  trainingName: string;
  locationSearch: boolean = false;
  profileImages: ImagesModel[] = [];
  indexImages: ImagesModel[] = [];

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(
      (result) => (this.categories = result.categories),
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
    this.locationSearch = false;

    this.route.paramMap.subscribe((params) => {
      this.categoryId = Number(params.get('category'));
      this.tagId = Number(params.get('tag'));
      this.trainerId = Number(params.get('trainer'));
      this.trainingName = params.get('name');
      this.selectedCounty = params.get('county');
      this.selectedCity = params.get('city');
      if (this.categoryId) {
        this.profileImages = [];
        this.indexImages = [];
        this.locationSearch = false;
        this.trainingService.getByCategory(this.categoryId).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.categoryName = result.category.name;
            console.log(this.trainers);
            for (const item of this.trainers) {
              this.userService.getImageById(item.image_id).subscribe(
                (result) => {
                  if (result != null) {
                    this.profileImages.push(result);
                  }
                },
                (error) => console.log(error)
              );
            }
            console.log(this.trainings);
            for (const training of this.trainings) {
              this.trainingService.getImageById(training.id).subscribe(
                (result) => {
                  console.log(result);
                  for (const item of result.images) {
                    if (item.id == training.index_image_id) {
                      this.indexImages.push(item);
                    }
                  }
                  console.log(this.indexImages);
                },
                (error) => console.log(error)
              );
            }
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.tagId) {
        this.profileImages = [];
        this.indexImages = [];
        this.locationSearch = false;
        this.trainingService.getByTag(this.tagId).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.tagName = result.tag.name;
            for (const item of this.trainers) {
              this.userService.getImageById(item.image_id).subscribe(
                (result) => {
                  console.log(result);
                  if (result != null) {
                    this.profileImages.push(result);
                  }
                  console.log(this.profileImages);
                },
                (error) => console.log(error)
              );
            }
            for (const training of this.trainings) {
              this.trainingService.getImageById(training.id).subscribe(
                (result) => {
                  console.log(result);
                  for (const item of result.images) {
                    if (item.id == training.index_image_id) {
                      this.indexImages.push(item);
                    }
                  }
                  console.log(this.indexImages);
                },
                (error) => console.log(error)
              );
            }
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.trainerId) {
        this.locationSearch = false;
        this.profileImages = [];
        this.indexImages = [];
        this.trainingService.getByTrainerId(this.trainerId).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainer = result.trainer;
            this.trainers.push(this.trainer);
            for (const item of this.trainers) {
              this.userService.getImageById(item.image_id).subscribe(
                (result) => {
                  console.log(result);
                  if (result != null) {
                    this.profileImages.push(result);
                  }
                },
                (error) => console.log(error)
              );
            }
            for (const training of this.trainings) {
              this.trainingService.getImageById(training.id).subscribe(
                (result) => {
                  for (const item of result.images) {
                    if (item.id == training.index_image_id) {
                      this.indexImages.push(item);
                    }
                  }
                },
                (error) => console.log(error)
              );
            }
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.trainingName) {
        this.profileImages = [];
        this.indexImages = [];
        this.locationSearch = false;

        this.trainingService.getByName(this.trainingName).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            for (const item of this.trainers) {
              this.userService.getImageById(item.image_id).subscribe(
                (result) => {
                  if (result != null) {
                    this.profileImages.push(result);
                  }
                },
                (error) => console.log(error)
              );
            }
            for (const training of this.trainings) {
              this.trainingService.getImageById(training.id).subscribe(
                (result) => {
                  console.log(result);
                  for (const item of result.images) {
                    if (item.id == training.index_image_id) {
                      this.indexImages.push(item);
                    }
                  }
                },
                (error) => console.log(error)
              );
            }
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.selectedCity) {
        this.profileImages = [];
        this.indexImages = [];
        this.trainingService.getByCity(this.selectedCity).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.locationSearch = true;
            for (const item of this.trainers) {
              this.userService.getImageById(item.image_id).subscribe(
                (result) => {
                  console.log(result);
                  if (result != null) {
                    this.profileImages.push(result);
                  }
                },
                (error) => console.log(error)
              );
            }
            for (const training of this.trainings) {
              this.trainingService.getImageById(training.id).subscribe(
                (result) => {
                  console.log(result);
                  for (const item of result.images) {
                    if (item.id == training.index_image_id) {
                      this.indexImages.push(item);
                    }
                  }
                },
                (error) => console.log(error)
              );
            }
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else if (this.selectedCounty) {
        this.profileImages = [];
        this.indexImages = [];
        this.locationSearch = true;

        this.trainingService.getByCounty(this.selectedCounty).subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            for (const item of this.trainers) {
              this.userService.getImageById(item.image_id).subscribe(
                (result) => {
                  console.log(result);
                  if (result != null) {
                    this.profileImages.push(result);
                  }
                },
                (error) => console.log(error)
              );
            }
            for (const training of this.trainings) {
              this.trainingService.getImageById(training.id).subscribe(
                (result) => {
                  console.log(result);
                  for (const item of result.images) {
                    if (item.id == training.index_image_id) {
                      this.indexImages.push(item);
                    }
                  }
                },
                (error) => console.log(error)
              );
            }
            this.tagTraining = result.tagTrainings;
          },
          (error) => console.log(error)
        );
      } else {
        this.trainingService.getAll().subscribe(
          (result) => {
            this.trainings = result.trainings;
            this.trainers = result.trainers;
            this.locationSearch = false;

            for (const item of this.trainers) {
              this.userService.getImageById(item.image_id).subscribe(
                (result) => {
                  if (result != null) {
                    this.profileImages.push(result);
                  }
                },
                (error) => console.log(error)
              );
            }
            for (const training of this.trainings) {
              this.trainingService.getImageById(training.id).subscribe(
                (result) => {
                  for (const item of result.images) {
                    if (item.id == training.index_image_id) {
                      this.indexImages.push(item);
                    }
                  }
                },
                (error) => console.log(error)
              );
            }
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
      } else if (value == item.id) {
        this.selectedCounty = item.county_name;
      }
    }
    this.locationService.getCities(this.selectedCounty).subscribe(
      (result) => (this.cities = result),
      (error) => console.log(error)
    );
    this.locationSearch = false;
  }
  CityChanged(value) {
    for (const item of this.cities) {
      if (item.city_name == value) {
        this.selectedCity = item.city_name;
      } else if (value == item.id) {
        this.selectedCity = item.city_name;
      }
    }
    this.locationSearch = false;
  }
}
