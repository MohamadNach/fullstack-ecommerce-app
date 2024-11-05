import { createSlice } from '@reduxjs/toolkit';

import { CartItemState } from '../../types/CartItem';
import {
  createCartItemThunk,
  deleteCartItemThunk,
  fetchCartItemThunk,
  updateCartItemThunk,
} from '../thunks/cartItemThunks';

const initialState: CartItemState = {
  cartItems: [],
  cart: null,
  loading: false,
  error: null,
  totalPrice: 0,
};

const cartItemSlice = createSlice({
  name: 'cartItem',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCartItemThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchCartItemThunk.fulfilled, (state, action) => {
        state.cartItems = action.payload;
        state.loading = false;
      })
      .addCase(fetchCartItemThunk.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to fetch cart';
        state.loading = false;
      })
      .addCase(createCartItemThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(createCartItemThunk.fulfilled, (state, action) => {
        state.cartItems = action.payload;
        state.loading = false;
      })
      .addCase(createCartItemThunk.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to fetch cart';
        state.loading = false;
      })
      .addCase(updateCartItemThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(updateCartItemThunk.fulfilled, (state, action) => {
        const index = state.cartItems.findIndex(
          (cartItem) => cartItem.id === action.payload.id
        );
        if (index >= 0) {
          state.cartItems[index] = action.payload;
        }
      })
      .addCase(updateCartItemThunk.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to fetch cart';
        state.loading = false;
      })
      .addCase(deleteCartItemThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(deleteCartItemThunk.fulfilled, (state, action) => {
        state.cartItems = state.cartItems.filter(
          (item) => item.id !== action.payload
        );
      })
      .addCase(deleteCartItemThunk.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to fetch cart';
        state.loading = false;
      });
  },
});

export default cartItemSlice.reducer;
