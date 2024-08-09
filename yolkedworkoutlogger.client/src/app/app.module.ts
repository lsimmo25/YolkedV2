import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { WorkoutsComponent } from './components/workouts/workouts.component';
import { BodyweightComponent } from './components/bodyweight/bodyweight.component';
import { FoodComponent } from './components/food/food.component';
import { ProfileComponent } from './components/profile/profile.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    WorkoutsComponent,
    BodyweightComponent,
    FoodComponent,
    ProfileComponent,
    DashboardComponent,
    LoginComponent,
    SignupComponent,
    // Declare other components here if needed
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    // Import other modules here if needed
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
