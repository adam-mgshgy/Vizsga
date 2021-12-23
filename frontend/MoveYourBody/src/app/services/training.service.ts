import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TrainingModel } from '../models/training-model';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TrainingService {
  constructor(private http: HttpClient) {}

  newTraining(model: TrainingModel): Observable<TrainingModel> {
    return this.http.put<TrainingModel>(`${environment.ApiURL}/training`, model).pipe(
      map((data: TrainingModel) => {
        return data;
      }),
      catchError((err) => {
        if (
          !environment.production &&
          (err.status == 404 || err.status == 405)
        ) {         
          return of(err);
        } else throw err;
      })
    );
  }

  
}
