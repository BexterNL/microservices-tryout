import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';
import { Store, select } from '@ngrx/store';
import { AppStore } from '../../store/app.store';
import GameStatsDto from '../../models/games.model';
import { GetStats } from '../../store/games/games.actions';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public gameStats: GameStatsDto;

  constructor(private store: Store<AppStore>) {
    const self = this;
    this.store
      .select(state => state.gamesState.stats)
      .filter(gameStatistics => gameStatistics != null)
      .subscribe(gameStatistics => {
        console.log('runs');
        self.gameStats = gameStatistics;
      });
  }

  ngOnInit() {
    this.store.dispatch(new GetStats());
  }
}
