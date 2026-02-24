export interface Product {
  id: string;
  name: string;
  alias: string;
  description: string;
  sku: string;
  price: number;
  categoryId: string;
  subcategoryId: string;
}

export interface ProductListResponse {
  items: Product[];
  totalItems: number;
  totalPages: number;
  page: number;
  pageSize: number;
}
