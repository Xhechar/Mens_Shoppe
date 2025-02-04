import { Prisma, PrismaClient } from "@prisma/client";
import { PrismaClientKnownRequestError } from "@prisma/client/runtime/library";
import { ReviewServices } from "../interfaces/review_service";
import { Review } from "../interfaces/review";
import { Res } from "../interfaces/res";
import { Order } from "../interfaces/order";
import { Product } from "../interfaces/product";

export class ReviewService implements ReviewServices {
  constructor(private prisma: PrismaClient = new PrismaClient()) {}

  async createReview(review: Review): Promise<Res<null>> {
    try {
      const product: Product | null = await this.prisma.product.findUnique({
        where: {
          id: review.productId,
          isDeleted: false,
        },
      });
      if (!product) {
        return {
          success: false,
          message: "Product does not exist",
          data: null,
        };
      }
      const orders: Order[] | null = await this.prisma.order.findMany({
        where: {
          userId: review.userId,
          productId: review.productId,
        },
      });
      if (!orders) {
        return {
          success: false,
          message: "User has not booked this product",
          data: null,
        };
      }
      const prevReviews: Review[] | null = await this.prisma.review.findMany({
        where: {
          userId: review.userId,
          productId: review.productId,
        },
      });
      if (prevReviews && prevReviews.length <= orders.length) {
        return {
          success: false,
          message: "User has already reviewed this product",
          data: null,
        };
      }
      orders.map((order) => {
        let isReviewed: boolean = false;
        prevReviews.map((prevReview) => {
          if (prevReview.orderId === order.id) {
            isReviewed = true;
          }
        });
        if (!isReviewed) {
          review.orderId = order.id;
        }
      });
      if (!review.orderId) {
        return {
          success: false,
          message: "User has already reviewed this product",
          data: null,
        };
      }
      await this.prisma.review.create({
        data: review,
      });
      return {
        success: true,
        message: "Review successfully created",
        data: null,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async getAllReviews(): Promise<Res<Review[] | null>> {
    try {
      const reviews = await this.prisma.review.findMany();
      return {
        success: true,
        message: "Reviews successfully retrieved",
        data: reviews,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async getReviewsByUserId(userId: string): Promise<Res<Review[] | null>> {
    try {
      const reviews = await this.prisma.review.findMany({
        where: {
          userId,
        },
      });
      return {
        success: true,
        message: "Reviews successfully retrieved",
        data: reviews,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async getReviewsByProductId(
    productId: string
  ): Promise<Res<Review[] | null>> {
    try {
      const product: Product | null = await this.prisma.product.findUnique({
        where: {
          id: productId,
        },
      });
      if (!product) {
        return {
          success: false,
          message: "Product does not exist",
          data: null,
        };
      }
      const reviews = await this.prisma.review.findMany({
        where: {
          productId,
        },
      });
      return {
        success: true,
        message: "Reviews successfully retrieved",
        data: reviews,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async updateReview(review: Review): Promise<Res<null>> {
    try {
      await this.prisma.review.update({
        where: {
          id: review.id,
        },
        data: review,
      });
      return {
        success: true,
        message: "Review successfully updated",
        data: null,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async deleteReview(id: string): Promise<Res<null>> {
    try {
      await this.prisma.review.delete({
        where: {
          id,
        },
      });
      return {
        success: true,
        message: "Review successfully deleted",
        data: null,
      };
    } catch (unknownError: unknown) {
      if (!(unknownError instanceof PrismaClientKnownRequestError)) {
        return {
          success: false,
          message: "An Error Occurred",
          data: null,
        };
      }
      const error: PrismaClientKnownRequestError =
        unknownError as PrismaClientKnownRequestError;
      if (error.message.includes("Record to delete does not exist.")) {
        return {
          success: false,
          message: "Product not found",
          data: null,
        };
      }
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }
}
