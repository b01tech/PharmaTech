import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Category, CategoryListResponse, Subcategory } from '../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private readonly _http = inject(HttpClient);
  private readonly _apiUrl = `${environment.apiUrl}/api/categories`;

  getAll(page: number = 1, pageSize: number = 10): Observable<Category[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    return this._http.get<Category[]>(this._apiUrl, { params });
  }

  getById(id: string): Observable<Category> {
    return this._http.get<Category>(`${this._apiUrl}/${id}`);
  }

  create(name: string, alias: string): Observable<Category> {
    return this._http.post<Category>(this._apiUrl, { name, alias });
  }

  update(id: string, name: string, alias: string): Observable<Category> {
    return this._http.put<Category>(`${this._apiUrl}/${id}`, { name, alias });
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._apiUrl}/${id}`);
  }

  getSubcategories(categoryId: string): Observable<Subcategory[]> {
    return this._http.get<Subcategory[]>(`${this._apiUrl}/${categoryId}/subcategories`);
  }

  createSubcategory(categoryId: string, name: string, alias: string): Observable<Subcategory> {
    return this._http.post<Subcategory>(`${this._apiUrl}/${categoryId}/subcategories`, {
      name,
      alias,
    });
  }

  deleteSubcategory(categoryId: string, subcategoryId: string): Observable<void> {
    return this._http.delete<void>(`${this._apiUrl}/subcategories/${subcategoryId}`);
  }
}
