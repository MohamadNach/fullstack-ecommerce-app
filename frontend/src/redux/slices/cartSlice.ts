import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import {
  CartItemState,
  CartItemReadDto,
  CartItemCreateDto,
  CartItemUpdateDto,
} from '../../types/CartItem';
import { RootState } from '../store';

const initialState: CartItemState = {
  cartItems: [],
  cart: null,
  loading: false,
  error: null,
  totalPrice: 0,
};

const cartSlice = createSlice({
  name: 'cart',
  initialState,
  reducers: {
    addToCart: (state, action: PayloadAction<CartItemCreateDto>) => {
      const existingItem = state.cartItems.find(
        (item) =>
          item.product === action.payload.product &&
          item.size === action.payload.size &&
          item.color === action.payload.color
      );
      if (existingItem) {
        existingItem.quantity += action.payload.quantity;
        existingItem.totalPrice +=
          action.payload.unitPrice * action.payload.quantity;
      } else {
        const newItem: CartItemReadDto = {
          id: new Date().toISOString(), // Generate a unique ID
          ...action.payload,
          totalPrice: action.payload.unitPrice * action.payload.quantity,
        };
        state.cartItems.push(newItem);
      }
      state.totalPrice += action.payload.unitPrice * action.payload.quantity;
    },
    removeFromCart: (state, action: PayloadAction<string>) => {
      const itemToRemove = state.cartItems.find(
        (item) => item.id === action.payload
      );
      if (itemToRemove) {
        state.totalPrice -= itemToRemove.totalPrice;
        state.cartItems = state.cartItems.filter(
          (item) => item.id !== action.payload
        );
      }
    },
    updateCartItem: (state, action: PayloadAction<CartItemUpdateDto>) => {
      const itemToUpdate = state.cartItems.find(
        (item) => item.id === action.payload.id
      );
      if (itemToUpdate) {
        const quantityDifference =
          action.payload.quantity - itemToUpdate.quantity;
        itemToUpdate.quantity = action.payload.quantity;
        itemToUpdate.unitPrice = action.payload.unitPrice;
        itemToUpdate.totalPrice =
          action.payload.unitPrice * action.payload.quantity;
        state.totalPrice += quantityDifference * itemToUpdate.unitPrice;
      }
    },
    clearCart: (state) => {
      state.cartItems = [];
      state.totalPrice = 0;
    },
    setLoading: (state, action: PayloadAction<boolean>) => {
      state.loading = action.payload;
    },
    setError: (state, action: PayloadAction<string | null>) => {
      state.error = action.payload;
    },
  },
});
export const selectCartTotalQuantity = (state: RootState) =>
  state.cartR.cartItems.reduce((total, item) => total + item.quantity, 0);

export const {
  addToCart,
  removeFromCart,
  updateCartItem,
  clearCart,
  setLoading,
  setError,
} = cartSlice.actions;
export default cartSlice.reducer;
