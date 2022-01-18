import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { ApplicantModel } from '../models/applicant-model';
import { TrainingSessionModel } from '../models/training-session-model';
import { UserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class ApplicantService {

  constructor(private http: HttpClient) { }
  newApplicant(model: ApplicantModel): Observable<ApplicantModel> {
    return this.http.put<ApplicantModel>(`${environment.ApiURL}/applicants/add`, model).pipe(
      map((data: ApplicantModel) => {
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
  deleteApplicant(model: ApplicantModel): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: model
    }
    
    return this.http.delete<any>(`${environment.ApiURL}/applicants`, options)
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
  listByUserId(userId: any): Observable<ApplicantModel[]> {
    return this.http.get<ApplicantModel[]>(`${environment.ApiURL}/applicants/list/user?userId=${userId}`, ).pipe(
      map((data: ApplicantModel[]) => {        
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
  listBySessionId(sessionId: any): Observable<ApplicantModel[]> {
    return this.http.get<ApplicantModel[]>(`${environment.ApiURL}/applicants/list/session?trainingSessionId=${sessionId}`, ).pipe(
      map((data: ApplicantModel[]) => {        
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
}
