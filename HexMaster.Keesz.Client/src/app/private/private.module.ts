import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from '../../core/guards/authguard';
import { FriendsComponent } from './friends/friends.component';
import { GamesComponent } from './games/games.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { ControlsModule } from '../controls/controls.module';
import { FriendsService } from '../services/friends.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, ControlsModule, FormsModule],
  declarations: [
    DashboardComponent,
    FriendsComponent,
    GamesComponent,
    StatisticsComponent
  ],
  providers: [AuthGuard, FriendsService]
})
export class PrivateModule {}
