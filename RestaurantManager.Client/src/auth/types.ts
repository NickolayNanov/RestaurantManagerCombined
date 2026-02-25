export type AuthUser = {
  id: string;
  email: string;
  name: string;
  roles: string[];
  exp: number;
};

export type AuthState = {
  token: string | null;
  user: AuthUser | null;
  isLoading: boolean;
};

export type LoginRequest = {
  email: string;
  password: string;
};

export type LoginResponse = {
  token: string;
  tokenExpiration: Date;
  user: AuthUser;
};

export type AuthEndpointResponse = {
    accessToken: string;
    accessTokenExpiration: Date;
    refreshToken: string | null;
    refreshTokenExpiration: Date | null;
}