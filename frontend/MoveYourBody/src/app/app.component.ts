import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthenticationService } from './services/authentication.service';
import { TagService } from './services/tag.service';
import { CategoriesService } from './services/categories.service';
import { TagModel } from './models/tag-model';
import { CategoryModel } from './models/category-model';
import { UserModel } from './models/user-model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  constructor(
    private router: Router,
    private categoryService: CategoriesService,
    private authenticationService: AuthenticationService,
    private jwtHelper: JwtHelperService,
    private tagService: TagService,
  ) {
    this.authenticationService.currentUser.subscribe(
      (x) => (this.user = x)
    );
  }
  mobile: boolean = false;
  title = 'MoveYourBody';
  categories: CategoryModel[] = [];
  tags: TagModel[] = [];
  user: UserModel;
  trainingName: string;
  ngOnInit() {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
    
    if (this.user) {
      this.categoryService.getCategories().subscribe(
        (result) => (this.categories = result.categories),
        (error) => console.log(error)
      );
      this.tagService.getTags().subscribe(
        (result) => (this.tags = result),
        (error) => console.log(error)
      )
      
    }
  }

  isUserAuthenticated() {
    const token: string = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

  Logout(){
    this.authenticationService.logout();
        this.router.navigate(['/login']);
  }

  Search() {
    this.router.navigateByUrl('/trainings/name/' + this.trainingName);
  }
  
}
