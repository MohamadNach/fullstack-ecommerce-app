import { Outlet } from 'react-router-dom';
import Login from '../pages/Login';

const getCookie = (name: string): string | null => {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop()?.split(';')?.shift() || null;
  return null;
};

const ProtectedRoutes = () => {
  const tokenString = getCookie('_auth');
  const tokenData = tokenString ? JSON.parse(tokenString) : null;

  // const authString = localStorage.getItem('auth');
  // const tokenString = localStorage.getItem('Token');
  // const authData = authString ? JSON.parse(authString) : null;
  // const tokenData = tokenString ? JSON.parse(tokenString) : null;

  // Check if userData exists and if the user is logged in
  // if (authData && tokenData) {
  if (tokenData) {
    return <Outlet />;
  } else {
    return <Login />;
  }
};

export default ProtectedRoutes;
