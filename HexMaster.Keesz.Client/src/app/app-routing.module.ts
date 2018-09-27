import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LandingpageComponent } from './public/landingpage/landingpage.component';
import { LayoutComponent } from './ui/layout/layout.component';
import { DashboardComponent } from './private/dashboard/dashboard.component';
import { AuthGuard } from '../core/guards/authguard';
import { CallbackComponent } from './public/callback/callback.component';
import { GamesComponent } from './private/games/games.component';
import { FriendsComponent } from './private/friends/friends.component';
import { StatisticsComponent } from './private/statistics/statistics.component';

const routes: Routes = [
  {
    path: '',
    component: LandingpageComponent
  },
  {
    path: 'callback',
    component: CallbackComponent
  },
  {
    path: 'private',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: DashboardComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'games', component: GamesComponent },
      { path: 'friends', component: FriendsComponent },
      { path: 'statistics', component: StatisticsComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
