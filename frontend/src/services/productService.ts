import axios from 'axios';

import { PaginationType } from '../types/Pagination';
import { ProductCreateDto, ProductUpdateDto } from '../types/Product';
import { base_Azure_URL, base_localhost_URL } from '../types/Auth';

// const API_URL = base_Azure_URL;
const API_URL = base_localhost_URL;

// Fetch All Products
export const fetchAllProduct = async ({ page, perPage }: PaginationType) => {
  let url = `${API_URL}/products?Page=${page}&PerPage=${perPage}`;

  const response = await axios.get(url);

  const products = response.data.items;
  const total = response.data.totalPages;

  return { products, total };
};

// Fetch Latest Product
export const fetchLatestProducts = async (productNumber: number) => {
  const response = await axios.get(
    `${API_URL}/products?limit=${productNumber}`
  );

  return response.data;
};

// Fetch Product
export const fetchProduct = async (productId: string) => {
  const response = await axios.get(`${API_URL}/products/${productId}`);
  return response.data;
};

// Create Product
export const createProduct = async (productData: ProductCreateDto) => {
  const response = await axios.post(`${API_URL}/products`, productData);
  return response.data;
};

// Update Product
export const updateProduct = async (
  productId: string,
  productData: ProductUpdateDto
) => {
  const response = await axios.put(
    `${API_URL}/products/${productId}`,
    productData
  );
  return response.data;
};

// Delete Product
export const deleteProduct = async (productId: string) => {
  const response = await axios.delete(`${API_URL}/products/${productId}`);
  return response.data;
};
