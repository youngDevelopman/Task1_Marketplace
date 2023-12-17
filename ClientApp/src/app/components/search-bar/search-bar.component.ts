import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, catchError, startWith } from 'rxjs/operators';
import { ProductService } from '../../services/ProductService';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent {
  constructor(private productService: ProductService) { }
  searchControl = new FormControl();
  results$ = this.searchControl.valueChanges.pipe(
    startWith(''),
    debounceTime(300),
    switchMap(term => this.productService.searchProducts(term))
  );
}
