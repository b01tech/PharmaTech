import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  template: `
    <aside class="w-64 bg-gray-900 text-white min-h-screen flex flex-col">
      <div class="h-16 flex items-center justify-center border-b border-gray-800">
        <a routerLink="/" class="flex items-center justify-center">
          <img src="pharma-logo.png" alt="PharmaTech" class="h-36 w-auto p-2 cursor-pointer" />
        </a>
      </div>

      <nav class="flex-1 px-4 py-6 space-y-2">
        <a
          routerLink="/"
          class="block px-4 py-2.5 rounded hover:bg-gray-800 transition duration-200"
          routerLinkActive="bg-blue-600 text-white shadow-lg"
        >
          <div class="flex items-center space-x-2">
            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6"
              ></path>
            </svg>
            <span>Dashboard</span>
          </div>
        </a>

        <div class="pt-4 pb-2">
          <p class="px-4 text-xs font-semibold text-gray-500 uppercase tracking-wider">Gestão</p>
        </div>

        <a
          routerLink="/products"
          class="block px-4 py-2.5 rounded hover:bg-gray-800 transition duration-200"
          routerLinkActive="bg-blue-600 text-white shadow-lg"
        >
          <div class="flex items-center space-x-2">
            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4"
              ></path>
            </svg>
            <span>Produtos</span>
          </div>
        </a>

        <a
          routerLink="/categories"
          class="block px-4 py-2.5 rounded hover:bg-gray-800 transition duration-200"
          routerLinkActive="bg-blue-600 text-white shadow-lg"
        >
          <div class="flex items-center space-x-2">
            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z"
              ></path>
            </svg>
            <span>Categorias</span>
          </div>
        </a>
      </nav>

      <div class="p-4 border-t border-gray-800">
        <button
          class="flex items-center space-x-2 text-gray-400 hover:text-white w-full px-4 py-2 transition duration-200"
        >
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1"
            ></path>
          </svg>
          <span>Sair</span>
        </button>
      </div>
    </aside>
  `,
})
export class SidebarComponent {}
