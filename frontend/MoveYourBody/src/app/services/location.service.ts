import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LocationModel } from '../models/location-model';
import { catchError, map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }
  getLocations(): Observable<LocationModel[]> {
    return this.http.get<LocationModel[]>(`${environment.ApiURL}`).pipe(
      map((data: LocationModel[]) => {
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
