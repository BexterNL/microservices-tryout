import UserInfoDto from '../../models/user.info.model';
import FriendDto, { SearchResultDto, InviteDto } from '../../models/friend.model';

export interface FriendsState {
  loading: boolean;
  searching: boolean;
  searchResult: Array<SearchResultDto>;
  errorMessage: string;
  friends: Array<FriendDto>;
  friendsInvited: Array<FriendDto>;
  friendRequests: Array<InviteDto>;
}

export const INITIAL_FRIENDS_STATE: FriendsState = {
  loading: false,
  searching: false,
  searchResult: null,
  errorMessage: null,
  friends: null,
  friendsInvited: null,
  friendRequests: null
};
