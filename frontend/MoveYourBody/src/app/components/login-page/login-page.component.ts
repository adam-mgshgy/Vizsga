import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { LoginModel } from 'src/app/models/login-model';
import { UserModel } from 'src/app/models/user-model';
import { UserService } from 'src/app/services/user.service';
import { first, Subscription } from 'rxjs';
import { LoginService } from 'src/app/services/login.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-loginpage',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginpageComponent implements OnInit {
  // constructor(private userService: UserService, private loginService: LoginService, private router: Router) { }
  user: UserModel;
  // subscription: Subscription;
  // @Output() userEvent = new EventEmitter<UserModel>();

  email = '';
  password = '';
  link = 'login';
  // ngOnInit(): void {
  //   this.subscription = this.loginService.currentUser.subscribe(user => this.user = user)
  // }
  // ngOnDestroy() {
  //   this.subscription.unsubscribe();
  // }
  // Login() {
  //   this.userService.Login(this.email, this.password).subscribe(
  //     result => {
  //       this.user = result;
  //       console.log(this.user);
  //       this.loginService.changeUser(this.user);
  //       this.link = "home";
  //       this.router.navigate(['/', this.link])
  //       console.log(this.link);
  //       //this.userEvent.emit(this.user);
  //     },
  //     error => console.log(error)
  //   );
  // }
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    // redirect to home if already logged in
    // if (this.authenticationService.currentUserValue) {
    //   this.router.navigate(['/']);
    // }
  }

  ngOnInit():void {    
  }

  Login() {
    console.log(this.email)
    this.email = "string",
    this.password = "string",//todo Visszaadni backendrol a usert

    this.authenticationService
      .login(this.email, this.password)
      .pipe(first())
      .subscribe(
        (result) => {
          this.authenticationService.currentUser.subscribe(
            (x) => (this.user = x)
          );          
          console.log(this.user)
        },
        (error) => {
          console.log(error);
         
        }
      );
  }
}
