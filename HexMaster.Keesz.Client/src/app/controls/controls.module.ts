import { NgModule } from '@angular/core';
import { LoadingComponent } from './loading/loading.component';
import { CommonModule } from '@angular/common';
import { ModalComponent } from './modal/modal.component';
import { ModalService } from './modal/modal.service';

@NgModule({
  imports: [CommonModule],
  exports: [LoadingComponent, ModalComponent],
  declarations: [LoadingComponent, ModalComponent],
  providers: [ModalService]
})
export class ControlsModule {}
