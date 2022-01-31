import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryModel } from 'src/app/models/category-model';
import { TagModel } from 'src/app/models/tag-model';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';
import { CategoriesService } from 'src/app/services/categories.service';
import { TagTrainingService } from 'src/app/services/tag-training.service';
import { TagService } from 'src/app/services/tag.service';
import { TrainingService } from 'src/app/services/training.service';
import { LoginService } from 'src/app/services/login.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-create-training-page',
  templateUrl: './create-training-page.component.html',
  styleUrls: ['./create-training-page.component.css'],
})
export class CreateTrainingPageComponent implements OnInit {
  id: string | null = null;
  isChecked = false;

  selectedCat = '';
  otherPhoneNumber = false;
  otherPhoneNumberInput: string;

  create = true;

  mobile: boolean = false;

  errorMessage = '';

  user: UserModel;

  categories: CategoryModel[] = [];
  tags: TagModel[] = [];
  training: TrainingModel = new TrainingModel();
  myTrainings: TrainingModel[] = [];
  selectedTags: string[] = [];
  selectedTagsFix: string[] = [];
  tagTraining: TagTrainingModel = new TagTrainingModel();
  tagTrainingFix: TagTrainingModel[] = [];
  deleteTag: string[] = [];
  deleteTagTraining: TagTrainingModel = new TagTrainingModel();

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoriesService,
    private trainingService: TrainingService,
    private tagService: TagService,
    private tagTrainingService: TagTrainingService,
    private authenticationService: AuthenticationService,
    private router: Router

  ) {
    this.authenticationService.currentUser.subscribe((x) => (this.user = x));
  }

  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);

    this.trainingService.getByTrainerId(this.user.id).subscribe(
      (result) => {
        this.myTrainings = result;

        this.route.paramMap.subscribe((params) => {
          this.id = params.get('id');
          if (this.id) {
            const filteredTrainings = this.myTrainings.filter(
              (t) => t.id == Number(this.id)
            );
            if (filteredTrainings.length == 1) {
              this.training = filteredTrainings[0];
            } else {
              this.training = new TrainingModel();
            }
          } else {
            this.training = new TrainingModel();
          }
        });

        this.tagTrainingService.getByTraining(this.training.id).subscribe(
          (tagtraining) => {
            this.tagTrainingFix = tagtraining;
            for (const item of tagtraining) {
              for (const tag of this.tags) {
                if (tag.id == item.tag_id) {
                  this.selectedTags.push(tag.name);
                  this.selectedTagsFix.push(tag.name);
                }
              }
            }
          },
          (error) => console.log(error)
        );
        if (
          this.training.contact_phone != this.user.phone_number &&
          this.training.contact_phone != ''
        ) {
          this.otherPhoneNumber = true;
          this.otherPhoneNumberInput = this.training.contact_phone;
        }
        if (this.training.id != null) {
          console.log(this.training);
          this.create = false;
        }
      },
      (error) => console.log(error)
    );

    this.categoryService.getCategories().subscribe(
      (result) => {
        this.categories = result;
        this.selectCategory(this.training.category_id);
      },
      (error) => console.log(error)
    );

    this.tagService.getTags().subscribe(
      (result) => (this.tags = result),
      (error) => console.log(error)
    );
  }
  save() {
    if (this.errorCheck()) {
      if (this.create) {
        if (this.otherPhoneNumber == true) {
          if (this.otherPhoneNumberInput != null) {
            this.training.id = 0;
            this.training.trainer_id = this.user.id;
            this.training.contact_phone = this.otherPhoneNumberInput;
            this.trainingService.newTraining(this.training).subscribe(
              (result) => {
                if (result.category_id != null || result.category_id != 0) {
                  this.errorMessage = 'Edzése sikeresen létrehozva!';
                this.router.navigateByUrl('/mytrainings');

                }
              },
              (error) => console.log(error)
            );
          }
        } else {
          this.training.id = 0;
          this.training.trainer_id = this.user.id;
          this.training.contact_phone = this.user.phone_number;

          this.trainingService.newTraining(this.training).subscribe(
            (result) => {
              if (result.category_id != null || result.category_id != 0) {
                this.errorMessage = 'Edzése sikeresen létrehozva!';
                this.router.navigateByUrl('/mytrainings');

              }

              this.training.id = result.id;
              this.tagTraining.id = 0;
              this.tagTraining.training_id = this.training.id;

              for (const item of this.selectedTags) {
                for (const tag of this.tags) {
                  if (item == tag.name) {
                    this.tagTraining.tag_id = tag.id;
                  }
                }

                this.tagTrainingService
                  .newTagTraining(this.tagTraining)
                  .subscribe(
                    (result) => {},
                    (error) => console.log(error)
                  );
              }
            },
            (error) => console.log(error)
          );
        }
      } else {
        if (this.otherPhoneNumber) {
          this.training.contact_phone = this.otherPhoneNumberInput;
        } else {
          this.training.contact_phone = this.user.phone_number;
        }
        this.trainingService.modifyTraining(this.training).subscribe(
          (result) => {
            this.errorMessage = 'Edzése sikeresen frissítve!';
            this.router.navigateByUrl('/mytrainings');

          },
          (error) => console.log(error)
        );
        //TAGTRAINING
        let indexes: Number[] = [];
        for (const item of this.deleteTag) {
          for (const tag of this.tags) {
            if (item == tag.name) {
              indexes.push(tag.id);
              let index: number;
              for (const selectedTag of this.selectedTags) {
                if (selectedTag == item) {
                  index = this.selectedTags[item];
                }
              }
              this.selectedTags.splice(index, 1);
            }
          }
        }
        for (const item of indexes) {
          let tagTr = new TagTrainingModel();
          tagTr = {
            id: 0,
            tag_id: Number(item),
            training_id: this.training.id,
          };
          console.log(tagTr);
          this.tagTrainingService.deleteTagTraining(tagTr).subscribe(
            (result) => {
              this.deleteTag = [];
              this.tagTrainingService.getByTraining(this.training.id).subscribe(
                (tagtraining) => {
                  this.deleteTag = [];
                  this.selectedTags = [];
                  this.selectedTagsFix = [];
                  this.tagTrainingFix = tagtraining;
                  for (const item of tagtraining) {
                    for (const tag of this.tags) {
                      if (
                        tag.id == item.tag_id &&
                        !this.selectedTags.includes(tag.name)
                      ) {
                        this.selectedTags.push(tag.name);
                        this.selectedTagsFix.push(tag.name);
                      }
                    }
                  }
                },
                (error) => console.log(error)
              );
            },
            (error) => console.log(error)
          );
        }

        this.tagTrainingService.getByTraining(this.training.id).subscribe(
          (tagtraining) => {
            this.selectedTagsFix = [];
            this.tagTrainingFix = tagtraining;
            for (const item of tagtraining) {
              for (const tag of this.tags) {
                if (tag.id == item.tag_id) {
                  this.selectedTagsFix.push(tag.name);
                }
              }
            }
          },
          (error) => console.log(error)
        );

        for (const item of this.selectedTags) {
          let tagTr = new TagTrainingModel();
          tagTr.id = 0;
          tagTr.training_id = this.training.id;
          for (const tag of this.tags) {
            if (item == tag.name) {
              tagTr.tag_id = tag.id;
              this.tagTrainingService.newTagTraining(tagTr).subscribe(
                (result) => {
                  this.refreshTags();
                },
                (error) => console.log(error)
              );
            }
          }
        }
      }
    }
  }

  refreshTags() {
    this.tagTrainingService.getByTraining(this.training.id).subscribe(
      (tagtraining) => {
        this.deleteTag = [];
        this.selectedTags = [];
        this.selectedTagsFix = [];
        this.tagTrainingFix = tagtraining;
        for (const item of tagtraining) {
          for (const tag of this.tags) {
            if (tag.id == item.tag_id) {
              this.selectedTags.push(tag.name);
              this.selectedTagsFix.push(tag.name);
            }
          }
        }
      },
      (error) => console.log(error)
    );
  }

  errorCheck(): boolean {
    if (this.training.name == '') {
      this.errorMessage = 'Kérem adja meg az edzés nevét!';
      return false;
    }
    if (this.training.category_id == null) {
      this.errorMessage = 'Kérem adja meg az edzéshez tartozó kategóriát!';
      return false;
    }
    if (this.training.description == '') {
      this.errorMessage =
        'Kérem adjon meg az edzéshez egy rövid tájékoztató leírást!';
      return false;
    }
    if (this.otherPhoneNumber == true && this.otherPhoneNumberInput == null) {
      this.errorMessage = 'Kérem adja meg a másodlagos telefonszámot!';
      return false;
    }
    return true;
  }
  phone() {
    this.otherPhoneNumber = false;
  }
  otherPhone() {
    this.otherPhoneNumber = true;
  }
  selectCategory(value) {
    for (const item of this.categories) {
      if (item.name == value) {
        this.training.category_id = item.id;
        this.selectedCat = item.name;
      } else if (value == '') {
        this.training.category_id = null;
      } else if (value == item.id) {
        this.selectedCat = item.name;
      }
    }
  }
  checkTags(value) {
    if (this.selectedTags.includes(value)) {
      return true;
    }
    return false;
  }

  onTagChange(value) {
    if (!this.selectedTags.includes(value)) {
      this.selectedTags.push(value);
      if (this.selectedTagsFix.includes(value)) {
        const index: number = this.deleteTag.indexOf(value);
        this.deleteTag.splice(index, 1);
      }
    } else {
      console.log(this.selectedTags.indexOf(value));
      const index: number = this.selectedTags.indexOf(value);
      if (this.selectedTagsFix.includes(value)) {
        this.deleteTag.push(value);
      }
      if (this.selectedTags.includes(value)) {
        this.selectedTags.splice(index, 1);
      }
    }
  }
}
