export interface Subcategory {
  id: string;
  name: string;
  alias: string;
}

export interface Category {
  id: string;
  name: string;
  alias: string;
  subcategories: Subcategory[];
}

export interface CategoryListResponse {
  items: Category[];
  totalItems: number;
  totalPages: number;
  page: number;
  pageSize: number;
}
