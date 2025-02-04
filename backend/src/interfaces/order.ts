export interface Order {
  id: string;
  productId: string;
  userId: string;
  productNumber: number;
  isOrderCompleted?: boolean;
  createdAt?: Date;
  updateAt?: Date;
}
