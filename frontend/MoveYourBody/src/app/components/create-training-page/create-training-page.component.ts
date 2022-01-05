import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryModel } from 'src/app/models/category-model';
import { TagModel } from 'src/app/models/tag-model';
import { TagTrainingModel } from 'src/app/models/tag-training-model';
import { TrainingModel } from 'src/app/models/training-model';
import { UserModel } from 'src/app/models/user-model';
import { CategoriesService } from 'src/app/services/categories.service';
import { TagTrainingService } from 'src/app/services/tag-training.service';
import { TagService } from 'src/app/services/tag.service';
import { TrainingService } from 'src/app/services/training.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-create-training-page',
  templateUrl: './create-training-page.component.html',
  styleUrls: ['./create-training-page.component.css'],
})
export class CreateTrainingPageComponent implements OnInit {
  id: string | null = null;
  isChecked = false;

  selectedCat = '';
  public min_member: number;
  public max_member: number;
  otherPhoneNumber = false;
  otherPhoneNumberInput: string;

  create = true;

  mobile: boolean = false;

  public messageBox = '';
  public messageTitle = '';

  user: UserModel = {
    //TODO user from backend
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
  public myTrainings: TrainingModel[] = [];
  public selectedTags: string[] = [];
  public selectedTagsFix: string[] = [];
  public tagTraining: TagTrainingModel = new TagTrainingModel();
  public tagTrainingFix: TagTrainingModel[] = [];
  public deleteTag: string[] = [];
  deleteTagTrainng: TagTrainingModel = new TagTrainingModel();

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoriesService,
    private trainingService: TrainingService,
    private tagService: TagService,
    private tagTrainingService: TagTrainingService,
    private modalService: NgbModal
  ) {}

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
          this.max_member = this.training.max_member;
          this.min_member = this.training.min_member;
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
    this.errorCheck();
    if (this.create) {
      if (this.otherPhoneNumber == true) {
        if (this.otherPhoneNumberInput != null) {
          this.training.id = 0;
          this.training.trainer_id = this.user.id;
          this.training.min_member = Number(this.min_member);
          this.training.max_member = Number(this.max_member);
          this.training.contact_phone = this.otherPhoneNumberInput;
          this.trainingService.newTraining(this.training).subscribe(
            (result) => {
              if (result.category_id != null || result.category_id != 0) {
                this.messageTitle = 'Siker';
                this.messageBox = 'Edzése sikeresen létrehozva!';
              }
            },
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
            if (result.category_id != null || result.category_id != 0) {
              this.messageTitle = 'Siker';
              this.messageBox = 'Edzése sikeresen létrehozva!';
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
      this.training.min_member = Number(this.min_member);
      this.training.max_member = Number(this.max_member);
      if (this.otherPhoneNumber) {
        this.training.contact_phone = this.otherPhoneNumberInput;
      } else {
        this.training.contact_phone = this.user.phone_number;
      }
      this.trainingService.modifyTraining(this.training).subscribe(
        (result) => {
          this.messageTitle = 'Siker';
          this.messageBox = 'Edzése siekresen frissítve!';

          for (const item of this.selectedTags) {
            for (const tag of this.tags) {
              if (item == tag.name) {
                this.tagTraining.tag_id = tag.id;
              }
            }
          }
        },
        (error) => console.log(error)
      );

      let indexes: Number[] = [];
      for (const item of this.deleteTag) {
        for (const tag of this.tags) {
          if (item == tag.name) {
            indexes.push(tag.id);
            const index: number = this.selectedTags.indexOf(item);
            this.selectedTags.splice(index, 1);
          }
        }
      }

      for (const item of this.tagTrainingFix) {
        for (const index of indexes) {
          if (index == item.tag_id) {
            this.deleteTagTrainng = item;
            this.tagTrainingService
              .deleteTagTraining(this.deleteTagTrainng)
              .subscribe();
          }
        }
      }
      console.log(this.selectedTags)
      this.tagTraining.id = 0;
      this.tagTraining.training_id = this.training.id;
      console.log(this.tagTraining);

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

      console.log(this.tagTrainingFix);

      for (const item of this.selectedTags) {
        for (const tag of this.tags) {
          if (item == tag.name) {
            this.tagTraining.tag_id = tag.id;
          }
        }
        this.tagTrainingService.newTagTraining(this.tagTraining).subscribe(
          (result) => {},
          (error) => console.log(error)
        );
      }

      console.log(this.tagTraining);
    }
  }
  errorCheck() {
    this.messageTitle = 'Hiba';
    if (this.training.name == '') {
      this.messageBox = 'Kérem adja meg az edzés nevét!';
    } else if (this.training.category_id == null) {
      this.messageBox = 'Kérem adja meg az edzéshez tartozó kategóriát!';
    } else if (this.training.description == '') {
      this.messageBox =
        'Kérem adjon meg az edzéshez egy rövid tájékoztató leírást!';
    } else if (this.training.max_member == 0) {
      this.messageBox = 'Kérem adja meg az edzés résztvevőinek maximum számát!';
    } else if (this.training.min_member > this.max_member) {
      this.messageBox =
        'Az edzéshez tartozó minimum résztvevők száma nagyobb mint a maximum!'; //TODO az inputból kikattintva menti csak el a résztvevők számát
    } else if (
      this.otherPhoneNumber == true &&
      this.otherPhoneNumberInput == null
    ) {
      this.messageBox = 'Kérem adja meg a másodlagos telefonszámot!';
    }
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
      const index: number = this.selectedTags.indexOf(value);
      if (this.selectedTagsFix.includes(value)) {
        this.selectedTags.splice(index, 1);
        this.deleteTag.push(value);      
      }
    }
  }
  closeResult = '';
  open(content: any) {
    this.modalService.open(content).result.then(
      (result) => {
        this.closeResult = `Closed with: ${result}`;
      },
      (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }
  close() {
    this.modalService.dismissAll();
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
