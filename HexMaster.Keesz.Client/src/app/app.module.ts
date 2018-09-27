import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { UiModule } from './ui/ui.module';
import { PublicModule } from './public/public.module';
import { PrivateModule } from './private/private.module';

import { OAuthModule } from 'angular-oauth2-oidc';
import { UsersService } from './services/users.service';
import { StoreModule } from '@ngrx/store';
import { environment } from '../environments/environment';
import { INITIAL_APPSTORE, reducers } from './store/app.store';

import { storeFreeze } from 'ngrx-store-freeze';
import { UserEffects } from './store/users/user.effects';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { TokenInterceptor } from '../core/interceptors/auth.token.interceptor';

import { ToastrModule } from 'ngx-toastr';
import { ControlsModule } from './controls/controls.module';
import { FriendsEffects } from './store/friends/friends.effects';
import { GamesEffects } from './store/games/games.effects';

let metaReducers = [];
if (environment.production === false) {
  metaReducers = [storeFreeze];
}
@NgModule({
  declarations: [AppComponent],
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    OAuthModule.forRoot(),
    StoreModule.forRoot(reducers, {
      metaReducers: metaReducers,
      initialState: INITIAL_APPSTORE
    }),
    StoreDevtoolsModule.instrument({ maxAge: 5 }),
    EffectsModule.forRoot([UserEffects, FriendsEffects, GamesEffects]),
    ToastrModule.forRoot(),
    UiModule,
    ControlsModule,
    PublicModule,
    PrivateModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    UsersService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() {
    console.log('APP STARTING');
  }
}
