import { Injectable } from '@angular/core';
import { CartServices } from '../interfaces/cart_service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart, CartItem } from '../interfaces/cart';
import { Res } from '../interfaces/res';

@Injectable({
  providedIn: 'root',
})
export class CartService implements CartServices {
  api: string = 'http://localhost:3000/cart';
  constructor(private http: HttpClient) {}

  addToCart(item: CartItem): Observable<Res<null>> {
    return this.http.post<Res<null>>(`${this.api}/add`, item);
  }

  getCart(): Observable<Res<Cart[] | null>> {
    return this.http.get<Res<Cart[] | null>>(`${this.api}/all`);
  }

  deleteFromCart(id: string): Observable<Res<null>> {
    return this.http.delete<Res<null>>(`${this.api}/delete/${id}`);
  }

  emptyCart(): Observable<Res<null>> {
    return this.http.delete<Res<null>>(`${this.api}/delete/all`);
  }

  incrementCartItem(id: string): Observable<Res<null>> {
    return this.http.put<Res<null>>(`${this.api}/increment/${id}`, null);
  }

  decrementCartItem(id: string): Observable<Res<null>> {
    return this.http.put<Res<null>>(`${this.api}/decrement/${id}`, null);
  }
}
