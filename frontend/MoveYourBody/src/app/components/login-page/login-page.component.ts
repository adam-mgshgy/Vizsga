import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { LoginModel } from 'src/app/models/login-model';
import { UserModel } from 'src/app/models/user-model';
import { UserService } from 'src/app/services/user.service';
import { Subscription } from 'rxjs';
import { LoginService } from 'src/app/services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loginpage',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginpageComponent implements OnInit {
  constructor(private userService: UserService, private loginService: LoginService, private router: Router) { }
  user: UserModel;
  subscription: Subscription;
  // @Output() userEvent = new EventEmitter<UserModel>();
  
  email = '';
  password = '';
  link = 'login';
  ngOnInit(): void {
    this.subscription = this.loginService.currentUser.subscribe(user => this.user = user)
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  Login() {
    this.userService.Login(this.email, this.password).subscribe(
      result => {
        this.user = result;
        console.log(this.user);
        this.loginService.changeUser(this.user);
        this.link = "home";
        this.router.navigate(['/', this.link])
        console.log(this.link);
        // this.userEvent.emit(this.user);
      },
      error => console.log(error)
    );
  }
  
}
