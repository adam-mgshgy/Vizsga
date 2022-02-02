import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TagModel } from '../models/tag-model';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  constructor(private http: HttpClient) { }

  getTags(): Observable<TagModel[]> {
    return this.http.get<TagModel[]>(`${environment.ApiURL}/tag`).pipe(
      map((data: TagModel[]) => {
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
  newTag(model: TagModel): Observable<TagModel> {
    return this.http.put<TagModel>(`${environment.ApiURL}/tag/add`, model).pipe(
      map((data: TagModel) => {
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
}
