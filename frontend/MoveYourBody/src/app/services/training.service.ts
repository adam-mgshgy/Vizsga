import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TrainingModel } from '../models/training-model';
import { catchError, map } from 'rxjs/operators';
import { TrainingImagesModel } from '../models/training-images-model';

@Injectable({
  providedIn: 'root',
})
export class TrainingService {
  constructor(private http: HttpClient) {}

  newTraining(model: TrainingModel): Observable<TrainingModel> {
    return this.http
      .put<TrainingModel>(`${environment.ApiURL}/training`, model)
      .pipe(
        map((data: TrainingModel) => {
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
  saveImage(images: any, trainingId: number): Observable<TrainingImagesModel> {
    return this.http
      .put<TrainingImagesModel>(`${environment.ApiURL}/training/Images?trainingId=${trainingId}`, images)
      .pipe(
        map((data: TrainingImagesModel) => {          
          return data;
        }),
        catchError((err) => {
          if (
            !environment.production &&
            (err.status == 404 || err.status == 405)
          ) {
            return of(images);
          } else {
            throw err;
          }
        })
      );
  }

  modifyTraining(model: TrainingModel): Observable<TrainingModel> {
    return this.http
      .post<TrainingModel>(`${environment.ApiURL}/training/modify`, model)
      .pipe(
        map((data: TrainingModel) => {
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
  getImageById(id: any): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/Images/${id}`)
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  deleteTraining(model: TrainingModel): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: model,
    };

    return this.http
      .delete<any>(`${environment.ApiURL}/training`, options)
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
  deleteImage(id: number[]): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: id,
    };

    return this.http
      .delete<any>(`${environment.ApiURL}/training/Images/delete`, options)
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

  getById(id: any): Observable<TrainingModel> {
    return this.http
      .get<TrainingModel>(`${environment.ApiURL}/training/${id}`)
      .pipe(
        map((data: TrainingModel) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }

  getByTrainerId(trainerId: any): Observable<any> {
    return this.http
      .get<any>(
        `${environment.ApiURL}/training/TrainerId/${trainerId}`
      )
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  getByUserId(userId: any): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/UserId/${userId}`)
      .pipe(
        map((data) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  getByCategory(id: any): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/category?id=${id}`)
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  getByTag(id: any): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/tag?id=${id}`)
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  getByName(name: string): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/name?trainingName=${name}`)
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  getByCounty(county: string): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/county?county=${county}`)
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  getByCity(city: string): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/city?city=${city}`)
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  listByTrainingId(trainingId: any): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/data?trainingId=${trainingId}`)
      .pipe(
        map((data) => {
          return data;
        }),
        catchError((err) => {
          if (!environment.production && err.status == 404) {
            return of(err);
          } else throw err;
        })
      );
  }
  getAll(): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/training/all`)
      .pipe(
        map((data: any) => {
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
