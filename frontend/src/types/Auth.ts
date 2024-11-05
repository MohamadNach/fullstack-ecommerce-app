// Type for representing the state of the auth in Redux.
export interface AuthState {
  token: string;
  authenticated: boolean;
  error: string | null;
  loading: boolean;
  userId: string | null;
  userRole: string | null;
}

export const base_Azure_URL =
  'https://ecommerce-dev-app.azurewebsites.net/api/v1';
export const base_localhost_URL = 'http://localhost:5096/api/v1';
