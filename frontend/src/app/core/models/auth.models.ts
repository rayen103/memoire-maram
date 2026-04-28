export interface AuthResponse {
  token: string;
  email: string;
  name: string;
  role: string;
  userId: number;
}

export interface AppUser {
  id: number;
  name: string;
  email: string;
  role: string;
}
