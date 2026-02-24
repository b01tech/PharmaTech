import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  template: `
    <header class="bg-white shadow-sm h-16 flex items-center justify-between px-6">
      <h2 class="text-xl font-semibold text-gray-800">Painel Administrativo</h2>
      <div class="flex items-center space-x-4">
        <span class="text-sm text-gray-600">Admin User</span>
        <div
          class="h-8 w-8 rounded-full bg-blue-500 flex items-center justify-center text-white font-bold"
        >
          AU
        </div>
      </div>
    </header>
  `,
})
export class HeaderComponent {}
