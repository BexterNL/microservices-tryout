import { UserState } from './user.state';
import { Action } from '@ngrx/store';
import { userActionTypes, GetUserInfo, GetUserInfoSuccess } from './user.actions';

export function UserReducer(state: UserState, action: any) {
  {
    switch (action.type) {
      case userActionTypes.getUserInfo:
        return handleFetchUserInfo(state, action);
      case userActionTypes.getUserInfoSuccess:
        return getUserInfoSuccessHandler(state, action);
      default:
        return state;
    }
  }
}

function handleFetchUserInfo(state: UserState, action: GetUserInfo): UserState {
  const copyState: UserState = Object.assign({}, state);
  copyState.loading = true;
  return copyState;
}

function getUserInfoSuccessHandler(state: UserState, action: GetUserInfoSuccess): UserState {
  const copyState: UserState = Object.assign({}, state);
  copyState.loading = false;
  copyState.userInfo = action.model;
  return copyState;
}
