import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="container mx-auto p-4">
      <div class="flex justify-between items-center mb-6">
        <h1 class="text-2xl font-bold text-gray-800">Categorias</h1>
        <a
          routerLink="/categories/new"
          class="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded transition duration-300"
        >
          Nova Categoria
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
                Alias
              </th>
              <th
                class="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider"
              >
                Ações
              </th>
            </tr>
          </thead>
          <tbody>
            @for (category of categories(); track category.id) {
              <tr class="hover:bg-gray-50 transition duration-150">
                <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                  <p class="text-gray-900 whitespace-no-wrap font-medium">{{ category.name }}</p>
                </td>
                <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                  <p class="text-gray-500 whitespace-no-wrap">{{ category.alias }}</p>
                </td>
                <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                  <div class="flex space-x-3">
                    <a
                      [routerLink]="['/categories', category.id, 'edit']"
                      class="text-blue-600 hover:text-blue-900 font-medium cursor-pointer"
                      >Editar</a
                    >
                    <button
                      (click)="deleteCategory(category.id)"
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
                  colspan="3"
                  class="px-5 py-5 border-b border-gray-200 bg-white text-sm text-center text-gray-500"
                >
                  Nenhuma categoria encontrada.
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
            class="bg-white border border-gray-300 hover:bg-gray-100 text-gray-700 font-semibold py-2 px-4 rounded disabled:opacity-50 disabled:cursor-not-allowed transition duration-300"
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
            class="bg-white border border-gray-300 hover:bg-gray-100 text-gray-700 font-semibold py-2 px-4 rounded disabled:opacity-50 disabled:cursor-not-allowed transition duration-300"
          >
            Próxima
          </button>
        </div>
      }
    </div>
  `,
})
export class CategoryListComponent {
  private categoryService = inject(CategoryService);

  categories = signal<Category[]>([]);
  page = signal(1);
  pageSize = signal(10);
  totalCount = signal(0);

  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize()));

  constructor() {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getAll(this.page(), this.pageSize()).subscribe({
      next: (response) => {
        this.categories.set(response);
      },
      error: (err) => console.error('Erro ao carregar categorias', err),
    });
  }

  changePage(newPage: number) {
    if (newPage >= 1 && newPage <= this.totalPages()) {
      this.page.set(newPage);
      this.loadCategories();
    }
  }

  deleteCategory(id: string) {
    if (confirm('Tem certeza que deseja excluir esta categoria?')) {
      this.categoryService.delete(id).subscribe({
        next: () => this.loadCategories(),
        error: (err) => alert('Erro ao excluir categoria: ' + JSON.stringify(err)),
      });
    }
  }
}
