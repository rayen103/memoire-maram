import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { AuthService } from '../../../core/services/auth.service';
import { Quiz, Score, StudentProfile, Video } from '../../../core/models/app.models';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css'],
  standalone: false
})
export class StudentDashboardComponent implements OnInit {
  profile?: StudentProfile;
  quizzes: Quiz[] = [];
  videos: Video[] = [];
  scores: Score[] = [];
  selectedQuizId?: number;
  loading = false;

  constructor(private readonly api: ApiService, private readonly auth: AuthService) {}

  ngOnInit(): void {
    const userId = this.auth.user?.id;
    if (!userId) {
      return;
    }

    this.loading = true;
    this.api.getStudentProfileByUser(userId).subscribe({
      next: (profile) => {
        this.profile = profile;
        this.api.getQuizzesByLevel(profile.level).subscribe((quizzes) => (this.quizzes = quizzes));
        this.api.getVideos(1, 6).subscribe((videos) => (this.videos = videos.items));
        this.api.getStudentScores(profile.id).subscribe((scores) => (this.scores = scores));
      },
      complete: () => (this.loading = false)
    });
  }

  startQuiz(quizId: number): void {
    this.selectedQuizId = quizId;
  }

  onQuizCompleted(score: number): void {
    if (this.profile && this.selectedQuizId) {
      this.api.recordScore({ studentProfileId: this.profile.id, quizId: this.selectedQuizId, scoreObtenu: score }).subscribe((s) => {
        this.scores = [s, ...this.scores.filter((x) => x.quizId !== s.quizId)];
      });
    }
  }
}
