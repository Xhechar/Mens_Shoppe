export interface Product {
  id: string;
  name: string;
  price: number;
  description: string;
  type: string;
  size: string;
  quantity: number;
  stockLimit: number;
  images: string;
  createdAt?: Date;
  updatedAt?: Date;
  isDeleted?: boolean;
}
export interface ProductImagesArray {
  id: string;
  name: string;
  price: number;
  description: string;
  type: string;
  size: string;
  quantity: number;
  stockLimit: number;
  images: string[];
  createdAt?: Date;
  updatedAt?: Date;
  isDeleted?: boolean;
}
