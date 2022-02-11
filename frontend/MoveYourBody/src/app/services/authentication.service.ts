import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserModel } from '../models/user-model';
import { UserService } from './user.service';
import jwt_decode from 'jwt-decode'


@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<UserModel>;
  public currentUser: Observable<UserModel>;

  constructor(private http: HttpClient, private userService: UserService) {
    this.currentUserSubject = new BehaviorSubject<UserModel>(
      JSON.parse(localStorage.getItem('currentUser'))
    );

    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserModel {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
    return this.http
      .post<any>(`${environment.ApiURL}/registration/login`, {
        email,
        password,
      })
      .pipe(
        map((data) => {
          const token = data.token;
          localStorage.setItem('jwt', token);
          this.userService.getUserById(data.userId).subscribe((result) => {            
            localStorage.setItem('currentUser', JSON.stringify(result));
            this.currentUserSubject.next(result);
          });
          return data;
        })
      );
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    localStorage.removeItem('jwt');
  }

  hasRole(roleName: string): boolean {
    if (!this.currentUser) {
      return false;
    }
    const userInfo: any = jwt_decode(localStorage.getItem('jwt'));
    var roles = userInfo["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    console.log(roles)
    if (Array.isArray(roles)){
      return roles.indexOf(roleName) >= 0
    } else {
      return roles == roleName;
    }
  }
}
