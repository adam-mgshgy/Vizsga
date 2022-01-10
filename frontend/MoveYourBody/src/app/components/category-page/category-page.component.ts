import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResizedEvent } from 'angular-resize-event';
import { map, shareReplay } from 'rxjs';
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
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.css'],
})
export class CategoryPageComponent implements OnInit {
  category_name: string | null = null;
  category_id: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private categoriesService: CategoriesService,
    private trainingService: TrainingService,
    private tagService: TagService,
    private tagTrainingService: TagTrainingService
  ) {}
  imgSrc = './assets/images/categoryPageImages/profile_rock.png';
  imgBckgSrc = './assets/images/categoryPageImages/index.jpg';

  mobile: boolean = false;

  counter = 0;
  index: number = 4;
  tagCounter = [];  
  public categories: CategoryModel[] = [];
  public trainings: TrainingModel[] = [];
  public allTrainings: TrainingModel[] = [];
  public users: UserModel[] = [
    {
      id: 1,
      email: 'tesztelek@gmail.com',
      full_name: 'Tesztelek Károlyné Elekfalvi Károly',
      trainer: true,
      password: 'pwd',
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 2,
      email: 'tesztelek@gmail.com',
      full_name: 'Tóth Sándor',
      trainer: true,
      password: 'pwd',
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 3,
      email: 'tesztelek@gmail.com',
      full_name: 'Kandisz Nóra',
      trainer: true,
      password: 'pwd',
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 4,
      email: 'tesztelek@gmail.com',
      full_name: 'Kovács Ákos',
      trainer: true,
      phone_number: '+36701234678',
      password: 'pwd',
      location_id: 1,
    },
    {
      id: 5,
      email: 'tesztelek@gmail.com',
      full_name: 'Futty Imre',
      trainer: true,
      password: 'pwd',
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 6,
      email: 'tesztelek@gmail.com',
      password: 'pwd',
      full_name: 'Mittomen Karoly',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 7,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      password: 'pwd',
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 8,
      password: 'pwd',
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 9,
      password: 'pwd',
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 10,
      password: 'pwd',
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      phone_number: '+36701234678',
      location_id: 1,
    },
    {
      id: 11,
      email: 'tesztelek@gmail.com',
      full_name: 'Teszt Elek',
      trainer: true,
      password: 'pwd',
      phone_number: '+36701234678',
      location_id: 1,
    },
  ];
  public tagTraining: TagTrainingModel[] = [];
  public tags: TagModel[] = [];
  public selectedTags: TagModel[] = [];
  public locations: LocationModel[] = [
    {
      id: 1,
      county_name: 'Komárom-Esztergom megye',
      city_name: 'Bana',
      address_name: 'Kis Károly utca 11.',
    },
    {
      id: 2,
      county_name: 'Komárom-Esztergom megye',
      city_name: 'Bana',
      address_name: 'Kis Károly utca 12.',
    },
  ];

  ngOnInit(): void {
    this.upload();

    if (window.innerWidth <= 991) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.tagsOnMobile();
    console.log(this.index);
  }
  upload() {
    this.categoriesService.getCategories().subscribe(
      (result) => {
        this.categories = result;
        this.route.paramMap.subscribe((params) => {
          this.category_name = params.get('category');
          for (const item of this.categories) {
            if (item.name == this.category_name) {
              this.category_id = item.id;
            }
          }
          //

          this.tagTrainingService.getTags(this.category_id).subscribe(
            (result) => {
              this.tagTraining = result;
            },
            (error) => console.log(error)
          );

          this.trainingService.getByCategory(this.category_id).subscribe(
            (result) => {
              this.trainings = result;
              for (const item of this.trainings) {
                this.tagCounter.push({
                  training_id: item.id,
                  count: 0,
              });
              }

              console.log(this.tagCounter[0].count)
              // this.trainings = this.allTrainings.filter(
              //   (t) => t.category_id == this.category_id
              // );
              // for (const item of this.trainings) {
              //   this.tagTrainingService.getByTraining(item.id).subscribe(
              //     (result) => {
              //       //this.tagTraining = result;
              //       console.log(this.tagTraining);
              //       for (const tag of this.tags) {
              //         for (const tagTr of this.tagTraining) {
              //           if (tagTr.tag_id == tag.id) {
              //             this.selectedTags.push(tag);
              //           }
              //         }
              //       }
              //     },
              //     (error) => console.log(error)
              //   );
              // }
            },
            (error) => console.log(error)
          );
        });
      },

      (error) => console.log(error)
    );
    this.tagService.getTags().subscribe(
      (result) => (this.tags = result),
      (error) => console.log(error)
    );
  }
  actualtags: TagModel[] = [];
  actualTags(training_id: number) {}
  onResized(event: ResizedEvent) {
    this.tagsOnMobile();
  }
  tagsOnMobile() {
    if (window.innerWidth < 2250 && window.innerWidth > 1950) {//5
      this.tagCounter[0].count = 5;
    } else if (window.innerWidth <= 1950 && window.innerWidth > 1700) {//4
      this.tagCounter[0].count = 5;

    } else if (window.innerWidth <= 1700 && window.innerWidth > 1399) {//3
      this.tagCounter[0].count = 5;

    } else if (window.innerWidth <= 1399 && window.innerWidth > 1300) {//5
      this.tagCounter[0].count = 5;

    } else if (window.innerWidth <= 1300 && window.innerWidth > 1150) {//4
      this.tagCounter[0].count = 5;

    } else if (window.innerWidth <= 1150 && window.innerWidth > 950) {//3
      this.tagCounter[0].count = 12;
      this.tagCounter[1].count = 4;
//kivonni az elozo traininghez tartozo tageket

    } else if (window.innerWidth <= 950 && window.innerWidth > 767) {//2
      this.tagCounter[0].count = 5;

    } else if (window.innerWidth <= 767 && window.innerWidth > 650) {//5
      this.tagCounter[0].count = 5;

    } else if (window.innerWidth <= 650 && window.innerWidth > 580) {//4
      this.tagCounter[0].count = 5;

    } else if (window.innerWidth <= 580 && window.innerWidth > 480) {//3
      this.tagCounter[0].count = 5;

    } else if (window.innerWidth <= 480) {//2
      this.tagCounter[0].count = 5;

    } else {
      this.tagCounter[0].count = 5;

    }
  }
}
