import { Product } from './product';

export interface CartItem {
  productId: string;
  productNumber: number;
}

export interface Cart {
  id: string;
  product: Product;
  productNumber: number;
}
