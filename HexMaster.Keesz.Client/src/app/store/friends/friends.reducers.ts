import * as _ from 'lodash';
import { Action } from '@ngrx/store';
import { FriendsState } from './friends.state';
import {
  friendsActionTypes,
  GetFriends,
  GetFriendsSuccess,
  SearchFriendsSuccess,
  SearchFriends,
  InviteFriendSuccess,
  GetInvitesSuccess,
  AcceptInvitation,
  AcceptInvitationSuccess
} from './friends.actions';
import FriendDto, { InviteDto } from '../../models/friend.model';

export function FriendsReducer(state: FriendsState, action: any) {
  {
    switch (action.type) {
      case friendsActionTypes.getFriends:
        return getFriendsHandler(state, action);
      case friendsActionTypes.getFriendsSuccess:
        return getFriendsSuccessHandler(state, action);
      case friendsActionTypes.getInvitesSuccess:
        return getInvitesSuccessHandler(state, action);
      case friendsActionTypes.searchFriends:
        return searchFriendsHandler(state, action);
      case friendsActionTypes.searchFriendsSuccess:
        return searchFriendsSuccessHandler(state, action);
      case friendsActionTypes.inviteFriendSuccess:
        return inviteFriendSuccessHandler(state, action);

      case friendsActionTypes.acceptInvitation:
        return acceptInvitationHandler(state, action);
      case friendsActionTypes.acceptInvitationSuccess:
        return acceptInvitationSuccessHandler(state, action);

      default:
        return state;
    }
  }
}

function getFriendsHandler(state: FriendsState, action: GetFriends): FriendsState {
  const copyState: FriendsState = Object.assign({}, state);
  copyState.loading = true;
  return copyState;
}

function getFriendsSuccessHandler(state: FriendsState, action: GetFriendsSuccess): FriendsState {
  const copyState: FriendsState = Object.assign({}, state);
  copyState.loading = false;
  copyState.errorMessage = null;
  copyState.friends = _.filter(action.model, function(frnd) {
    return frnd.isAccepted;
  });
  copyState.friendsInvited = _.filter(action.model, function(frnd) {
    return !frnd.isAccepted;
  });
  return copyState;
}

function getInvitesSuccessHandler(state: FriendsState, action: GetInvitesSuccess): FriendsState {
  const copyState: FriendsState = Object.assign({}, state);
  copyState.errorMessage = null;
  copyState.friendRequests = action.result;
  return copyState;
}

function searchFriendsHandler(state: FriendsState, action: SearchFriends): FriendsState {
  const copyState: FriendsState = Object.assign({}, state);
  copyState.searching = true;
  return copyState;
}
function searchFriendsSuccessHandler(state: FriendsState, action: SearchFriendsSuccess): FriendsState {
  const copyState: FriendsState = Object.assign({}, state);
  copyState.searching = false;
  copyState.searchResult = action.result;
  return copyState;
}

function inviteFriendSuccessHandler(state: FriendsState, action: InviteFriendSuccess): FriendsState {
  const copyState: FriendsState = Object.assign({}, state);

  if (state.friendsInvited != null) {
    const originalEntries = copyState.friendsInvited as Array<FriendDto>;
    const newInvited = new Array<FriendDto>(...originalEntries);
    newInvited.push(action.result);
    copyState.friendsInvited = newInvited;
  } else {
    const newInvited = new Array<FriendDto>();
    newInvited.push(action.result);
    copyState.friendsInvited = newInvited;
  }

  return copyState;
}

function acceptInvitationHandler(state: FriendsState, action: AcceptInvitation): FriendsState {
  const copyState: FriendsState = Object.assign({}, state);
  copyState.loading = true;

  const originalInvitations = copyState.friendRequests as Array<InviteDto>;
  const newInvitations = new Array<InviteDto>(...originalInvitations);
  const entry = _.find(newInvitations, { id: action.invitation.id });
  const index = newInvitations.indexOf(entry, 0);
  if (index > -1) {
    newInvitations.splice(index, 1);
  }
  copyState.friendRequests = newInvitations;

  return copyState;
}
function acceptInvitationSuccessHandler(state: FriendsState, action: AcceptInvitationSuccess): FriendsState {
  const copyState: FriendsState = Object.assign({}, state);
  copyState.loading = false;
  copyState.errorMessage = null;

  if (state.friends != null) {
    const originalEntries = copyState.friends as Array<FriendDto>;
    const newFriendsArray = new Array<FriendDto>(...originalEntries);
    newFriendsArray.push(action.result);
    copyState.friends = newFriendsArray;
  } else {
    const newFriendsArray = new Array<FriendDto>();
    newFriendsArray.push(action.result);
    copyState.friends = newFriendsArray;
  }

  return copyState;
}
