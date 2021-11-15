import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryPageComponent } from './components/category-page/category-page.component';
import { LoginpageComponent } from './components/loginpage/loginpage.component';
import { MainpageComponent } from './components/mainpage/mainpage.component';

const routes: Routes = [
  {path: 'home', component: MainpageComponent},
  {path: 'login', component: LoginpageComponent},
  {path: 'category', component: CategoryPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
