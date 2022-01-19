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
  user: UserModel;

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

  ngOnInit(): void {
    this.Login();
  }
  public email = '';
  public password = '';
  link = 'login';

  Login() {    
    this.authenticationService
      .login(this.email, this.password)
      .pipe(first())
      .subscribe(
        (result) => {
          this.authenticationService.currentUser.subscribe(
            (x) => (this.user = x)
          );
          console.log(this.user);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
