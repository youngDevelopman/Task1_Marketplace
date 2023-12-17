import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../models/Product';
import { ProductService } from '../../services/ProductService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private productService: ProductService) { }
  products$: Observable<Product[]>;
  ngOnInit(): void {
    this.products$ = this.productService.getProducts();
  }
}
