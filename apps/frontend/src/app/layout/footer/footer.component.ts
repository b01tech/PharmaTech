import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule],
  template: `
    <footer class="bg-white border-t border-gray-200 py-4 px-6">
      <div class="container mx-auto text-center text-sm text-gray-500">
        &copy; {{ currentYear }} PharmaTech ERP. Todos os direitos reservados.
      </div>
    </footer>
  `,
})
export class FooterComponent {
  currentYear = new Date().getFullYear();
}
