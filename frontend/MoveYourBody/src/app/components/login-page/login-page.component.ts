import { Component, OnInit} from '@angular/core';
import { first } from 'rxjs';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-loginpage',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginpageComponent implements OnInit {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }
  user: UserModel;
  public email = '';
  public password = '';
  errorMessage = '';

  ngOnInit(): void {}

  Login() {
    if (this.email == '') {
      this.errorMessage = 'Kérem adja meg az e-mail címét!';
    } else if (this.password == '') {
      this.errorMessage = 'Kérem adja meg a jelszavát!';
    } else {
      this.authenticationService
        .login(this.email, this.password)
        .pipe(first())
        .subscribe(
          (result) => {
            this.authenticationService.currentUser.subscribe(
              (x) => (this.user = x)
            );
            this.router.navigateByUrl('/home');
          },
          (error) => {
            console.log(error);
            this.errorMessage = 'Hibás e-mail cím vagy jelszó!';
          }
        );
    }
  }
}
