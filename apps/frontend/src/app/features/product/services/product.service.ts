import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Product, ProductListResponse } from '../models/product.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly _http = inject(HttpClient);
  private readonly _apiUrl = `${environment.apiUrl}/api/products`;

  getAll(
    page: number = 1,
    pageSize: number = 10,
    categoryId?: string,
    subcategoryId?: string,
  ): Observable<ProductListResponse> {
    let params = new HttpParams().set('page', page.toString()).set('pageSize', pageSize.toString());

    if (categoryId) params = params.set('categoryId', categoryId);
    if (subcategoryId) params = params.set('subcategoryId', subcategoryId);

    return this._http.get<ProductListResponse>(this._apiUrl, { params });
  }

  getById(id: string): Observable<Product> {
    return this._http.get<Product>(`${this._apiUrl}/${id}`);
  }

  create(product: Omit<Product, 'id'>): Observable<Product> {
    return this._http.post<Product>(this._apiUrl, product);
  }

  update(id: string, product: Omit<Product, 'id'>): Observable<Product> {
    return this._http.put<Product>(`${this._apiUrl}/${id}`, product);
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._apiUrl}/${id}`);
  }
}
