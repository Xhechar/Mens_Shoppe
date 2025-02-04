import { Observable } from 'rxjs';
import { Cart, CartItem } from './cart';
import { Res } from './res';

export interface CartServices {
  addToCart(item: CartItem): Observable<Res<null>>;
  getCart(): Observable<Res<Cart[] | null>>;
  deleteFromCart(id: string): Observable<Res<null>>;
  emptyCart(): Observable<Res<null>>;
  incrementCartItem(id: string): Observable<Res<null>>;
  decrementCartItem(id: string): Observable<Res<null>>;
}
