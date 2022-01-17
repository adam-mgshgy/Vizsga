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
import { CreateTrainingPageComponent } from './components/create-training-page/create-training-page.component';
import { AddSessionPageComponent } from './components/add-session-page/add-session-page.component';
import { TrainingPageComponent } from './components/training-page/training-page.component';

import {MatIconModule} from '@angular/material/icon';

import { AngularResizeEventModule } from 'angular-resize-event';
import { MyTrainingsPageComponent } from './components/my-trainings-page/my-trainings-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {MatDividerModule} from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtInterceptor } from './JWT/JwtInterceptor';
import { ErrorInterceptor } from './JWT/ErrorInterceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginpageComponent,
    MainpageComponent,
    CategoriesPageComponent,
    CategoryPageComponent,
    RegistryPageComponent,
    ProfileSettingsComponent,
    CreateTrainingPageComponent,
    AddSessionPageComponent,
    TrainingPageComponent,
    MyTrainingsPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AngularResizeEventModule,
    BrowserAnimationsModule,
    NgbModule,
    MatExpansionModule,
    MatDividerModule,
    HttpClientModule,
    FormsModule,
    MatIconModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
