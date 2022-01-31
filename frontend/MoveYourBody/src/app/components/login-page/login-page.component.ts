import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserModel } from 'src/app/models/user-model';
import { first } from 'rxjs';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-loginpage',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginpageComponent implements OnInit {
  user: UserModel;

  constructor(
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
  errorMessage = '';
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
          this.router.navigateByUrl('/home');
          console.log(this.user);
        },
        (error) => {
          console.log(error);
          this.errorMessage = error;
        }
      );
  }
}
