import { createSlice } from '@reduxjs/toolkit';

import { UserState } from '../../types/User';
import {
  fetchUserThunk,
  createUserThunk,
  updateUserThunk,
  deleteUserThunk,
} from '../thunks/userThunks';

const initialState: UserState = {
  users: [],
  user: null,
  loading: false,
  error: null,
};
const userSlice = createSlice({
  name: 'user',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchUserThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchUserThunk.fulfilled, (state, action) => {
        state.user = action.payload;
        state.loading = false;
      })
      .addCase(fetchUserThunk.rejected, (state, action) => {
        state.error = action.payload as string; // Type assertion;
        state.loading = false;
      })
      .addCase(createUserThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(createUserThunk.fulfilled, (state, action) => {
        state.user = action.payload;
        state.loading = false;
      })
      .addCase(createUserThunk.rejected, (state, action) => {
        state.error = action.payload as string; // Type assertion;
        state.loading = false;
      })
      .addCase(updateUserThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(updateUserThunk.fulfilled, (state, action) => {
        state.user = action.payload;
      })
      .addCase(updateUserThunk.rejected, (state, action) => {
        state.error = action.payload as string; // Type assertion;
        state.loading = false;
      })
      .addCase(deleteUserThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(deleteUserThunk.fulfilled, (state, action) => {
        state.user = action.payload;
      })
      .addCase(deleteUserThunk.rejected, (state, action) => {
        state.error = action.payload as string; // Type assertion;
        state.loading = false;
      });
  },
});

export default userSlice.reducer;
