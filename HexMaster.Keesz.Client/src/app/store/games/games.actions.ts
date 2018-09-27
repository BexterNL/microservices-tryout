import { Action } from '@ngrx/store';
import GameStatsDto from '../../models/games.model';

export const gamesActionTypes = {
  getStats: '[GamesActions] getStats',
  getStatsSuccess: '[GamesActions] getStatsSuccess',
  getStatsFailed: '[GamesActions] getStatsFailed'
};

export class GetStats implements Action {
  readonly type = gamesActionTypes.getStats;
  constructor() {}
}

export class GetStatsSuccess implements Action {
  readonly type = gamesActionTypes.getStatsSuccess;
  constructor(public model: GameStatsDto) {}
}

export class GetStatsFailed implements Action {
  readonly type = gamesActionTypes.getStatsFailed;
  constructor() {}
}
