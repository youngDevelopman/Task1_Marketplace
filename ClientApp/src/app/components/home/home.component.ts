import { Component } from '@angular/core';
import { Product } from '../../models/Product';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  products: Product[] = [
    { id: 1, name: 'Product 1', addedBy: 'Nazar Mazuryk', price: 50, description: 'Self care', image: 'https://m.media-amazon.com/images/W/MEDIAX_792452-T1/images/I/81qpIuOreGL._SL1500_.jpg' },
    { id: 2, name: 'Product 2', addedBy: 'Nazar Mazuryk', price: 75, description: 'Games', image: 'https://m.media-amazon.com/images/W/MEDIAX_792452-T1/images/I/610JSEeMobL._AC_SL1500_.jpg' },
    // Add more products as needed
  ];
}
