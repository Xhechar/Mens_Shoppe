import { FormComponent } from './../form/form.component';
import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Subscription } from 'rxjs';
import { Product } from '../../interfaces/product';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-products-admin',
  standalone: true,
  imports: [CommonModule, FormComponent],
  templateUrl: './products-admin.component.html',
  styleUrls: ['./products-admin.component.css'],
  animations: [
    trigger('slideOutDelete', [
      transition(':leave', [
        style({ backgroundColor: 'lightcoral' }), // Initial background color
        animate('0.5s ease-out',
          style({ transform: 'translateX(-100%)', opacity: 0 }))
      ])
    ])
  ]
})
export class ProductsAdminComponent implements OnInit, OnDestroy {
  progressPercentage: number = 20;
  products: Product[] = [];
  productsSubscription: Subscription | undefined;

  @Output() editProductEvent = new EventEmitter<Product>();

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.loadProducts();
  }

  ngOnDestroy() {
    if (this.productsSubscription) {
      this.productsSubscription.unsubscribe();
    }
  }

  loadProducts() {
    this.productsSubscription = this.productService.getAllProducts().subscribe({
      next: (res) => {
        if (res.success) {
          this.products = res.data || [];
        } else {
          console.error('Failed to load products:', res.message);
        }
      },
      error: (error) => {
        console.error('Error loading products:', error);
      }
    });
  }

  handleEditClick(product: Product) {
    this.editProductEvent.emit(product);
  }
  handleDeleteClick(productId: string) {
    this.productService.deleteProduct(productId).subscribe({
      next: (res) => {
        if (res.success) {
          this.products = this.products.filter(product => product.id !== productId);
          console.log('Product deleted successfully');
          
        } else {
          console.error('Failed to delete product:', res.message);
        }
      },
      error: (error) => {
        console.error('Error deleting product:', error);
      }
    });
  }
}
