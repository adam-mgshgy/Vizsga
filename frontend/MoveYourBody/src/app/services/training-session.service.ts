import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { TrainingSessionModel } from '../models/training-session-model';

@Injectable({
  providedIn: 'root'
})
export class TrainingSessionService {
  constructor(private http: HttpClient) { }

  CreateTrainingSession(model: TrainingSessionModel): Observable<TrainingSessionModel> {
    return this.http
      .put<TrainingSessionModel>(`${environment.ApiURL}/sessions/create`, model)
      .pipe(
        map((data: TrainingSessionModel) => {
          return data;
        }),
        catchError((err) => {
          if (
            !environment.production &&
            (err.status == 404 || err.status == 405)
          ) {
            return of(model);
          } else {
            throw err;
          }
        })
      );
  }
  modifyTrainingSession(model: TrainingSessionModel): Observable<TrainingSessionModel> {
    return this.http
      .post<TrainingSessionModel>(`${environment.ApiURL}/sessions/modify`, model)
      .pipe(
        map((data: TrainingSessionModel) => {
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
  deleteTrainingSession(model: TrainingSessionModel): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: model,
    };

    return this.http
      .delete<any>(`${environment.ApiURL}/sessions`, options)
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (
            !environment.production &&
            (err.status == 404 || err.status == 405)
          ) {
            return of(true);
          } else throw err;
        })
      );
  }
  listByTrainingId(trainingId: any): Observable<TrainingSessionModel[]> {
    return this.http
      .get<TrainingSessionModel[]>(`${environment.ApiURL}/sessions/list/${trainingId}`)
      .pipe(
        map((data: TrainingSessionModel[]) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
}
