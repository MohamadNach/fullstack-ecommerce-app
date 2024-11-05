export enum UserRole {
  Admin = 1,
  User = 2,
}

// Common interface for product details used across different parts of the application.
export interface UserBase {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  role: UserRole;
}

// Interface for creating a product. Typically does not include an ID.
export interface UserCreateDto extends UserBase {
  password: string;
}

// Interface for updating a product. Includes the ID to specify which product to update.
export interface UserUpdateDto extends UserBase {
  id: string;
  createdAt: string;
}

// Interface for reading or fetching a product.
export interface UserReadDto extends UserBase {
  id: string;
  createdAt: string | number | Date;
}

// Type for representing a list of products.
export type ProductList = UserReadDto[];

// Type for representing the state of the product in Redux.
export interface UserState {
  users: UserReadDto[];
  user: UserReadDto | null;
  loading: boolean;
  error: string | null;
}
