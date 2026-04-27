import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StudentRoutingModule } from './student-routing.module';
import { StudentDashboardComponent } from './pages/student-dashboard/student-dashboard.component';
import { QuizPlayerComponent } from './components/quiz-player/quiz-player.component';
import { QuestionCardComponent } from './components/question-card/question-card.component';

@NgModule({
  declarations: [StudentDashboardComponent, QuizPlayerComponent, QuestionCardComponent],
  imports: [CommonModule, FormsModule, StudentRoutingModule]
})
export class StudentModule {}
