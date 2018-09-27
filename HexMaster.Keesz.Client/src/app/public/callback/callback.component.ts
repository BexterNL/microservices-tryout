import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { Store } from '@ngrx/store';
import { AppStore } from '../../store/app.store';
import { GetUserInfo } from '../../store/users/user.actions';

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.scss']
})
export class CallbackComponent implements OnInit {
  constructor(private oauthService: OAuthService, private router: Router, private store: Store<AppStore>) {
    const self = this;
    this.store
      .select(state => state.userState.userInfo)
      .filter(value => value != null)
      .subscribe(value => {
        self.router.navigate(['/private/dashboard']);
      });
  }

  ngOnInit() {
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
      if (!this.oauthService.hasValidIdToken() || !this.oauthService.hasValidAccessToken()) {
        this.oauthService.initImplicitFlow('some-state');
      } else {
        this.store.dispatch(new GetUserInfo());
      }
    });
  }
}
