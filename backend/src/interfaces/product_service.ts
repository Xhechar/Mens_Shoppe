import { Product } from "./product";
import { Res } from "./res";

export interface ProductServices {
  createProduct(product: Product): Promise<Res<null>>;
  createProducts(products: Product[]): Promise<Res<null>>;
  updateProduct(product: Product): Promise<Res<null>>;
  deleteProduct(id: string): Promise<Res<null>>;
  getProduct(productId: string): Promise<Res<Product | null>>;
  getAllProducts(): Promise<Res<Product[] | null>>;
  getProductsByType(tourType: string): Promise<Res<Product[] | null>>;
  getProductsByName(productName: string): Promise<Res<Product[] | null>>;
}
