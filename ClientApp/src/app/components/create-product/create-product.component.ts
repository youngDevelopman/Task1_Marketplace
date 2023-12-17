import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CreateProduct } from '../../models/CreateProduct';
import { ProductService } from '../../services/ProductService';

@Component({
  selector: 'app-product-creation',
  templateUrl: './create-product.component.html',
  styleUrls: ['.//create-product.component.css']
})
export class CreateProductComponent implements OnInit {
  constructor(private productService: ProductService) { }

  productForm = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    imageUrl: new FormControl('', [Validators.required, Validators.pattern(/(https?:\/\/.*\.(?:png|jpg))/i)]),
    rating: new FormControl(0, [Validators.required, Validators.pattern(/^[0-5]$/), Validators.max(5)]),
    price: new FormControl(0, [Validators.required, Validators.pattern(/^[0-9]*\.?[0-9]+$/)])
  });

  ngOnInit(): void {
  }

  successMessageVisible: boolean = false;

  onSubmit() {
    if (this.productForm.valid) {
      const product = {
        name: this.productForm.value.name,
        description: this.productForm.value.description,
        image: this.productForm.value.imageUrl,
        rating: this.productForm.value.rating,
        price: this.productForm.value.price
      } as CreateProduct;
      this.productService.addProduct(product).subscribe({
        next: (response) => {
          this.showSuccessMessage();
          this.productForm.reset();
        },
        error: (error) => {
          console.error('Error adding product:', error);
        }
      });
    }
  }

  private showSuccessMessage() {
    this.successMessageVisible = true;
    setTimeout(() => this.successMessageVisible = false, 3000);
  }
}
