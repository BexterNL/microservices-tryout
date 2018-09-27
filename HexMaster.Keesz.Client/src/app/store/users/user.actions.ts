import { Action } from '@ngrx/store';
import UserInfoDto from '../../models/user.info.model';

export const userActionTypes = {
  getUserInfo: '[UserActions] getUserInfo',
  getUserInfoSuccess: '[UserActions] getUserInfoSuccess',
  getUserInfoError: '[UserActions] getUserInfoError'
};

export class GetUserInfo implements Action {
  readonly type = userActionTypes.getUserInfo;
  constructor() {}
}

export class GetUserInfoSuccess implements Action {
  readonly type = userActionTypes.getUserInfoSuccess;
  constructor(public model: UserInfoDto) {}
}

export class GetUserInfoError implements Action {
  readonly type = userActionTypes.getUserInfoError;
  constructor() {}
}
