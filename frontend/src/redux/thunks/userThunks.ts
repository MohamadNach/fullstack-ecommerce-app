import { createAsyncThunk } from '@reduxjs/toolkit';

import * as userService from '../../services/userService';
import { UserCreateDto, UserUpdateDto } from '../../types/User';

export const fetchUserThunk = createAsyncThunk(
  'user/fetchUser',
  async (userId: string, thunkAPI) => {
    try {
      const user = await userService.fetchUser(userId);
      return user;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const createUserThunk = createAsyncThunk(
  'user/create',
  async (userData: UserCreateDto, thunkAPI) => {
    try {
      const response = await userService.createUser(userData);
      return response.data;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const updateUserThunk = createAsyncThunk(
  'user/updateUser',
  async (
    { userId, userData }: { userId: string; userData: UserUpdateDto },
    thunkAPI
  ) => {
    try {
      const user = await userService.updateUser(userId, userData);
      return user;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);

export const deleteUserThunk = createAsyncThunk(
  'user/deletUser',
  async (userId: string, thunkAPI) => {
    try {
      const data = await userService.deleteUser(userId);
      return data;
    } catch (error) {
      if (error instanceof Error) {
        return thunkAPI.rejectWithValue(error.message);
      }
    }
  }
);
