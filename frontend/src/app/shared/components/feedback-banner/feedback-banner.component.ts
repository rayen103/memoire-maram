import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-feedback-banner',
  templateUrl: './feedback-banner.component.html',
  styleUrls: ['./feedback-banner.component.css']
})
export class FeedbackBannerComponent {
  @Input() message = '';
  @Input() type: 'success' | 'error' | 'info' = 'info';
}
