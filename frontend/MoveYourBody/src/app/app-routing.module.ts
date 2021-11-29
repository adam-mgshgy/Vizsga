import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesPageComponent } from './components/categories-page/categories-page.component';
import { CategoryPageComponent } from './components/category-page/category-page.component';
import { LoginpageComponent } from './components/login-page/login-page.component';
import { MainpageComponent } from './components/main-page/main-page.component';
import { RegistryPageComponent } from './components/registry-page/registry-page.component';
import { ProfileSettingsComponent } from './components/profile-settings/profile-settings.component';
import { CreateTrainingPageComponent } from './components/create-training-page/create-training-page.component';
import { AddSessionPageComponent } from './components/add-session-page/add-session-page.component';

const routes: Routes = [
  {path: 'home', component: MainpageComponent},
  {path: 'login', component: LoginpageComponent},
  {path: 'categories', component: CategoriesPageComponent},
  {path: 'category/:category', component: CategoryPageComponent},
  {path: 'register', component: RegistryPageComponent},
  {path: 'profile', component: ProfileSettingsComponent},
  {path: 'createtraining', component: CreateTrainingPageComponent},
  {path: 'addsession', component: AddSessionPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
