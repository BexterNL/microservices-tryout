import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-landingpage',
  templateUrl: './landingpage.component.html',
  styleUrls: ['./landingpage.component.scss']
})
export class LandingpageComponent implements OnInit {
  constructor(private oauthService: OAuthService) {}

  login() {
    this.oauthService.initImplicitFlow();
  }

  ngOnInit() {}
}
