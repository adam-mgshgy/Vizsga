import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router,
    private authService: AuthenticationService
  ) {}
  canActivate(route: ActivatedRouteSnapshot) {
    const token = localStorage.getItem('jwt');

    if (!token && this.jwtHelper.isTokenExpired(token)) {
      return false;
    }
    try {
      const routerPath = route.routeConfig?.path?.toLowerCase();
      switch (routerPath) {
        case 'categories':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'admin':
          return this.authService.hasRole('Admin');
        case 'profile':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'createtraining':
          return this.authService.hasRole('Trainer');
        case 'createtraining/:id':
          return this.authService.hasRole('Trainer');
        case 'addsession':
          return this.authService.hasRole('Trainer');
        case 'addsession/:trainingId/:sessionId':
          return this.authService.hasRole('Trainer');
        case 'training/:id':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'mytrainings':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'trainings':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'trainings/category/:category':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'trainings/tag/:tag':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'trainings/trainer/:trainer':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'trainings/name/:name':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'trainings/county/:county':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        case 'trainings/city/:city':
          return (
            this.authService.hasRole('Trainer') ||
            this.authService.hasRole('Admin') ||
            this.authService.hasRole('User')
          );
        default:
          return true;
      }
    } catch (Error) {
      this.router.navigate(['login']);
      return false;
    }
  }
}
