import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Store } from '@ngrx/store';
import { AppStore } from '../../store/app.store';
import { GetUserInfo } from '../../store/users/user.actions';
import UserInfoDto from '../../models/user.info.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public userInfo: UserInfoDto;
  private loaded = false;
  constructor(private authService: OAuthService, private store: Store<AppStore>) {
    const self = this;
    this.store.select(state => state.userState.userInfo).subscribe(value => (self.userInfo = value));
  }

  ngOnInit() {}

  logout() {
    this.authService.logOut();
  }
}
