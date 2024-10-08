import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { WorkoutsComponent } from './components/workouts/workouts.component';
import { BodyweightComponent } from './components/bodyweight/bodyweight.component';
import { FoodComponent } from './components/food/food.component';
import { ProfileComponent } from './components/profile/profile.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component'
import { SignupComponent } from './components/signup/signup.component'

const routes: Routes = [
  { path: '', component: HomeComponent }, // Default route to HomeComponent
  { path: 'workouts', component: WorkoutsComponent },
  { path: 'bodyweight', component: BodyweightComponent },
  { path: 'food', component: FoodComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent }
  // Add other routes here if needed
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
