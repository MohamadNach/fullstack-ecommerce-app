import { createAsyncThunk } from '@reduxjs/toolkit';

import * as productService from '../../services/productService';
import { ProductCreateDto, ProductUpdateDto } from '../../types/Product';
import { PaginationType } from '../../types/Pagination';

export const fetchAllProductThunk = createAsyncThunk(
  'product/fetchAllProducts',
  async ({ page, perPage }: PaginationType, thunkAPI) => {
    try {
      const products = await productService.fetchAllProduct({
        page,
        perPage,
      });
      return products;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const fetchLatestProductsThunk = createAsyncThunk(
  'product/fetchLetastProduct',
  async (productNumber: number, thunkAPI) => {
    try {
      const product = await productService.fetchLatestProducts(productNumber);
      return product;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const fetchProductThunk = createAsyncThunk(
  'product/fetchProduct',
  async (productId: string, thunkAPI) => {
    try {
      const product = await productService.fetchProduct(productId);
      return product;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const createProductThunk = createAsyncThunk(
  'product/createProduct',
  async (productData: ProductCreateDto, thunkAPI) => {
    try {
      const product = await productService.createProduct(productData);
      return product;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.response?.data || error.message);
    }
  }
);

export const updateProductThunk = createAsyncThunk(
  'product/updateProduct',
  async (
    {
      productId,
      productData,
    }: { productId: string; productData: ProductUpdateDto },
    thunkAPI
  ) => {
    try {
      const product = await productService.updateProduct(
        productId,
        productData
      );
      return product;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const deleteProductThunk = createAsyncThunk(
  'product/deleteProduct',
  async (productId: string, thunkAPI) => {
    try {
      const data = await productService.deleteProduct(productId);
      return data;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);
