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
  description: string;
  level: number;
  scoreMax: number;
  questionCount: number;
}

export interface Correction {
  id: number;
  questionId: number;
  text: string;
  image: string;
  video: string;
}

export interface Question {
  id: number;
  quizId: number;
  content: string;
  type: string;
  image: string;
  explication: string;
  answers: Answer[];
  correction?: Correction;
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
  type: string;
  image: string;
  minPoints: number;
}

export interface StudentProfile {
  id: number;
  userId: number;
  firstName: string;
  age: number;
  avatar: string;
  points: number;
  level: number;
  name: string;
  badges: Badge[];
}

export interface Score {
  id: number;
  studentProfileId: number;
  quizId: number;
  quizTitle: string;
  scoreObtenu: number;
  date: string;
}

export interface Defi {
  id: number;
  title: string;
  description: string;
  objective: string;
  pointsGain: number;
}

export interface StudentDefi {
  defiId: number;
  title: string;
  description: string;
  objective: string;
  pointsGain: number;
  isCompleted: boolean;
  startedAt: string;
  completedAt?: string;
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
  title: string;
  description: string;
  type: string;
  image: string;
  video: string;
}

export interface ParkingZone {
  id: number;
  schoolName: string;
  zoneName: string;
  type: string;
  location: string;
  description: string;
  latitude: number;
  longitude: number;
}

export interface DashboardStats {
  totalUsers: number;
  totalStudents: number;
  totalParents: number;
  totalQuizzes: number;
  averageScore: number;
}
