import { Component, Input } from '@angular/core';
import { Product } from '../../models/Product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  @Input() product: Product = {
    id: 1, name: 'The XYZ Nail Clipper', rating: 5, addedBy: 'Nazar Mazuryk', price: 50, description: 'The XYZ Nail Clipper is a precision grooming tool designed for effortless and precise nail care. With its durable stainless steel construction, ergonomic design, and sharp cutting edges, this nail clipper ensures a smooth and clean trim every time. The compact size makes it ideal for on-the-go use, fitting comfortably in your hand for easy maneuvering. Whether at home or while traveling, the XYZ Nail Clipper is your trusted companion for maintaining well-groomed and healthy nails with ease. Upgrade your nail care routine with this essential tool that combines functionality, durability, and convenience in a sleek and stylish design.', image: 'https://m.media-amazon.com/images/W/MEDIAX_792452-T1/images/I/81qpIuOreGL._SL1500_.jpg'
  };

  // Convert numeric rating to star representation
  displayRating(rating: number): string {
    const roundedRating = Math.round(rating);
    return '⭐'.repeat(roundedRating);
  }
}
