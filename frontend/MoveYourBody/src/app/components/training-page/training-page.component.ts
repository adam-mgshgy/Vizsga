import { Component, OnInit } from '@angular/core';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { TagModel } from 'src/app/models/tag-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';
import { LocationModel } from 'src/app/models/location-model';
import { CategoriesService } from 'src/app/services/categories.service';
import { TrainingService } from 'src/app/services/training.service';
import { TagService } from 'src/app/services/tag.service';
import { TagTrainingService } from 'src/app/services/tag-training.service';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoginService } from 'src/app/services/login.service';
import { CategoryModel } from 'src/app/models/category-model';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { TrainingSessionService } from 'src/app/services/training-session.service';
import { LocationService } from 'src/app/services/location.service';

@Component({
  selector: 'app-training-page',
  templateUrl: './training-page.component.html',
  styleUrls: ['./training-page.component.css']
})
export class TrainingPageComponent implements OnInit {
  id: Number;
  user: UserModel;

  public training: TrainingModel;
  public trainer: UserModel;
  public category: CategoryModel;
  public sessions: TrainingSessionModel[];

  categories: CategoryModel[];
  locations: LocationModel[];
  tagTraining: TagTrainingModel[] = [];
  tags: TagModel[] = [];
  subscription: Subscription;
  mobile: boolean = false;
  constructor(
    private categoriesService: CategoriesService,
    private trainingService: TrainingService,
    private trainingSessionService: TrainingSessionService,
    private loginService: LoginService, 
    private tagService: TagService,
    private tagTrainingService: TagTrainingService,
    private userService: UserService,
    private route: ActivatedRoute,
    private locationService: LocationService,
  ) { }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    this.subscription = this.loginService.currentUser.subscribe(user => this.user = user)

    this.route.paramMap.subscribe((params) => {
      this.id = Number(params.get('id'));
    });
    this.trainingService.getById(this.id).subscribe(
      (result) => {
        this.training = result;
        this.userService.getUserById(this.training.trainer_id).subscribe(
          (result) => {
            this.trainer = result;
            this.trainingSessionService.listByTrainingId(this.training.id).subscribe(
              (result) => {
                this.sessions = result;
                console.log(this.sessions);
                this.locationService.getLocations().subscribe(
                  result => {
                    this.locations = result;
                  },
                  error => console.log(error)
                );
              },
              (error) => console.log(error)
            );
          },
          (error) => console.log(error)
        );
      },
      (error) => console.log(error)
    );
    
    this.categoriesService.getCategories().subscribe(
      (result) => {
        this.categories = result;
        var i = 0;
        while (this.categories[i].id != this.training.category_id && i < this.categories.length) {
          i++;
        }
        if (i < this.categories.length) {
          this.category = this.categories[i];
        }
        this.tagTrainingService.getTags(this.category.id).subscribe(
          (result) => {
            this.tagTraining = result;
            console.log(result);
          },
          (error) => console.log(error)
        );
        console.log(result)
      },
      (error) => console.log(error)
    );
    this.tagService.getTags().subscribe(
      (result) => (this.tags = result),
      (error) => console.log(error)
    );
  }
}
