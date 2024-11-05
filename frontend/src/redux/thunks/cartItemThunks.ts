import { createAsyncThunk } from '@reduxjs/toolkit';

import * as cartItemService from '../../services/cartItemService';
import { CartItemCreateDto, CartItemUpdateDto } from '../../types/CartItem';

export const fetchCartItemThunk = createAsyncThunk(
  'product/fetchCartItem',
  async (userId: string, thunkAPI) => {
    try {
      const cartItem = await cartItemService.fetchCartItem(userId);
      return cartItem;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const createCartItemThunk = createAsyncThunk(
  'product/createCartItem',
  async (cartItemData: CartItemCreateDto, thunkAPI) => {
    try {
      const cartItem = await cartItemService.createCartItem(cartItemData);
      return cartItem;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.response?.data || error.message);
    }
  }
);

export const updateCartItemThunk = createAsyncThunk(
  'product/updateCartItem',
  async (
    {
      cartItemId,
      cartItemData,
    }: { cartItemId: string; cartItemData: CartItemUpdateDto },
    thunkAPI
  ) => {
    try {
      const cartItem = await cartItemService.updateCartItem(
        cartItemId,
        cartItemData
      );
      return cartItem;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const deleteCartItemThunk = createAsyncThunk(
  'product/deleteCartItem',
  async (cartItemId: string, thunkAPI) => {
    try {
      const data = await cartItemService.deleteCartItem(cartItemId);
      return data;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);
