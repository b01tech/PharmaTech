import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'products',
    pathMatch: 'full',
  },
  {
    path: 'categories',
    loadComponent: () =>
      import('./features/category/pages/category-list.component').then(
        (m) => m.CategoryListComponent,
      ),
  },
  {
    path: 'categories/new',
    loadComponent: () =>
      import('./features/category/pages/category-form.component').then(
        (m) => m.CategoryFormComponent,
      ),
  },
  {
    path: 'categories/:id/edit',
    loadComponent: () =>
      import('./features/category/pages/category-form.component').then(
        (m) => m.CategoryFormComponent,
      ),
  },
  {
    path: 'products',
    loadComponent: () =>
      import('./features/product/pages/product-list.component').then((m) => m.ProductListComponent),
  },
  {
    path: 'products/new',
    loadComponent: () =>
      import('./features/product/pages/product-form.component').then((m) => m.ProductFormComponent),
  },
  {
    path: 'products/:id/edit',
    loadComponent: () =>
      import('./features/product/pages/product-form.component').then((m) => m.ProductFormComponent),
  },
];
