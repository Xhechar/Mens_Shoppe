import { Injectable } from '@angular/core';
import { OrderServices } from '../interfaces/order_service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Res } from '../interfaces/res';
import { Order } from '../interfaces/order';

@Injectable({
  providedIn: 'root',
})
export class OrderService implements OrderServices {
  api: string = 'http://localhost:3000/orders';
  constructor(private http: HttpClient) {}

  createOrder(userId: string): Observable<Res<null>> {
    return this.http.post<Res<null>>(`${this.api}/create`, userId);
  }

  getAllOrders(): Observable<Res<Order[] | null>> {
    return this.http.get<Res<Order[] | null>>(`${this.api}/all`);
  }

  getCompletedOrders(): Observable<Res<Order[] | null>> {
    return this.http.get<Res<Order[] | null>>(`${this.api}/completed`);
  }

  getIncompleteOrders(): Observable<Res<Order[] | null>> {
    return this.http.get<Res<Order[] | null>>(`${this.api}/incomplete`);
  }

  getOrdersByProductId(productId: string): Observable<Res<Order[] | null>> {
    return this.http.get<Res<Order[] | null>>(
      `${this.api}/product/${productId}`
    );
  }

  getOrdersByUserId(userId: string): Observable<Res<Order[] | null>> {
    return this.http.get<Res<Order[] | null>>(`${this.api}/user/${userId}`);
  }
}
