import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private user = new BehaviorSubject<UserModel>(new UserModel());
  currentUser = this.user.asObservable();
  constructor() { }

  changeUser(user: UserModel) {
    this.user.next(user)
  }

}
