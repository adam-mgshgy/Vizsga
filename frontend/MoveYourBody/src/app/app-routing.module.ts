import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesPageComponent } from './components/categories-page/categories-page.component';
import { LoginpageComponent } from './components/login-page/login-page.component';
import { MainpageComponent } from './components/main-page/main-page.component';
import { RegistryPageComponent } from './components/registry-page/registry-page.component';
import { ProfileSettingsComponent } from './components/profile-settings/profile-settings.component';
import { CreateTrainingPageComponent } from './components/create-training-page/create-training-page.component';
import { AddSessionPageComponent } from './components/add-session-page/add-session-page.component';
import { TrainingPageComponent } from './components/training-page/training-page.component';
import { MyTrainingsPageComponent } from './components/my-trainings-page/my-trainings-page.component';
import { AdminPageComponent } from './components/admin-page/admin-page.component';
import { TrainingsPageComponent } from './components/trainings-page/trainings-page.component';
import { AuthGuard } from './JWT/auth-guard.service';

const routes: Routes = [
  {path: '', redirectTo:'home', pathMatch: 'full'},
  {path: 'home', component: MainpageComponent},
  {path: 'login', component: LoginpageComponent},
  {path: 'categories', component: CategoriesPageComponent, canActivate:[AuthGuard]},
  {path: 'register', component: RegistryPageComponent},
  {path: 'profile', component: ProfileSettingsComponent, canActivate:[AuthGuard]},
  {path: 'createtraining', component: CreateTrainingPageComponent, canActivate:[AuthGuard]},
  {path: 'createtraining/:id', component: CreateTrainingPageComponent, canActivate:[AuthGuard]},
  {path: 'addsession', component: AddSessionPageComponent, canActivate:[AuthGuard]},
  {path: 'addsession/:trainingId/:sessionId', component: AddSessionPageComponent, canActivate:[AuthGuard]},
  {path: 'training/:id', component: TrainingPageComponent, canActivate:[AuthGuard]},
  {path: 'mytrainings/:mode', component: MyTrainingsPageComponent, canActivate:[AuthGuard]},
  {path: 'admin', component: AdminPageComponent, canActivate:[AuthGuard]},
  {path: 'trainings', component: TrainingsPageComponent, canActivate:[AuthGuard]},
  {path: 'trainings/category/:category', component: TrainingsPageComponent, canActivate:[AuthGuard]},
  {path: 'trainings/tag/:tag', component: TrainingsPageComponent, canActivate:[AuthGuard]},
  {path: 'trainings/trainer/:trainer', component: TrainingsPageComponent, canActivate:[AuthGuard]},
  {path: 'trainings/name/:name', component: TrainingsPageComponent, canActivate:[AuthGuard]},
  {path: 'trainings/county/:county', component: TrainingsPageComponent, canActivate:[AuthGuard]},
  {path: 'trainings/city/:city', component: TrainingsPageComponent, canActivate:[AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
