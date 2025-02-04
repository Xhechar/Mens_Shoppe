import { PrismaClient } from "@prisma/client";
import { Res } from "../interfaces/res";
import { Product } from "../interfaces/product";
import { CartServices } from "../interfaces/cart_service";
import { Cart, CartItem } from "../interfaces/cart";

export class CartService implements CartServices {
  constructor(private prisma: PrismaClient = new PrismaClient()) {}

  async addToCart(cartItem: CartItem): Promise<Res<null>> {
    try {
      const product: Product | null = await this.prisma.product.findUnique({
        where: {
          id: cartItem.productId,
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
      const prevCartItem: CartItem | null = await this.prisma.cart.findFirst({
        where: {
          productId: cartItem.productId,
          userId: cartItem.userId,
        },
      });
      if (prevCartItem) {
        return {
          success: false,
          message: "Product already in cart",
          data: null,
        };
      }
      if (product.quantity < cartItem.productNumber) {
        return {
          success: false,
          message: "Product quantity cannot be more than available quantity",
          data: null,
        };
      }
      await this.prisma.cart.create({
        data: cartItem,
      });
      return {
        success: true,
        message: "Item successfully added to cart",
        data: null,
      };
    } catch (error: any) {
      if (error.message.includes("Foreign key constraint failed")) {
        return {
          success: false,
          message: "Invalid Product or User Id",
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

  async getCart(userId: string): Promise<Res<Cart[] | null>> {
    try {
      const cartItems: CartItem[] = await this.prisma.cart.findMany({
        where: {
          userId: userId,
        },
      });
      if (!cartItems) {
        return {
          success: false,
          message: "Cart is empty",
          data: null,
        };
      }
      const products: Product[] = await this.prisma.product.findMany({
        where: {
          id: {
            in: cartItems.map((item) => item.productId),
          },
        },
      });
      const cart: Cart[] = cartItems.map((item) => {
        const product: Product = products.find(
          (p) => p.id === item.productId
        ) as Product;
        return {
          id: item.id,
          product: product,
          productNumber: item.productNumber,
        };
      });
      return {
        success: true,
        message: "Cart successfully retrieved",
        data: cart,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async deleteFromCart(id: string): Promise<Res<null>> {
    try {
      await this.prisma.cart.delete({
        where: {
          id: id,
        },
      });
      return {
        success: true,
        message: "Item successfully removed from cart",
        data: null,
      };
    } catch (error: any) {
      if (error.message.includes("Record to delete does not exist.")) {
        return {
          success: false,
          message: "item not found in cart",
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

  async emptyCart(userId: string): Promise<Res<null>> {
    try {
      await this.prisma.cart.deleteMany({
        where: {
          userId: userId,
        },
      });
      return {
        success: true,
        message: "Cart successfully emptied",
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

  async incrementCartItem(id: string): Promise<Res<null>> {
    try {
      const cartItem: CartItem | null = await this.prisma.cart.findUnique({
        where: {
          id: id,
        },
      });
      if (!cartItem) {
        return {
          success: false,
          message: "Product not in cart",
          data: null,
        };
      }
      const product: Product | null = await this.prisma.product.findUnique({
        where: {
          id: cartItem.productId,
        },
      });
      if (!product) {
        return {
          success: false,
          message: "Product does not exist",
          data: null,
        };
      }
      if (cartItem.productNumber + 1 >= product.quantity) {
        return {
          success: false,
          message: "Product quantity cannot be more than available quantity",
          data: null,
        };
      }
      cartItem.productNumber += 1;
      await this.prisma.cart.update({
        where: {
          id: cartItem.id,
        },
        data: cartItem,
      });
      return {
        success: true,
        message: "Product quantity successfully incremented",
        data: null,
      };
    } catch {
      return {
        success: false,
        message: "An error occurred",
        data: null,
      };
    }
  }

  async decrementCartItem(id: string): Promise<Res<null>> {
    try {
      const cartItem: CartItem | null = await this.prisma.cart.findUnique({
        where: {
          id: id,
        },
      });
      if (!cartItem) {
        return {
          success: false,
          message: "Product not in cart",
          data: null,
        };
      }
      if (cartItem.productNumber < 2) {
        return {
          success: false,
          message: "Product quantity cannot be less than 1",
          data: null,
        };
      }
      cartItem.productNumber -= 1;
      await this.prisma.cart.update({
        where: {
          id: cartItem.id,
        },
        data: cartItem,
      });
      return {
        success: true,
        message: "Product quantity successfully decremented",
        data: null,
      };
    } catch {
      return {
        success: false,
        message: "An error occurred",
        data: null,
      };
    }
  }
}
