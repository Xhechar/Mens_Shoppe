import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

interface Order {
  clientProfile: string;
  name: string;
  product: string;
  date: string;
  status: 'delivered' | 'pending';
  quantity: number;
  amountPaid: number;
}

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
  selectedOrder: Order | null = null;
  orders: Order[] = [
    {
      clientProfile: 'https://example.com/client1.jpg',
      name: 'John Doe',
      product: 'Floral Tshirt',
      date: '2023-05-01',
      status: 'delivered',
      quantity: 2,
      amountPaid: 2400
    },
    {
      clientProfile: 'https://example.com/client2.jpg',
      name: 'Jane Smith',
      product: 'Striped Shirt',
      date: '2023-05-03',
      status: 'pending',
      quantity: 1,
      amountPaid: 1800
    },

  ];

  showPopup(order: Order) {
    this.selectedOrder = order;
  }

  closePopup() {
    this.selectedOrder = null;
  }
}
