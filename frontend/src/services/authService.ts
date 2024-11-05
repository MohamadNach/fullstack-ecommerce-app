import axios from 'axios';
import { base_Azure_URL, base_localhost_URL } from '../types/Auth';

// const API_URL = base_Azure_URL;
const API_URL = base_localhost_URL;

// Login User
export const loginUser = async (loginData: {
  email: string;
  password: string;
}) => {
  const response = await axios.post(`${API_URL}/Auth/login`, loginData, {
    // Ensuring cookies are sent
    withCredentials: true,
  });

  // Store the JWT token in localStorage (or cookies)
  if (response.data) {
    localStorage.setItem('Token', JSON.stringify(response.data));
  }
  var responses = {
    token: response.data.token,
    userId: response.data.userId,
    userRole: response.data.userRole,
  };
  return responses;
};

// Login User
export const logoutUser = async () => {
  const response = await axios.post(`${API_URL}/Auth/logout`);
  return response.data;
};

// Function to set up Axios with Authorization header
export const setupAuthHeader = () => {
  const token = localStorage.getItem('token');
  if (token) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  }
};
