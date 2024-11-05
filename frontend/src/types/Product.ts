// Common interface for product details used across different parts of the application.
export interface ProductBase {
  productTitle: string;
  description: string;
  price: number;
  quantity: number;
  categoryId: string;
  brandName: string;
}

// Interface for creating a product. Typically does not include an ID.
export interface ProductCreateDto extends ProductBase {}

// Interface for updating a product. Includes the ID to specify which product to update.
export interface ProductUpdateDto extends ProductBase {
  id: string;
  createdAt: string;
}

// Interface for reading or fetching a product.
export interface ProductReadDto extends ProductBase {
  createdAt: string | number | Date;
  id: string;
}

// Type for representing a list of products.
export type ProductList = ProductReadDto[];

// Type for representing the state of the product in Redux.
export interface ProductState {
  products: ProductList;
  product: ProductReadDto;
  loading: boolean;
  error: string | null;
  totalPages: number;
  page: number;
}
