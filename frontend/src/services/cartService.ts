import axios from 'axios';

import { CartCreateDto, CartUpdateDto } from '../types/Cart';
import { base_Azure_URL, base_localhost_URL } from '../types/Auth';

// const API_URL = base_Azure_URL;
const API_URL = base_localhost_URL;

// Fetch Cart
export const fetchCart = async (cartId: string | undefined) => {
  const response = await axios.get(`${API_URL}/carts/${cartId}`);
  return response.data;
};

// Create Cart
export const createCart = async (cartData: CartCreateDto) => {
  const response = await axios.post(`${API_URL}/carts`, cartData);
  return response.data;
};

// Update Cart
export const updateCart = async (carttId: string, cartData: CartUpdateDto) => {
  const response = await axios.put(`${API_URL}/carts/${carttId}`, cartData);
  return response.data;
};

// Delete Cart
export const deleteCart = async (cartId: string) => {
  const response = await axios.delete(`${API_URL}/carts/${cartId}`);
  return response.data;
};
