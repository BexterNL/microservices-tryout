import { Action } from '@ngrx/store';
import UserInfoDto from '../../models/user.info.model';
import FriendDto, { SearchResultDto, InviteDto } from '../../models/friend.model';

export const friendsActionTypes = {
  getFriends: '[FriendsActions] getFriends',
  getFriendsSuccess: '[UserActions] getFriendsSuccess',
  getFriendsError: '[UserActions] getFriendsError',

  searchFriends: '[FriendsActions] searchFriends',
  searchFriendsSuccess: '[UserActions] searchFriendsSuccess',
  searchFriendsError: '[UserActions] searchFriendsError',

  getInvites: '[FriendsActions] getInvites',
  getInvitesSuccess: '[UserActions] getInvitesSuccess',
  getInvitesError: '[UserActions] getInvitesError',

  inviteFriend: '[FriendsActions] inviteFriend',
  inviteFriendSuccess: '[UserActions] inviteFriendSuccess',
  inviteFriendError: '[UserActions] inviteFriendError',

  acceptInvitation: '[FriendsActions] acceptInvitation',
  acceptInvitationSuccess: '[UserActions] acceptInvitationSuccess',
  acceptInvitationError: '[UserActions] acceptInvitationError'
};

export class GetFriends implements Action {
  readonly type = friendsActionTypes.getFriends;
  constructor() {}
}
export class GetFriendsSuccess implements Action {
  readonly type = friendsActionTypes.getFriendsSuccess;
  constructor(public model: Array<FriendDto>) {}
}
export class GetFriendsError implements Action {
  readonly type = friendsActionTypes.getFriendsError;
  constructor() {}
}

export class SearchFriends implements Action {
  readonly type = friendsActionTypes.searchFriends;
  constructor(public query: string) {}
}
export class SearchFriendsSuccess implements Action {
  readonly type = friendsActionTypes.searchFriendsSuccess;
  constructor(public result: Array<SearchResultDto>) {}
}
export class SearchFriendsError implements Action {
  readonly type = friendsActionTypes.searchFriendsError;
  constructor() {}
}

export class GetInvites implements Action {
  readonly type = friendsActionTypes.getInvites;
  constructor() {}
}
export class GetInvitesSuccess implements Action {
  readonly type = friendsActionTypes.getInvitesSuccess;
  constructor(public result: Array<InviteDto>) {}
}
export class GetInvitesError implements Action {
  readonly type = friendsActionTypes.getInvitesError;
  constructor() {}
}

export class InviteFriend implements Action {
  readonly type = friendsActionTypes.inviteFriend;
  constructor(public userId: string) {}
}
export class InviteFriendSuccess implements Action {
  readonly type = friendsActionTypes.inviteFriendSuccess;
  constructor(public result: FriendDto) {}
}
export class InviteFriendError implements Action {
  readonly type = friendsActionTypes.inviteFriendError;
  constructor() {}
}

export class AcceptInvitation implements Action {
  readonly type = friendsActionTypes.acceptInvitation;
  constructor(public invitation: InviteDto) {}
}
export class AcceptInvitationSuccess implements Action {
  readonly type = friendsActionTypes.acceptInvitationSuccess;
  constructor(public result: FriendDto) {}
}
export class AcceptInvitationError implements Action {
  readonly type = friendsActionTypes.acceptInvitationError;
  constructor() {}
}
