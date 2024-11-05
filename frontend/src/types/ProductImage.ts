// Common interface for image details used across different parts of the application.
export interface ProductImageBase {
  productId: string;
  imageURL: string;
  isDefault: boolean;
  imageText: string;
}

// Interface for creating an image. Typically does not include an ID.
export interface ImageCreateDto extends ProductImageBase {}

// Interface for updating an image. Includes the ID to specify which image to update.
export interface ImageUpdateDto extends ProductImageBase {
  id: string;
}

export interface ImageReadDto extends ProductImageBase {
  id: string;
}

// Type for representing a list of images.
export type ImageList = ImageReadDto[];

// Type for representing the state of the image in Redux.
export interface ProductImageState {
  images: ImageList;
  image: ImageReadDto | null;
  loading: boolean;
  error: string | null;
}
