import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { Action } from '@ngrx/store';
import {
  friendsActionTypes,
  GetFriends,
  GetFriendsError,
  GetFriendsSuccess,
  SearchFriends,
  SearchFriendsSuccess,
  SearchFriendsError,
  InviteFriend,
  InviteFriendSuccess,
  InviteFriendError,
  GetInvites,
  GetInvitesSuccess,
  AcceptInvitation,
  AcceptInvitationSuccess,
  AcceptInvitationError
} from './friends.actions';
import { FriendsService } from '../../services/friends.service';
import FriendDto, { SearchResultDto, InviteDto } from '../../models/friend.model';
import 'rxjs/Rx';

@Injectable()
export class FriendsEffects {
  constructor(private actions$: Actions, private friendsService: FriendsService) {}

  @Effect()
  getFriends$: Observable<Action> = this.actions$
    .ofType<GetFriends>(friendsActionTypes.getFriends)
    .debounceTime(500)
    .mergeMap(action => {
      return this.friendsService
        .GetFriends()
        .map((data: Array<FriendDto>) => {
          return new GetFriendsSuccess(data);
        })
        .catch(() => of(new GetFriendsError()));
    });

  @Effect()
  getInvites$: Observable<Action> = this.actions$
    .ofType<GetInvites>(friendsActionTypes.getInvites)
    .debounceTime(500)
    .mergeMap(action => {
      return this.friendsService
        .GetInvites()
        .map((data: Array<InviteDto>) => {
          return new GetInvitesSuccess(data);
        })
        .catch(() => of(new GetFriendsError()));
    });

  @Effect()
  searchFriends$: Observable<Action> = this.actions$
    .ofType<SearchFriends>(friendsActionTypes.searchFriends)
    .debounceTime(500)
    .mergeMap(action => {
      return this.friendsService
        .Search(action.query)
        .map((data: Array<SearchResultDto>) => {
          return new SearchFriendsSuccess(data);
        })
        .catch(() => of(new SearchFriendsError()));
    });

  @Effect()
  inviteFriend$: Observable<Action> = this.actions$
    .ofType<InviteFriend>(friendsActionTypes.inviteFriend)
    .debounceTime(500)
    .mergeMap(action => {
      return this.friendsService
        .Invite(action.userId)
        .map((data: FriendDto) => {
          return new InviteFriendSuccess(data);
        })
        .catch(() => of(new InviteFriendError()));
    });

  @Effect()
  acceptInvitation$: Observable<Action> = this.actions$
    .ofType<AcceptInvitation>(friendsActionTypes.acceptInvitation)
    .debounceTime(500)
    .mergeMap(action => {
      return this.friendsService
        .AcceptInvitation(action.invitation.id)
        .map((data: FriendDto) => {
          return new AcceptInvitationSuccess(data);
        })
        .catch(() => of(new AcceptInvitationError()));
    });
}
