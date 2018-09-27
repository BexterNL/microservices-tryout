import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingpageComponent } from './landingpage/landingpage.component';
import { CallbackComponent } from './callback/callback.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [LandingpageComponent, CallbackComponent]
})
export class PublicModule { }
