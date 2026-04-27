import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FeedbackBannerComponent } from './components/feedback-banner/feedback-banner.component';

@NgModule({
  declarations: [NavbarComponent, FeedbackBannerComponent],
  imports: [CommonModule, RouterModule],
  exports: [NavbarComponent, FeedbackBannerComponent]
})
export class SharedModule {}
