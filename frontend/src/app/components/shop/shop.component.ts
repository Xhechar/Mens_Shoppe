import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';
import { ProductService } from '../../services/product.service';
import { Product } from '../../interfaces/product';
import { Router, RouterLink } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { CartItem } from '../../interfaces/cart';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent, RouterLink],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css'
})

export class ShopComponent {
  minPrice: number = 500;
  maxPrice: number = 100000;
  progressWidth: string = '0%';
  products: Product[] = [];

  constructor(private productService: ProductService, private router: Router) {
    this.updateProgressWidth();
    this.productService.getAllProducts().subscribe(response => {
      console.log(response);
      if (response.success && response.data) {
        this.products = response.data;
      }
    });
  }
  updatePriceRange(): void {
    if (this.minPrice > this.maxPrice) {
      let temp = this.maxPrice;
      this.maxPrice = this.minPrice;
      this.minPrice = temp;
    }
    this.updateProgressWidth();
  }
  private updateProgressWidth(): void {
    let totalRange = 10000;
    let selectedRange = this.maxPrice - this.minPrice;
    let progressPercentage = (selectedRange / totalRange) * 100;
    this.progressWidth = `${progressPercentage}%`;
  }

  viewProduct(id: string): void {
    this.router.navigate(['/product', id]);
    console.log("Product Id:" + id);
  }
}
