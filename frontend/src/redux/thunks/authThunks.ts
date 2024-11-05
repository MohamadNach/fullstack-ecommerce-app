import { createAsyncThunk } from '@reduxjs/toolkit';

import * as authService from '../../services/authService';

export const loginUserThunk = createAsyncThunk(
  'auth/login',
  async (userCredientials: { email: string; password: string }, thunkAPI) => {
    try {
      const response = await authService.loginUser(userCredientials);

      return response;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(
        error.response?.data || 'Failed to login'
      );
    }
  }
);

export const logoutUserThunk = createAsyncThunk(
  'auth/logout',
  async (_, thunkAPI) => {
    try {
      const response = await authService.logoutUser();
      return response;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(
        error.response?.data || 'Failed to logout'
      );
    }
  }
);
