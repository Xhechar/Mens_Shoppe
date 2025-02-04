export interface Review {
  id: string;
  productId: string;
  userId: string;
  orderId: string;
  rating: number;
  comment: string;
  createdAt: Date;
  updatedAt: Date;
}
export interface NewReview {
  productId: string;
  rating: number;
  comment: string;
}
