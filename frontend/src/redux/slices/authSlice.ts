import { createSlice } from '@reduxjs/toolkit';
import createStore from 'react-auth-kit/createStore';

import { loginUserThunk, logoutUserThunk } from '../thunks/authThunks';
import { AuthState } from '../../types/Auth';

export const authStore = createStore({
  authName: '_auth',
  authType: 'cookie',
  cookieDomain: window.location.hostname,
  cookieSecure: false, // localhost
  // cookieSecure: window.location.protocol === 'https:',
});

const initialState: AuthState = {
  token: '',
  authenticated: false,
  loading: false,
  error: null,
  userId: '' || null,
  userRole: 'User',
};

const authSlice = createSlice({
  name: 'auth',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(loginUserThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(loginUserThunk.fulfilled, (state, action) => {
        state.authenticated = true;
        state.loading = false;
        state.token = action.payload.token;
        state.userId = action.payload.userId;
        state.userRole = action.payload.userRole;
      })
      .addCase(loginUserThunk.rejected, (state, action) => {
        state.loading = false;
        console.error('Login failed:', action.payload);
      })
      .addCase(logoutUserThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(logoutUserThunk.fulfilled, (state) => {
        state.loading = false;
        state.authenticated = false;
        state.token = '';
        state.userId = '';
        state.userRole = 'User';
      })
      .addCase(logoutUserThunk.rejected, (state, action) => {
        console.error('Logout failed:', action.payload);
        state.loading = false;
        state.authenticated = false;
      });
  },
});

export default authSlice.reducer;
