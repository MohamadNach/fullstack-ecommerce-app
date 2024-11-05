import { ProductReadDto } from './Product';

// Common interface for cart details used across different parts of the application.
export interface CartBase {
  cartItems: ProductReadDto[];
  cartTotalQuantity: number;
  cartTotalAmount: number;
}

// Interface for creating a cart. Typically does not include an ID.
export interface CartCreateDto extends CartBase {}

// Interface for updating a cart. Includes the ID to specify which cart to update.
export interface CartUpdateDto extends CartBase {
  id: string;
}

// Interface for reading or fetching a cart.
export interface CartReadDto extends CartBase {
  id: string;
}

// Type for representing a list of carts.
export type CartList = CartReadDto[];

// Type for representing the state of the cart in Redux.
export interface CartState {
  cartItems: CartList;
  cartTotalQuantity: number;
  cartTotalAmount: number;
  loading: boolean;
  error: string | null;
}
