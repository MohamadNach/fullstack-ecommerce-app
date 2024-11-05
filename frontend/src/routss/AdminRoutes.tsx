import { Outlet } from 'react-router-dom';
import Login from '../pages/Login';

const AdminRoutes = () => {
  const userDataString = localStorage.getItem('userData');
  const userData = userDataString ? JSON.parse(userDataString) : null;

  // Check if userData exists and if the user is logged in
  if (userData && userData.isLoggedIn) {
    return <Outlet />;
  } else {
    return <Login />;
  }
};

export default AdminRoutes;
