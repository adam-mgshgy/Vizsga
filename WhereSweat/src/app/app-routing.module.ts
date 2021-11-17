import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesPageComponent } from './components/categories-page/categories-page.component';
import { CategoryPageComponent } from './components/category-page/category-page.component';
import { LoginpageComponent } from './components/loginpage/loginpage.component';
import { MainpageComponent } from './components/mainpage/mainpage.component';
import { RegistryPageComponent } from './components/registry-page/registry-page.component';

const routes: Routes = [
  {path: 'home', component: MainpageComponent},
  {path: 'login', component: LoginpageComponent},
  {path: 'categories', component: CategoriesPageComponent},
  {path: 'category', component: CategoryPageComponent},
  {path: 'register', component: RegistryPageComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
