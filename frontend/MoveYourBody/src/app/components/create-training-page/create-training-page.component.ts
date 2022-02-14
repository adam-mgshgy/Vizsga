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
import { TrainingImagesModel } from 'src/app/models/training-images-model';
import { ImagesModel } from 'src/app/models/images-model';

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
  
  deleteTag: string[] = [];
  deleteTagTraining: TagTrainingModel = new TagTrainingModel();
  trainingImages: TrainingImagesModel[] = [];
  Images: ImagesModel[] = [];

  tagTrainings: TagTrainingModel[] = [];

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
        this.myTrainings = result.trainings;

        this.route.paramMap.subscribe((params) => {
          this.id = params.get('id');
          if (this.id) {
            const filteredTrainings = this.myTrainings.filter(
              (t) => t.id == Number(this.id)
            );
            if (filteredTrainings.length == 1) {
              this.training = filteredTrainings[0];
              this.tagTrainingService.getByTraining(this.training.id).subscribe(
                (result) => this.tagTrainings = result,
                (error) => console.log(error)
              );
            } else {
              this.training = new TrainingModel();
            }
          } else {
            this.training = new TrainingModel();
          }
        });
        if (
          this.training.contact_phone != this.user.phone_number &&
          this.training.contact_phone != ''
        ) {
          this.otherPhoneNumber = true;
          this.otherPhoneNumberInput = this.training.contact_phone;
        }
        if (this.training.id != null) {
          this.create = false;
        }
        if (!this.create) {
          this.trainingService.getImageById(this.training.id).subscribe(
            (result) => {
              for (const item of result.trainingImages) {
                this.trainingImages.push(item);
                console.log(this.trainingImages);
              }
              for (const item of result.images) {
                this.Images.push(item);
              }
            },
            (error) => console.log(error)
          );
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

  images: string[] = [];
  imageError: string;
  isImageSaved: boolean;
  cardImageBase64: string;
  fileChangeEvent(fileInput: any) {
    //TODO max image size, input text change
    this.imageError = null;
    if (fileInput.target.files && fileInput.target.files[0]) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const imgBase64Path = e.target.result;
        this.cardImageBase64 = imgBase64Path;
        this.isImageSaved = true;
        this.images.push(this.cardImageBase64);
      };

      reader.readAsDataURL(fileInput.target.files[0]);
    }
    return false;
  }
  selectIndexImage() {
    if (this.selectIndex.length == 1) {
      console.log(this.selectIndex);
      this.training.indexImageId = this.selectIndex[0];
      console.log(this.training);
      this.trainingService.modifyTraining(this.training).subscribe(
        (result) => {
          this.training = result;
          this.selectIndex = [];
        },
        (error) => console.log(error)
      );
    }
  }

  selectNewIndex: number[] = [];
  selectIndex: number[] = [];

  delete: string[] = [];
  selectedNewImage(i: number) {
    //Új képek kiválasztása
    if (this.selectNewIndex.includes(i)) {
      this.selectNewIndex.splice(this.selectNewIndex.indexOf(i), 1);
    } else {
      this.selectNewIndex.push(i);
    }
  }
  selectedImage(i: number) {
    //Adatbazisbol betoltott képek kiválasztása
    if (this.selectIndex.includes(i)) {
      this.selectIndex.splice(this.selectIndex.indexOf(i), 1);
    } else {
      this.selectIndex.push(i);
    }
  }
  deleteImage() {
    for (const index of this.selectNewIndex) {
      this.images.splice(index, 1);
      this.selectNewIndex.splice(this.selectNewIndex.indexOf(index), 1);
    }

    this.trainingService.deleteImage(this.selectIndex).subscribe(
      (result) => {
        this.selectIndex = [];
        this.trainingService.getById(this.training.id).subscribe(
          (result) => {
            this.training = result;
          },
          (error) => console.log(error)
        );
        this.trainingService.getImageById(this.training.id).subscribe(
          (result) => {
            this.trainingImages = result.trainingImages;
            this.Images = result.images;
          },
          (error) => console.log(error)
        );
      },
      (error) => console.log(error)
    );

    if (this.images.length < 1) {
      this.cardImageBase64 = null;
      this.isImageSaved = false;
    }
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
                this.trainingService
                  .saveImage(this.images, result.id)
                  .subscribe(
                    (result) => console.log(result),
                    (error) => console.log(error)
                  );
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
              this.trainingService.saveImage(this.images, result.id).subscribe(
                (result) => console.log(result),
                (error) => console.log(error)
              );
              if (result.category_id != null || result.category_id != 0) {
                this.errorMessage = 'Edzése sikeresen létrehozva!';
                this.router.navigateByUrl('/mytrainings');
              }

              this.training.id = result.id;
              //
            },
            (error) => console.log(error)
          );
        }
      } else {
        this.trainingService.saveImage(this.images, this.training.id).subscribe(
          (result) => console.log(result),
          (error) => console.log(error)
        );
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
        
      }
    }
  }

  refreshTags() {
   
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
    for (const item of this.tagTrainings) {
      if (value == item.tag_id) {
        return true;
      }
      
    }
   return false
  }

  onTagChange(value) {
    
    
  }
}
