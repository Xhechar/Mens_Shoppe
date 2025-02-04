import { Router } from "express";

import {
  createReview,
  getReviews,
  getReviewsByUserId,
  getReviewsByProductId,
  updateReview,
  deleteReview,
} from "../controllers/review.controller";

const ReviewRouter = Router();

ReviewRouter.post("/create", createReview);
ReviewRouter.put("/update/:id", updateReview);
ReviewRouter.delete("/delete/:id", deleteReview);
ReviewRouter.get("/all", getReviews);
ReviewRouter.get("/user/:userId", getReviewsByUserId);
ReviewRouter.get("/product/:productId", getReviewsByProductId);

export default ReviewRouter;
