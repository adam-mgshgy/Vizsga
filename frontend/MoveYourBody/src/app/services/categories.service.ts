import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { CategoryModel } from '../models/category-model';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { ImagesModel } from '../models/images-model';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  constructor(private http: HttpClient) {}

  getCategories(): Observable<any> {
    return this.http
      .get<any>(`${environment.ApiURL}/categories`)
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
  newCategory(model: CategoryModel): Observable<CategoryModel> {
    return this.http
      .put<CategoryModel>(`${environment.ApiURL}/categories/add`, model)
      .pipe(
        map((data: CategoryModel) => {
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
  newImage(image: string[]): Observable<any> {
    return this.http
      .put<any>(`${environment.ApiURL}/categories/addImage`, image)
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((err) => {
          if (
            !environment.production &&
            (err.status == 404 || err.status == 405)
          ) {
            return of(image);
          } else throw err;
        })
      );
  }
}
