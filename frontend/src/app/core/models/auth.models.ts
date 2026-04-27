export interface AuthResponse {
  token: string;
  expiresAt: string;
  user: {
    id: number;
    name: string;
    email: string;
    role: string;
  };
}

export interface AppUser {
  id: number;
  name: string;
  email: string;
  role: string;
}
