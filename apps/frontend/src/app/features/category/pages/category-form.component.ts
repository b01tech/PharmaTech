import { Component, inject, input, signal, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category, Subcategory } from '../models/category.model';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  template: `
    <div class="container mx-auto p-4 max-w-2xl">
      <h1 class="text-2xl font-bold text-gray-800 mb-6">
        {{ isEditMode() ? 'Editar' : 'Nova' }} Categoria
      </h1>

      <form
        [formGroup]="form"
        (ngSubmit)="onSubmit()"
        class="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4"
      >
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name"> Nome </label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            placeholder="Nome da Categoria"
            formControlName="name"
          />
          @if (
            form.get('name')?.invalid && (form.get('name')?.dirty || form.get('name')?.touched)
          ) {
            <p class="text-red-500 text-xs italic">Nome é obrigatório.</p>
          }
        </div>
        <div class="mb-6">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="alias"> Alias </label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
            id="alias"
            type="text"
            placeholder="Alias (opcional)"
            formControlName="alias"
          />
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
            routerLink="/categories"
            class="inline-block align-baseline font-bold text-sm text-blue-500 hover:text-blue-800 transition duration-300"
          >
            Cancelar
          </a>
        </div>
      </form>

      @if (isEditMode()) {
        <div class="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 mt-6">
          <h2 class="text-xl font-bold text-gray-800 mb-4">Subcategorias</h2>

          <div class="flex mb-4">
            <input
              #subName
              class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mr-2"
              placeholder="Nome da Subcategoria"
            />
            <button
              (click)="addSubcategory(subName.value); subName.value = ''"
              class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline transition duration-300"
            >
              Adicionar
            </button>
          </div>

          <ul class="divide-y divide-gray-200">
            @for (sub of subcategories(); track sub.id) {
              <li class="flex justify-between items-center py-3">
                <span class="text-gray-800">{{ sub.name }}</span>
                <button
                  (click)="removeSubcategory(sub.id)"
                  class="text-red-600 hover:text-red-800 text-sm font-semibold transition duration-300"
                >
                  Remover
                </button>
              </li>
            } @empty {
              <li class="text-gray-500 py-3">Nenhuma subcategoria cadastrada.</li>
            }
          </ul>
        </div>
      }
    </div>
  `,
})
export class CategoryFormComponent {
  private fb = inject(FormBuilder);
  private categoryService = inject(CategoryService);
  private router = inject(Router);

  // Input from route parameter 'id' (enabled by withComponentInputBinding)
  id = input<string>();

  form = this.fb.group({
    name: ['', Validators.required],
    alias: [''],
  });

  loading = signal(false);
  isEditMode = signal(false);
  subcategories = signal<Subcategory[]>([]);

  constructor() {
    effect(() => {
      const categoryId = this.id();
      if (categoryId) {
        this.isEditMode.set(true);
        this.loadCategory(categoryId);
        this.loadSubcategories(categoryId);
      }
    });
  }

  loadCategory(id: string) {
    this.categoryService.getById(id).subscribe((cat) => {
      this.form.patchValue({
        name: cat.name,
        alias: cat.alias,
      });
    });
  }

  loadSubcategories(id: string) {
    this.categoryService.getSubcategories(id).subscribe((subs) => {
      this.subcategories.set(subs);
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    this.loading.set(true);
    const { name, alias } = this.form.value;

    // Auto-generate alias if empty
    const finalAlias = alias || name?.toLowerCase().replace(/\s+/g, '-');

    if (this.isEditMode()) {
      this.categoryService.update(this.id()!, name!, finalAlias!).subscribe({
        next: () => {
          this.loading.set(false);
          alert('Categoria atualizada com sucesso!');
        },
        error: (err) => {
          this.loading.set(false);
          alert('Erro ao atualizar: ' + JSON.stringify(err));
        },
      });
    } else {
      this.categoryService.create(name!, finalAlias!).subscribe({
        next: (newCat) => {
          this.loading.set(false);
          // Navigate to edit mode to add subcategories if desired, or back to list
          if (confirm('Categoria criada! Deseja adicionar subcategorias agora?')) {
            this.router.navigate(['/categories', newCat.id, 'edit']);
          } else {
            this.router.navigate(['/categories']);
          }
        },
        error: (err) => {
          this.loading.set(false);
          alert('Erro ao criar: ' + JSON.stringify(err));
        },
      });
    }
  }

  addSubcategory(name: string) {
    if (!name) return;
    const alias = name.toLowerCase().replace(/\s+/g, '-');

    this.categoryService.createSubcategory(this.id()!, name, alias).subscribe({
      next: (newSub) => {
        this.subcategories.update((subs) => [...subs, newSub]);
      },
      error: (err) => alert('Erro ao adicionar subcategoria: ' + JSON.stringify(err)),
    });
  }

  removeSubcategory(subId: string) {
    if (confirm('Remover esta subcategoria?')) {
      this.categoryService.deleteSubcategory(this.id()!, subId).subscribe({
        next: () => {
          this.subcategories.update((subs) => subs.filter((s) => s.id !== subId));
        },
        error: (err) => alert('Erro ao remover subcategoria: ' + JSON.stringify(err)),
      });
    }
  }
}
