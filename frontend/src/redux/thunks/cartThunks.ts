import { createAsyncThunk } from '@reduxjs/toolkit';

import * as cartService from '../../services/cartService';
import { CartCreateDto, CartUpdateDto } from '../../types/Cart';

export const fetchCartThunk = createAsyncThunk(
  'product/fetchCart',
  async (cartId: string | undefined, thunkAPI) => {
    try {
      const cart = await cartService.fetchCart(cartId);
      return cart;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const createCartThunk = createAsyncThunk(
  'product/createCart',
  async (cartData: CartCreateDto, thunkAPI) => {
    try {
      const cart = await cartService.createCart(cartData);
      return cart;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.response?.data || error.message);
    }
  }
);

export const updateCartThunk = createAsyncThunk(
  'product/updateCart',
  async (
    { cartId, cartData }: { cartId: string; cartData: CartUpdateDto },
    thunkAPI
  ) => {
    try {
      const cart = await cartService.updateCart(cartId, cartData);
      return cart;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const deleteCartThunk = createAsyncThunk(
  'product/deleteCart',
  async (cartId: string, thunkAPI) => {
    try {
      const data = await cartService.deleteCart(cartId);
      return data;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);
