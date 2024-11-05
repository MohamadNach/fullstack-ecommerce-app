import axios from 'axios';

import { UserCreateDto, UserUpdateDto } from '../types/User';
import { base_Azure_URL, base_localhost_URL } from '../types/Auth';

// const API_URL = base_Azure_URL;
const API_URL = base_localhost_URL;

// Fetch User
export const fetchUser = async (userId: string) => {
  const response = await axios.get(`${API_URL}/users/${userId}`);
  return response.data;
};

// Create User
export const createUser = async (userData: UserCreateDto) => {
  const response = await axios.post(`${API_URL}/users`, userData);
  return response.data;
};

// Update User
export const updateUser = async (userId: string, userData: UserUpdateDto) => {
  const response = await axios.put(`${API_URL}/users/${userId}`, userData);
  return response.data;
};

// Delete User
export const deleteUser = async (userId: string) => {
  const response = await axios.delete(`${API_URL}/users/${userId}`);
  return response.data;
};
