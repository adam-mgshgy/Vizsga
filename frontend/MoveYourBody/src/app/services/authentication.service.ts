import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable} from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserModel } from '../models/user-model';
import { UserService } from './user.service';

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
    this.currentUser.subscribe(result => console.log(result));
  }

  public get currentUserValue(): UserModel {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
    return this.http
      .post<any>(`${environment.ApiURL}/registration/login`, {email,password})
      .pipe(
        map((data) => {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          const token = data.token;
          localStorage.setItem("jwt", token);
          console.log(token);
          this.userService.getUserById(data.userId).subscribe(
            result => {
              localStorage.setItem('currentUser', JSON.stringify(result));
              
              this.currentUserSubject.next(result);
          }
          );
                    
          return data;
        })
      );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    localStorage.removeItem("jwt");
  }
}
