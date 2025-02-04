import { Observable } from 'rxjs';
import { Order } from './order';
import { Res } from './res';

export interface OrderServices {
  createOrder(userId: string): Observable<Res<null>>;
  getAllOrders(): Observable<Res<Order[] | null>>;
  getCompletedOrders(): Observable<Res<Order[] | null>>;
  getIncompleteOrders(): Observable<Res<Order[] | null>>;
  getOrdersByProductId(productId: string): Observable<Res<Order[] | null>>;
  getOrdersByUserId(userId: string): Observable<Res<Order[] | null>>;
}
