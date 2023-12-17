import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateProduct } from "../models/CreateProduct";
import { Product } from "../models/Product";

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) { }

  searchProducts(searchText: string): Observable<{ id: string, name: string }[]> {
    const request = {
      searchText: searchText,
    }
    return this.http.post<{ id: string, name: string }[]>(`/api/products/search`, request);
  }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`/api/products`);
  }

  getProduct(id: string): Observable<Product> {
    return this.http.get<Product>(`/api/products/${id}`);
  }

  addProduct(request: CreateProduct): Observable<any> {
    return this.http.post(`/api/products`, request);
  }
}
