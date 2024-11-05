//@ts-nocheck
import { createSlice } from '@reduxjs/toolkit';

import {
  createProductThunk,
  deleteProductThunk,
  fetchAllProductThunk,
  fetchProductThunk,
  updateProductThunk,
} from '../thunks/productThunks';
import { ProductState } from '../../types/Product';

const initialState: ProductState = {
  products: [],
  product: null,
  totalPages: 0,
  page: 0,
  loading: false,
  error: null,
};
const productSlice = createSlice({
  name: 'product',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllProductThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchAllProductThunk.fulfilled, (state, action) => {
        state.products = action.payload?.products;
        state.totalPages = action.payload?.total;
        state.loading = false;
      })
      .addCase(fetchAllProductThunk.rejected, (state, action) => {
        state.error = action.payload as string;
        state.loading = false;
      })
      .addCase(fetchProductThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchProductThunk.fulfilled, (state, action) => {
        state.product = action.payload;
        state.loading = false;
      })
      .addCase(fetchProductThunk.rejected, (state, action) => {
        state.error = action.payload as string; // Type assertion;
        state.loading = false;
      })
      .addCase(createProductThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(createProductThunk.fulfilled, (state, action) => {
        state.product = action.payload;
        state.loading = false;
      })
      .addCase(createProductThunk.rejected, (state, action) => {
        state.error = action.payload as string; // Type assertion;
        state.loading = false;
      })
      .addCase(updateProductThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(updateProductThunk.fulfilled, (state, action) => {
        state.product = action.payload;
        state.loading = false;
      })
      .addCase(updateProductThunk.rejected, (state, action) => {
        state.error = action.payload as string; // Type assertion;
        state.loading = false;
      })
      .addCase(deleteProductThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(deleteProductThunk.fulfilled, (state, action) => {
        state.product = action.payload;
        state.loading = false;
      })
      .addCase(deleteProductThunk.rejected, (state, action) => {
        state.error = action.payload as string; // Type assertion;
        state.loading = false;
      });
  },
});

export default productSlice.reducer;
