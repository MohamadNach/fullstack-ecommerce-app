import { createAsyncThunk } from '@reduxjs/toolkit';

import * as categoryService from '../../services/categoryService';
import { CategoryCreateDto, CategoryUpdateDto } from '../../types/Category';

// Use unique action types for each thunk
export const fetchAllCategoriesThunk = createAsyncThunk(
  'category/fetchAllCategories',
  async () => {
    try {
      const data = await categoryService.fetchAllCategories();
      return data;
    } catch (error) {
      if (error instanceof Error) {
        return error.message;
      }
    }
  }
);

export const fetchCategoryThunk = createAsyncThunk(
  'category/fetchCategory',
  async (categoryId: string | undefined, thunkAPI) => {
    try {
      const category = await categoryService.fetchCategory(categoryId);
      return category;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const createCategoryThunk = createAsyncThunk(
  'category/createCategory',
  async (categoryData: CategoryCreateDto, thunkAPI) => {
    try {
      const category = await categoryService.createCategory(categoryData);
      return category;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.response?.data || error.message);
    }
  }
);

export const updateCategoryThunk = createAsyncThunk(
  'category/updateCategory',
  async (
    {
      categoryId,
      categoryData,
    }: { categoryId: string; categoryData: CategoryUpdateDto },
    thunkAPI
  ) => {
    try {
      const category = await categoryService.updateCategory(
        categoryId,
        categoryData
      );
      return category;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const deleteCategoryThunk = createAsyncThunk(
  'category/deleteCategory',
  async (categoryId: string, thunkAPI) => {
    try {
      const data = await categoryService.deleteCategory(categoryId);
      return data;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);
