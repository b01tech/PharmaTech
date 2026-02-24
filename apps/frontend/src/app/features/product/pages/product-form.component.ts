import { Component, inject, input, signal, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ProductService } from '../services/product.service';
import { CategoryService } from '../../category/services/category.service';
import { Category, Subcategory } from '../../category/models/category.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  template: `
    <div class="container mx-auto p-4 max-w-2xl">
      <h1 class="text-2xl font-bold text-gray-800 mb-6">
        {{ isEditMode() ? 'Editar' : 'Novo' }} Produto
      </h1>

      <form
        [formGroup]="form"
        (ngSubmit)="onSubmit()"
        class="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4"
      >
        <!-- Name -->
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Nome</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            formControlName="name"
          />
          @if (
            form.get('name')?.invalid && (form.get('name')?.dirty || form.get('name')?.touched)
          ) {
            <p class="text-red-500 text-xs italic">Nome é obrigatório.</p>
          }
        </div>

        <!-- Alias -->
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="alias">Alias</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="alias"
            type="text"
            formControlName="alias"
          />
        </div>

        <!-- Description -->
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="description"
            >Descrição</label
          >
          <textarea
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="description"
            formControlName="description"
          ></textarea>
          @if (
            form.get('description')?.invalid &&
            (form.get('description')?.dirty || form.get('description')?.touched)
          ) {
            <p class="text-red-500 text-xs italic">Descrição é obrigatória.</p>
          }
        </div>

        <!-- SKU & Price -->
        <div class="flex mb-4 space-x-4">
          <div class="w-1/2">
            <label class="block text-gray-700 text-sm font-bold mb-2" for="sku">SKU</label>
            <input
              class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              id="sku"
              type="text"
              formControlName="sku"
            />
            @if (form.get('sku')?.invalid && (form.get('sku')?.dirty || form.get('sku')?.touched)) {
              <p class="text-red-500 text-xs italic">SKU é obrigatório.</p>
            }
          </div>
          <div class="w-1/2">
            <label class="block text-gray-700 text-sm font-bold mb-2" for="price">Preço</label>
            <input
              class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              id="price"
              type="number"
              step="0.01"
              formControlName="price"
            />
            @if (
              form.get('price')?.invalid && (form.get('price')?.dirty || form.get('price')?.touched)
            ) {
              <p class="text-red-500 text-xs italic">Preço é obrigatório.</p>
            }
          </div>
        </div>

        <!-- Category & Subcategory -->
        <div class="flex mb-6 space-x-4">
          <div class="w-1/2">
            <label class="block text-gray-700 text-sm font-bold mb-2" for="categoryId"
              >Categoria</label
            >
            <select
              class="shadow border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              id="categoryId"
              formControlName="categoryId"
              (change)="onCategoryChange()"
            >
              <option value="">Selecione...</option>
              @for (cat of categories(); track cat.id) {
                <option [value]="cat.id">{{ cat.name }}</option>
              }
            </select>
            @if (
              form.get('categoryId')?.invalid &&
              (form.get('categoryId')?.dirty || form.get('categoryId')?.touched)
            ) {
              <p class="text-red-500 text-xs italic">Categoria é obrigatória.</p>
            }
          </div>
          <div class="w-1/2">
            <label class="block text-gray-700 text-sm font-bold mb-2" for="subcategoryId"
              >Subcategoria</label
            >
            <select
              class="shadow border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              id="subcategoryId"
              formControlName="subcategoryId"
            >
              <option value="">Selecione...</option>
              @for (sub of subcategories(); track sub.id) {
                <option [value]="sub.id">{{ sub.name }}</option>
              }
            </select>
            @if (
              form.get('subcategoryId')?.invalid &&
              (form.get('subcategoryId')?.dirty || form.get('subcategoryId')?.touched)
            ) {
              <p class="text-red-500 text-xs italic">Subcategoria é obrigatória.</p>
            }
          </div>
        </div>

        <div class="flex items-center justify-between">
          <button
            class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline disabled:opacity-50 transition duration-300"
            type="submit"
            [disabled]="form.invalid || loading()"
          >
            {{ loading() ? 'Salvando...' : 'Salvar' }}
          </button>
          <a
            routerLink="/products"
            class="inline-block align-baseline font-bold text-sm text-blue-500 hover:text-blue-800 transition duration-300"
          >
            Cancelar
          </a>
        </div>
      </form>
    </div>
  `,
})
export class ProductFormComponent {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private router = inject(Router);

  id = input<string>();

  form = this.fb.group({
    name: ['', Validators.required],
    alias: [''],
    description: ['', Validators.required],
    sku: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(0.01)]],
    categoryId: ['', Validators.required],
    subcategoryId: ['', Validators.required],
  });

  loading = signal(false);
  isEditMode = signal(false);
  categories = signal<Category[]>([]);
  subcategories = signal<Subcategory[]>([]);

  constructor() {
    this.loadCategories();

    effect(() => {
      const productId = this.id();
      if (productId) {
        this.isEditMode.set(true);
        this.loadProduct(productId);
      }
    });
  }

  loadCategories() {
    this.categoryService.getAll(1, 100).subscribe((res) => {
      this.categories.set(res.items);
    });
  }

  onCategoryChange() {
    const categoryId = this.form.get('categoryId')?.value;
    if (categoryId) {
      this.loadSubcategories(categoryId);
    } else {
      this.subcategories.set([]);
    }
  }

  loadSubcategories(categoryId: string) {
    this.categoryService.getSubcategories(categoryId).subscribe((subs) => {
      this.subcategories.set(subs);
    });
  }

  loadProduct(id: string) {
    this.productService.getById(id).subscribe((prod) => {
      this.form.patchValue({
        name: prod.name,
        alias: prod.alias,
        description: prod.description,
        sku: prod.sku,
        price: prod.price,
        categoryId: prod.categoryId,
        subcategoryId: prod.subcategoryId,
      });

      // Load subcategories for the selected category
      if (prod.categoryId) {
        this.loadSubcategories(prod.categoryId);
      }
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    this.loading.set(true);
    const formValue = this.form.value;

    // Auto-generate alias if empty
    const finalAlias = formValue.alias || formValue.name?.toLowerCase().replace(/\s+/g, '-');

    const productData: any = {
      ...formValue,
      alias: finalAlias,
    };

    if (this.isEditMode()) {
      this.productService.update(this.id()!, productData).subscribe({
        next: () => {
          this.loading.set(false);
          alert('Produto atualizado com sucesso!');
          this.router.navigate(['/products']);
        },
        error: (err) => {
          this.loading.set(false);
          alert('Erro ao atualizar: ' + JSON.stringify(err));
        },
      });
    } else {
      this.productService.create(productData).subscribe({
        next: () => {
          this.loading.set(false);
          alert('Produto criado com sucesso!');
          this.router.navigate(['/products']);
        },
        error: (err) => {
          this.loading.set(false);
          alert('Erro ao criar: ' + JSON.stringify(err));
        },
      });
    }
  }
}
