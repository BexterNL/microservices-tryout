import { CanActivate } from "@angular/router";
import { Injectable } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private oauthService: OAuthService) { }

    canActivate() {
        if (this.oauthService.hasValidAccessToken()) {
            return true;
        }

        this.oauthService.initImplicitFlow();
    }
}