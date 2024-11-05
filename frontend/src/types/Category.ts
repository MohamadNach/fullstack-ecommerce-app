// Common interface for category details used across different parts of the application.
export interface CategoryBase {
  categoryName: string;
  parentCategoryId: string | null;
  subCategories: SubCategoryReadDto[];
}

// Interface for creating a category. Typically does not include an ID.
export interface CategoryCreateDto extends CategoryBase {}

// Interface for updating a category. Includes the ID to specify which category to update.
export interface CategoryUpdateDto extends CategoryBase {
  id: string;
}

export interface CategoryReadDto extends CategoryBase {
  id: string;
}

// Type for representing a list of categories.
export type CategoryList = CategoryReadDto[];

// Type for representing the state of the category in Redux.
export interface CategoryState {
  categories: CategoryList;
  category: CategoryReadDto | null;
  loading: boolean;
  error: string | null;
}

// Interface for subcategories within a category
export interface SubCategoryReadDto extends CategoryBase {
  id: string;
  subCategories: SubCategoryReadDto[];
}
