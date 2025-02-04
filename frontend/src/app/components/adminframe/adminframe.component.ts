import { ClientsComponent } from './../clients/clients.component';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AdminVisualsComponent } from '../admin-visuals/admin-visuals.component';
import { OrdersComponent } from '../orders/orders.component';
import { ProductsAdminComponent } from '../products-admin/products-admin.component';
import { FormComponent } from '../form/form.component'
import { Product } from '../../interfaces/product';

@Component({
  selector: 'app-adminframe',
  standalone: true,
  imports: [RouterLink, CommonModule, AdminVisualsComponent, ProductsAdminComponent, ClientsComponent, OrdersComponent, FormComponent],
  templateUrl: './adminframe.component.html',
  styleUrl: './adminframe.component.css'
})
export class AdminframeComponent {
  selectedOption: string = 'dashboard';



  totalOrders = 50;
  pendingOrders = 20;
  completedOrders = 30;
  totalEarnings = 10000;

  editProduct: Product | null = null;

  selectOption(option: string) {
    this.selectedOption = option;
  }

  handleEditProductEvent(product: Product) {
    this.editProduct = product;
    this.selectOption('settings');
  }
  handleCancelEdit() {
    this.selectOption('products');
  }
}
