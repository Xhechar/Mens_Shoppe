import { Observable } from 'rxjs';
import { Res } from './res';
import { NewReview, Review } from './review';

export interface ReviewServices {
  createReview(review: NewReview): Observable<Res<null>>;
  updateReview(review: Review): Observable<Res<null>>;
  deleteReview(id: string): Observable<Res<null>>;
  getAllReviews(): Observable<Res<Review[] | null>>;
  getReviewsByUserId(userId: string): Observable<Res<Review[] | null>>;
  getReviewsByProductId(productId: string): Observable<Res<Review[] | null>>;
}
