import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { TagTrainingModel } from '../models/tag-training-model';

@Injectable({
  providedIn: 'root'
})
export class TagTrainingService {

  constructor(private http: HttpClient) { }

  newTagTraining(model: TagTrainingModel): Observable<TagTrainingModel> {
    return this.http.put<TagTrainingModel>(`${environment.ApiURL}/tagTraining`, model).pipe(
      map((data: TagTrainingModel) => {
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

 deleteTagTraining(model: TagTrainingModel): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: model
    }
    
    return this.http.delete<any>(`${environment.ApiURL}/tagTraining`, options)
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

  getByTag(id: any): Observable<TagTrainingModel[]> {
    return this.http.get<TagTrainingModel[]>(`${environment.ApiURL}/tagTraining/tag?field=${id}`, ).pipe(
      map((data: TagTrainingModel[]) => {
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

  getByTraining(id: any): Observable<TagTrainingModel[]> {
    return this.http.get<TagTrainingModel[]>(`${environment.ApiURL}/tagTraining/training?id=${id}`, ).pipe(
      map((data: TagTrainingModel[]) => {        
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