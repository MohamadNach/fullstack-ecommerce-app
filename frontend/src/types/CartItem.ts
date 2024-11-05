import { CartReadDto } from './Cart';

// Common interface for cart item details used across different parts of the application.
export interface CartItemBase {
  product: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  size: string;
  color: string;
}

// Interface for creating a cart item. Typically does not include an ID.
export interface CartItemCreateDto extends CartItemBase {}

// Interface for updating a cart item. Includes the ID to specify which cart item to update.
export interface CartItemUpdateDto extends CartItemBase {
  id: string;
}

// Interface for reading or fetching a cart item.
export interface CartItemReadDto extends CartItemBase {
  id: string;
}

// Type for representing a list of cart items.
export type CartItemList = CartItemReadDto[];

// Type for representing the state of cart items in Redux.
export interface CartItemState {
  cartItems: CartItemList;
  cart: CartReadDto | null;
  loading: boolean;
  error: string | null;
  totalPrice: number;
}
