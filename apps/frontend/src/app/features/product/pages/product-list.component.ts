import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ProductService } from '../services/product.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="container mx-auto p-4">
      <div class="flex justify-between items-center mb-6">
        <h1 class="text-2xl font-bold text-gray-800">Produtos</h1>
        <a
          routerLink="/products/new"
          class="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded transition duration-300"
        >
          Novo Produto
        </a>
      </div>

      <div class="bg-white shadow-md rounded-lg overflow-hidden">
        <table class="min-w-full leading-normal">
          <thead>
            <tr>
              <th
                class="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider"
              >
                Nome
              </th>
              <th
                class="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider"
              >
                SKU
              </th>
              <th
                class="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider"
              >
                Preço
              </th>
              <th
                class="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider"
              >
                Ações
              </th>
            </tr>
          </thead>
          <tbody>
            @for (product of products(); track product.id) {
              <tr class="hover:bg-gray-50 transition duration-150">
                <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                  <p class="text-gray-900 font-medium">{{ product.name }}</p>
                  <p class="text-gray-500 text-xs truncate max-w-xs">{{ product.description }}</p>
                </td>
                <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                  <p class="text-gray-900">{{ product.sku }}</p>
                </td>
                <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                  <p class="text-gray-900 font-bold">{{ product.price | currency: 'BRL' }}</p>
                </td>
                <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                  <div class="flex space-x-3">
                    <a
                      [routerLink]="['/products', product.id, 'edit']"
                      class="text-blue-600 hover:text-blue-900 font-medium cursor-pointer"
                      >Editar</a
                    >
                    <button
                      (click)="deleteProduct(product.id)"
                      class="text-red-600 hover:text-red-900 font-medium cursor-pointer"
                    >
                      Excluir
                    </button>
                  </div>
                </td>
              </tr>
            } @empty {
              <tr>
                <td
                  colspan="4"
                  class="px-5 py-5 border-b border-gray-200 bg-white text-sm text-center text-gray-500"
                >
                  Nenhum produto encontrado.
                </td>
              </tr>
            }
          </tbody>
        </table>
      </div>

      @if (totalPages() > 1) {
        <div class="mt-4 flex justify-between items-center">
          <button
            [disabled]="page() === 1"
            (click)="changePage(page() - 1)"
            class="bg-white border border-gray-300 cursor-pointer hover:bg-gray-100 text-gray-700 font-semibold py-2 px-4 rounded disabled:opacity-50 disabled:cursor-not-allowed transition duration-300"
          >
            Anterior
          </button>
          <span class="text-gray-700"
            >Página <span class="font-bold">{{ page() }}</span> de
            <span class="font-bold">{{ totalPages() }}</span></span
          >
          <button
            [disabled]="page() === totalPages()"
            (click)="changePage(page() + 1)"
            class="bg-white border border-gray-300 cursor-pointer hover:bg-gray-100 text-gray-700 font-semibold py-2 px-4 rounded disabled:opacity-50 disabled:cursor-not-allowed transition duration-300"
          >
            Próxima
          </button>
        </div>
      }
    </div>
  `,
})
export class ProductListComponent {
  private productService = inject(ProductService);

  products = signal<Product[]>([]);
  page = signal(1);
  pageSize = signal(10);
  totalCount = signal(0);

  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize()));

  constructor() {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getAll(this.page(), this.pageSize()).subscribe({
      next: (response) => {
        this.products.set(response.items);
        this.totalCount.set(response.totalItems);
      },
      error: (err) => console.error('Erro ao carregar produtos', err),
    });
  }

  changePage(newPage: number) {
    if (newPage >= 1 && newPage <= this.totalPages()) {
      this.page.set(newPage);
      this.loadProducts();
    }
  }

  deleteProduct(id: string) {
    if (confirm('Tem certeza que deseja excluir este produto?')) {
      this.productService.delete(id).subscribe({
        next: () => this.loadProducts(),
        error: (err) => alert('Erro ao excluir produto: ' + JSON.stringify(err)),
      });
    }
  }
}
