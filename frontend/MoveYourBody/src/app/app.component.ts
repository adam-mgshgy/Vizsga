import { Component } from '@angular/core';
import { CategoryModel } from './models/category-model';
import { UserModel } from './models/user-model';
import { Subscription } from 'rxjs';
import { LoginService } from './services/login.service';
import { CategoriesService } from './services/categories.service';
import { AuthenticationService } from './services/authentication.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  //user: UserModel;
  //subscription: Subscription;

  user: UserModel;
  constructor(
    private router: Router,
    private loginService: LoginService,
    private categoryService: CategoriesService,
    private authenticationService: AuthenticationService,
    private jwtHelper: JwtHelperService,
    private userService: UserService
  ) {
    this.authenticationService.currentUser.subscribe(
      (x) => (this.user = x)
    );
  }
  ngOnInit() {
    // this.subscription = this.loginService.currentUser.subscribe(
    //   (user) => (this.user = user)
    // );


    console.log(this.user)
    this.categoryService.getCategories().subscribe(
      (result) => (this.categories = result),
      (error) => console.log(error)
    );
  }
  // ngOnDestroy() {
  //   this.subscription.unsubscribe();
  // }
  title = 'MoveYourBody';
  public categories: CategoryModel[] = [];

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
  
}
