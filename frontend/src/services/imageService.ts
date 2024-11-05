import axios from 'axios';
import { base_Azure_URL, base_localhost_URL } from '../types/Auth';

// const API_URL = base_Azure_URL;
const API_URL = base_localhost_URL;

// Fetch All Product Images
export const fetchProductImages = async () => {
  const response = await axios.get(`${API_URL}/productImages/images/`);
  console.log(response.data);
  return response.data;
};

// Fetch All Product main image
export const fetchProductMainImage = async (productId: string) => {
  const response = await axios.get(
    `${API_URL}/productImages/main/${productId}`
  );
  console.log(response.data);
  return response.data;
};
