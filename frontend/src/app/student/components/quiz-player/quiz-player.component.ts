import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { Answer, Question, QuizResult } from '../../../core/models/app.models';

@Component({
  selector: 'app-quiz-player',
  templateUrl: './quiz-player.component.html',
  styleUrls: ['./quiz-player.component.css'],
  standalone: false
})
export class QuizPlayerComponent implements OnChanges {
  @Input() studentProfileId!: number;
  @Input() quizId!: number;

  questions: Question[] = [];
  answersByQuestion: Record<number, Answer[]> = {};
  currentIndex = 0;
  feedback = '';
  currentCorrect = 0;
  submitted = false;
  result?: QuizResult;

  constructor(private readonly api: ApiService) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['quizId']?.currentValue) {
      this.reset();
      this.loadQuiz();
    }
  }

  private reset(): void {
    this.questions = [];
    this.answersByQuestion = {};
    this.currentIndex = 0;
    this.feedback = '';
    this.currentCorrect = 0;
    this.submitted = false;
    this.result = undefined;
  }

  private loadQuiz(): void {
    this.api.getQuestionsByQuiz(this.quizId).subscribe((questions) => {
      this.questions = questions;
      this.questions.forEach((q) => {
        this.api.getAnswersByQuestion(q.id).subscribe((answers) => (this.answersByQuestion[q.id] = answers));
      });
    });
  }

  onAnswered(answerId: number): void {
    const question = this.questions[this.currentIndex];
    this.submitted = true;
    this.api
      .submitAnswer({ studentProfileId: this.studentProfileId, questionId: question.id, selectedAnswerId: answerId })
      .subscribe((response) => {
        this.feedback = response.message;
        if (response.isCorrect) {
          this.currentCorrect += 1;
        }
      });
  }

  next(): void {
    this.feedback = '';
    this.submitted = false;
    if (this.currentIndex < this.questions.length - 1) {
      this.currentIndex += 1;
      return;
    }

    this.api.getQuizResult(this.studentProfileId, this.quizId).subscribe((result) => (this.result = result));
  }

  get progress(): number {
    if (!this.questions.length) return 0;
    return ((this.currentIndex + 1) / this.questions.length) * 100;
  }
}
