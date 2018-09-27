import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { Action } from '@ngrx/store';
import { FriendsService } from '../../services/friends.service';
import 'rxjs/Rx';
import {
  gamesActionTypes,
  GetStatsSuccess,
  GetStatsFailed,
  GetStats
} from './games.actions';
import GameStatsDto from '../../models/games.model';
import { GamesService } from '../../services/games.service';

@Injectable()
export class GamesEffects {
  constructor(private actions$: Actions, private gamesService: GamesService) {}

  @Effect()
  getStats$: Observable<Action> = this.actions$
    .ofType<GetStats>(gamesActionTypes.getStats)
    .debounceTime(500)
    .mergeMap((action) => {
      return this.gamesService
        .GetUserStatistics()
        .map((data: GameStatsDto) => {
          return new GetStatsSuccess(data);
        })
        .catch(() => of(new GetStatsFailed()));
    });
}
