import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginpageComponent } from './components/login-page/login-page.component';
import { MainpageComponent } from './components/main-page/main-page.component';
import { CategoriesPageComponent } from './components/categories-page/categories-page.component';
import { CategoryPageComponent } from './components/category-page/category-page.component';
import { RegistryPageComponent } from './components/registry-page/registry-page.component';
import { ProfileSettingsComponent } from './components/profile-settings/profile-settings.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginpageComponent,
    MainpageComponent,
    CategoriesPageComponent,
    CategoryPageComponent,
    RegistryPageComponent,
    ProfileSettingsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }