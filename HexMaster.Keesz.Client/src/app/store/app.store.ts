import { UserState, INITIAL_USER_STATE } from './users/user.state';
import { UserReducer } from './users/user.reducers';
import { FriendsState, INITIAL_FRIENDS_STATE } from './friends/friends.state';
import { FriendsReducer } from './friends/friends.reducers';
import { GamesState, INITIAL_GAMES_STATE } from './games/games.state';
import { GamesReducer } from './games/games.reducer';

export interface AppStore {
  userState: UserState;
  friendsState: FriendsState;
  gamesState: GamesState;
}

export const INITIAL_APPSTORE: AppStore = {
  userState: INITIAL_USER_STATE,
  friendsState: INITIAL_FRIENDS_STATE,
  gamesState: INITIAL_GAMES_STATE
};

export const reducers = {
  userState: UserReducer,
  friendsState: FriendsReducer,
  gamesState: GamesReducer
};
