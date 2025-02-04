import { Observable } from 'rxjs';
import { NewProduct, Product } from './product';
import { Res } from './res';

export interface ProductServices {
  createProduct(product: NewProduct): Observable<Res<null>>;
  createProducts(products: NewProduct[]): Observable<Res<null>>;
  updateProduct(product: Product): Observable<Res<null>>;
  deleteProduct(id: string): Observable<Res<null>>;
  getProduct(productId: string): Observable<Res<Product | null>>;
  getAllProducts(): Observable<Res<Product[] | null>>;
  getProductsByType(productType: string): Observable<Res<Product[] | null>>;
  getProductsByName(productName: string): Observable<Res<Product[] | null>>;
}
