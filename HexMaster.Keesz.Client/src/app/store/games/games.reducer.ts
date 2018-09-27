import { GamesState } from './games.state';
import { gamesActionTypes, GetStats, GetStatsSuccess } from './games.actions';

export function GamesReducer(state: GamesState, action: any) {
  {
    switch (action.type) {
      case gamesActionTypes.getStats:
        return getStatsHandler(state, action);
      case gamesActionTypes.getStatsSuccess:
        return getStatsSuccessHandler(state, action);
      default:
        return state;
    }
  }
}

function getStatsHandler(state: GamesState, action: GetStats): GamesState {
  const copyState: GamesState = Object.assign({}, state);
  copyState.loading = true;
  copyState.stats = null;
  return copyState;
}

function getStatsSuccessHandler(
  state: GamesState,
  action: GetStatsSuccess
): GamesState {
  const copyState: GamesState = Object.assign({}, state);
  copyState.loading = false;
  copyState.stats = action.model;
  return copyState;
}
