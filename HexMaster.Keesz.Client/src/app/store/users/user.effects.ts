import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { Action } from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { UsersService } from '../../services/users.service';
import { GetUserInfo, GetUserInfoSuccess, GetUserInfoError, userActionTypes } from './user.actions';
import UserInfoDto from '../../models/user.info.model';
import 'rxjs/Rx';

@Injectable()
export class UserEffects {
  constructor(private actions$: Actions, private usersService: UsersService) {}

  @Effect()
  getUserInfo$: Observable<Action> = this.actions$
    .ofType<GetUserInfo>(userActionTypes.getUserInfo)
    .debounceTime(500)
    .mergeMap(action => {
      return this.usersService
        .GetUserInfo()
        .map((data: UserInfoDto) => {
          return new GetUserInfoSuccess(data);
        })
        .catch(() => of(new GetUserInfoError()));
    });
}
