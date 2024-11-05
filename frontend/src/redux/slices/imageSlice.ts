import { createSlice } from '@reduxjs/toolkit';
import {
  fetchProductImagesThunk,
  fetchProductMainImageThunk,
} from '../thunks/imageThunks';
import { ProductImageState } from '../../types/ProductImage';

const initialState: ProductImageState = {
  images: [],
  image: null,
  loading: false,
  error: null,
};

const imageSlice = createSlice({
  name: 'image',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchProductImagesThunk.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchProductImagesThunk.fulfilled, (state, action) => {
        state.images = action.payload;
        state.loading = false;
      })
      .addCase(fetchProductImagesThunk.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Failed to fetch images';
      })
      .addCase(fetchProductMainImageThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchProductMainImageThunk.fulfilled, (state, action) => {
        state.image = action.payload;
        state.loading = false;
      })
      .addCase(fetchProductMainImageThunk.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Failed to fetch image';
      });
  },
});

export default imageSlice.reducer;
