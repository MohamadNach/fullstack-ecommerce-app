import axios from 'axios';

import { CategoryCreateDto, CategoryUpdateDto } from '../types/Category';
import { base_Azure_URL, base_localhost_URL } from '../types/Auth';

// const API_URL = base_Azure_URL;
const API_URL = base_localhost_URL;

// Fetch All Categories with Subcategories
export const fetchAllCategories = async () => {
  const response = await axios.get(`${API_URL}/categories/with-subcategories`);
  return response.data;
};

// Fetch Category
export const fetchCategory = async (categoryId: string | undefined) => {
  const response = await axios.get(`${API_URL}/categories/${categoryId}`);
  return response.data;
};

// Create Cart
export const createCategory = async (cartData: CategoryCreateDto) => {
  const response = await axios.post(`${API_URL}/categories`, cartData);
  return response.data;
};

// Update Cart
export const updateCategory = async (
  categoryId: string,
  categoryData: CategoryUpdateDto
) => {
  const response = await axios.put(
    `${API_URL}/categories/${categoryId}`,
    categoryData
  );
  return response.data;
};

// Delete Cart
export const deleteCategory = async (categoryId: string) => {
  const response = await axios.delete(`${API_URL}/categories/${categoryId}`);
  return response.data;
};
