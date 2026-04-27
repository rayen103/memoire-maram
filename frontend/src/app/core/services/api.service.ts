import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  Answer,
  Badge,
  DashboardStats,
  ParkingZone,
  PagedResult,
  Question,
  Quiz,
  QuizResult,
  SafetyTip,
  StudentProfile,
  Video
} from '../models/app.models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly baseUrl = environment.apiUrl;

  constructor(private readonly http: HttpClient) {}

  getQuizzes(pageNumber = 1, pageSize = 10): Observable<PagedResult<Quiz>> {
    return this.http.get<PagedResult<Quiz>>(`${this.baseUrl}/quizzes?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getQuizzesByLevel(level: number): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(`${this.baseUrl}/quizzes/level/${level}`);
  }

  createQuiz(payload: { title: string; level: number }): Observable<Quiz> {
    return this.http.post<Quiz>(`${this.baseUrl}/quizzes`, payload);
  }

  getQuestionsByQuiz(quizId: number): Observable<Question[]> {
    return this.http.get<Question[]>(`${this.baseUrl}/questions/quiz/${quizId}`);
  }

  createQuestion(payload: { quizId: number; content: string }): Observable<Question> {
    return this.http.post<Question>(`${this.baseUrl}/questions`, payload);
  }

  getAnswersByQuestion(questionId: number): Observable<Answer[]> {
    return this.http.get<Answer[]>(`${this.baseUrl}/answers/question/${questionId}`);
  }

  createAnswer(payload: { questionId: number; content: string; isCorrect: boolean }): Observable<Answer> {
    return this.http.post<Answer>(`${this.baseUrl}/answers`, payload);
  }

  submitAnswer(payload: { studentProfileId: number; questionId: number; selectedAnswerId: number }): Observable<{ isCorrect: boolean; message: string }> {
    return this.http.post<{ isCorrect: boolean; message: string }>(`${this.baseUrl}/students/submit-answer`, payload);
  }

  getStudentProfileByUser(userId: number): Observable<StudentProfile> {
    return this.http.get<StudentProfile>(`${this.baseUrl}/students/profile/user/${userId}`);
  }

  getStudentBadges(studentProfileId: number): Observable<Badge[]> {
    return this.http.get<Badge[]>(`${this.baseUrl}/students/${studentProfileId}/badges`);
  }

  getQuizResult(studentProfileId: number, quizId: number): Observable<QuizResult> {
    return this.http.get<QuizResult>(`${this.baseUrl}/students/${studentProfileId}/quiz-result/${quizId}`);
  }

  getVideos(pageNumber = 1, pageSize = 10): Observable<PagedResult<Video>> {
    return this.http.get<PagedResult<Video>>(`${this.baseUrl}/videos?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createVideo(payload: { title: string; url: string; description: string }): Observable<Video> {
    return this.http.post<Video>(`${this.baseUrl}/videos`, payload);
  }

  getSafetyTips(pageNumber = 1, pageSize = 10): Observable<PagedResult<SafetyTip>> {
    return this.http.get<PagedResult<SafetyTip>>(`${this.baseUrl}/safety-tips?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createSafetyTip(payload: { content: string }): Observable<SafetyTip> {
    return this.http.post<SafetyTip>(`${this.baseUrl}/safety-tips`, payload);
  }

  getParkingZones(pageNumber = 1, pageSize = 10): Observable<PagedResult<ParkingZone>> {
    return this.http.get<PagedResult<ParkingZone>>(`${this.baseUrl}/parking-zones?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createParkingZone(payload: { schoolName: string; type: string; location: string }): Observable<ParkingZone> {
    return this.http.post<ParkingZone>(`${this.baseUrl}/parking-zones`, payload);
  }

  getDashboardStats(): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(`${this.baseUrl}/dashboard/stats`);
  }
}
