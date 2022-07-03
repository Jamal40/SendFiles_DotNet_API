import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Product from '../types/Product';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  baseUrl = 'https://localhost:7264/api/Products';
  constructor(private clinet: HttpClient) {}

  addProduct(product: Product) {
    return this.clinet.post(this.baseUrl, product);
  }
}
