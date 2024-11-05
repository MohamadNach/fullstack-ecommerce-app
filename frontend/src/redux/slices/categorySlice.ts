import { createSlice } from '@reduxjs/toolkit';
import {
  createCategoryThunk,
  deleteCategoryThunk,
  fetchAllCategoriesThunk,
  updateCategoryThunk,
} from '../thunks/categoryThunks';
import { CategoryState } from '../../types/Category';

const initialState: CategoryState = {
  categories: [],
  category: null,
  loading: false,
  error: null,
};

const categorySlice = createSlice({
  name: 'category',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllCategoriesThunk.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAllCategoriesThunk.fulfilled, (state, action) => {
        state.categories = action.payload;
        state.loading = false;
      })
      .addCase(fetchAllCategoriesThunk.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to fetch categories';
        state.loading = false;
      })
      .addCase(createCategoryThunk.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(createCategoryThunk.fulfilled, (state, action) => {
        state.categories.push(action.payload);
        state.loading = false;
      })
      .addCase(createCategoryThunk.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to create category';
        state.loading = false;
      })
      .addCase(updateCategoryThunk.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(updateCategoryThunk.fulfilled, (state, action) => {
        const index = state.categories.findIndex(
          (item) => item.id === action.payload.id
        );
        if (index >= 0) {
          state.categories[index] = action.payload;
        }
        state.loading = false;
      })
      .addCase(updateCategoryThunk.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to update category';
        state.loading = false;
      })
      .addCase(deleteCategoryThunk.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(deleteCategoryThunk.fulfilled, (state, action) => {
        state.categories = state.categories.filter(
          (item) => item.id !== action.payload
        );
        state.loading = false;
      })
      .addCase(deleteCategoryThunk.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to delete category';
        state.loading = false;
      });
  },
});

export default categorySlice.reducer;
