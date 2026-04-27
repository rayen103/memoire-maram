export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface Quiz {
  id: number;
  title: string;
  level: number;
}

export interface Question {
  id: number;
  quizId: number;
  content: string;
}

export interface Answer {
  id: number;
  questionId: number;
  content: string;
  isCorrect: boolean;
}

export interface Badge {
  id: number;
  name: string;
  description: string;
  minPoints: number;
}

export interface StudentProfile {
  id: number;
  userId: number;
  points: number;
  level: number;
  name: string;
  badges: Badge[];
}

export interface QuizResult {
  quizId: number;
  quizTitle: string;
  totalQuestions: number;
  correctAnswers: number;
  score: number;
  answers: { questionContent: string; selectedAnswerContent: string; correctAnswerContent: string; isCorrect: boolean }[];
}

export interface Video {
  id: number;
  title: string;
  url: string;
  description: string;
}

export interface SafetyTip {
  id: number;
  content: string;
}

export interface ParkingZone {
  id: number;
  schoolName: string;
  type: string;
  location: string;
}

export interface DashboardStats {
  totalUsers: number;
  totalStudents: number;
  totalParents: number;
  totalQuizzes: number;
  averageScore: number;
}
