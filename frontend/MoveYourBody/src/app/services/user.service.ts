import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from '../models/user-model';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { LoginModel } from '../models/login-model';



@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  Register(model: UserModel): Observable<UserModel> {
    return this.http.put<UserModel>(`${environment.ApiURL}/user/register`, model).pipe(
      map((data: UserModel) => {
        return data;
      }),
      catchError((err) => {
        if (
          !environment.production &&
          (err.status == 404 || err.status == 405)
        ) {         
          return of(model);
        } else throw err;
      })
    );
  }
  getUserById(id: any): Observable<UserModel> {
    return this.http.get<UserModel>(`${environment.ApiURL}/user/${id}`).pipe(
      map((data: UserModel) => {
        return data;
      }),
      catchError(err => {
        if (!environment.production && err.status == 404) {
          return of(err);
        } 
        else 
          throw err;
      })
    );
  }
  getTrainer(training_id: any): Observable<UserModel> {
    return this.http.get<UserModel>(`${environment.ApiURL}/user/getTrainer?training_id=${training_id}`).pipe(
      map((data: any) => {
        return data;
      }),
      catchError(err => {
        if (!environment.production && err.status == 404) {
          return of(err);
        } 
        else 
          throw err;
      })
    );
  }

  Login(email, password): Observable<UserModel> {
    return this.http.get<UserModel>(`${environment.ApiURL}/user/login?email=${email}&password=${password}`).pipe(
      map((data: UserModel) => {
        return data;
      }),
      catchError((err) => {
        if (
          !environment.production &&
          (err.status == 404 || err.status == 405)
        ) {         
          return of(email, password);
        } else throw err;
      })
    );
  }
  modifyUser(model: UserModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${environment.ApiURL}/user/modify`, model).pipe(
      map((data: UserModel) => {
        return data;
      }),
      catchError((err) => {
        if (
          !environment.production &&
          (err.status == 404 || err.status == 405)
        ) {         
          return of(model);
        } else throw err;
      })
    );
  }
  deleteUser(model: UserModel): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: model
    }
    
    return this.http.delete<any>(`${environment.ApiURL}/user`, options)
    .pipe(
      map((data: any) => {
        return data;
      }),
      catchError(err => {
        if (!environment.production && (err.status == 404 || err.status == 405)) {          
          return of(true);
        }
        else
          throw err;
      })
    );
  }
}
