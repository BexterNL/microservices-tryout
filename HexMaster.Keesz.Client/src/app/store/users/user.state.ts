import UserInfoDto from '../../models/user.info.model';

export interface UserState {
  userInfo: UserInfoDto;
  loading: boolean;
}

export const INITIAL_USER_STATE: UserState = {
  userInfo: null,
  loading: false
};
