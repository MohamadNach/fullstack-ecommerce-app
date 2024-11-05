import axios from 'axios';

import { CartItemCreateDto, CartItemUpdateDto } from '../types/CartItem';
import { base_Azure_URL, base_localhost_URL } from '../types/Auth';

// const API_URL = base_Azure_URL;
const API_URL = base_localhost_URL;

// Fetch All CartItem
export const fetchCartItem = async (cartId: string) => {
  const response = await axios.get(`${API_URL}/cartItems/${cartId}`);
  return response.data;
};

// Create CartItem
export const createCartItem = async (cartItemData: CartItemCreateDto) => {
  const response = await axios.post(`${API_URL}/cartItems`, cartItemData);
  return response.data;
};

// Update CartItem
export const updateCartItem = async (
  cartItemId: string,
  cartItemData: CartItemUpdateDto
) => {
  const response = await axios.put(
    `${API_URL}/cartItems/${cartItemId}`,
    cartItemData
  );
  return response.data;
};

// Delete CartItem
export const deleteCartItem = async (cartItemId: string) => {
  const response = await axios.delete(`${API_URL}/cartItems/${cartItemId}`);
  return response.data;
};
