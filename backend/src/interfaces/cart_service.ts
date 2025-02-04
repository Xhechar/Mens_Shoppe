import { Cart, CartItem } from "./cart";
import { Res } from "./res";

export interface CartServices {
  addToCart(item: CartItem): Promise<Res<null>>;
  getCart(userId: string): Promise<Res<Cart[] | null>>;
  deleteFromCart(id: string): Promise<Res<null>>;
  emptyCart(userId: string): Promise<Res<null>>;
  incrementCartItem(id: string): Promise<Res<null>>;
  decrementCartItem(id: string): Promise<Res<null>>;
}
