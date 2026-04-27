import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Answer, Question } from '../../../core/models/app.models';

@Component({
  selector: 'app-question-card',
  templateUrl: './question-card.component.html',
  styleUrls: ['./question-card.component.css'],
  standalone: false
})
export class QuestionCardComponent {
  @Input() question!: Question;
  @Input() answers: Answer[] = [];
  @Input() disabled = false;
  @Output() answered = new EventEmitter<number>();

  submit(answerId: number): void {
    if (!this.disabled) {
      this.answered.emit(answerId);
    }
  }
}
