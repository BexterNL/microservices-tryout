import GameStatsDto from '../../models/games.model';

export interface GamesState {
  loading: boolean;
  stats: GameStatsDto;
}

export const INITIAL_GAMES_STATE: GamesState = {
  stats: null,
  loading: false
};
