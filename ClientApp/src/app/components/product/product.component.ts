import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ProductService } from '../../services/ProductService';
import { Product } from '../../models/Product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  product$: Observable<Product>;

  constructor(private route: ActivatedRoute, private productService: ProductService) { }

  ngOnInit(): void {
    this.product$ = this.route.params.pipe(
      switchMap(params => {
        const productId = params['id'];
        return this.productService.getProduct(productId);
      })
    );
  }

  // Convert numeric rating to star representation
  displayRating(rating: number): string {
    const roundedRating = Math.round(rating);
    return '⭐'.repeat(roundedRating);
  }
}
