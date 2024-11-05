import { createAsyncThunk } from '@reduxjs/toolkit';

import * as imageService from '../../services/imageService';

export const fetchProductImagesThunk = createAsyncThunk(
  'productImage/fetchProductImages',
  async () => {
    try {
      const data = await imageService.fetchProductImages();
      return data;
    } catch (error) {
      if (error instanceof Error) {
        return error.message;
      }
    }
  }
);

export const fetchProductMainImageThunk = createAsyncThunk(
  'productImage/fetchProductMainImage',
  async (productId: string) => {
    try {
      const data = await imageService.fetchProductMainImage(productId);
      return data;
    } catch (error) {
      if (error instanceof Error) {
        return error.message;
      }
    }
  }
);
