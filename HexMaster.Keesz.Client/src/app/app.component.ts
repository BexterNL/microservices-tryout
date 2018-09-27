import { Component } from '@angular/core';
import { OAuthService, JwksValidationHandler, AuthConfig } from 'angular-oauth2-oidc';
import { environment } from '../environments/environment';

export const authConfig: AuthConfig = {
  issuer: 'http://identity.local.bexter.nl',
  requireHttps: false,
  redirectUri: environment.loginCallback,
  logoutUrl: environment.logoutCallback,
  clientId: 'angularclient',
  scope: 'openid profile game connect api angularclient'
};

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor(private oauthService: OAuthService) {
    this.configureWithNewConfigApi();
  }

  private configureWithNewConfigApi() {
    this.oauthService.configure(authConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }
}
