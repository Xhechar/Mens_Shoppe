import { Prisma, PrismaClient } from "@prisma/client";
import { Product } from "../interfaces/product";
import { ProductServices } from "../interfaces/product_service";
import { Res } from "../interfaces/res";

export class ProductService implements ProductServices {
  constructor(private prisma: PrismaClient = new PrismaClient()) {}

  async createProduct(product: Product): Promise<Res<null>> {
    try {
      await this.prisma.product.create({
        data: product,
      });
      return {
        success: true,
        message: "Product successfully created",
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

  async createProducts(products: Product[]): Promise<Res<null>> {
    try {
      await this.prisma.product.createMany({
        data: products,
      });
      return {
        success: true,
        message: "Products successfully created",
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

  async updateProduct(product: Product): Promise<Res<null>> {
    try {
      await this.prisma.product.update({
        where: {
          id: product.id,
        },
        data: product,
      });
      return {
        success: true,
        message: "Product successfully updated",
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

  async deleteProduct(id: string): Promise<Res<null>> {
    try {
      await this.prisma.product.update({
        where: {
          id: id,
          isDeleted: false,
        },
        data: {
          isDeleted: true,
        },
      });
      return {
        success: true,
        message: "Product successfully deleted",
        data: null,
      };
    } catch (error: unknown) {
      if (error instanceof Prisma.PrismaClientKnownRequestError) {
        if (error.message.includes("Record to update not found")) {
          return {
            success: false,
            message: "Product not found",
            data: null,
          };
        }
      }

      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async getProduct(productId: string): Promise<Res<Product | null>> {
    try {
      const product: Product | null = await this.prisma.product.findUnique({
        where: {
          id: productId,
          isDeleted: false,
        },
      });
      if (!product) {
        return {
          success: false,
          message: "Product not found",
          data: null,
        };
      }
      delete product.isDeleted;
      return {
        success: true,
        message: "Product found",
        data: product,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async getAllProducts(): Promise<Res<Product[] | null>> {
    try {
      const products: Product[] = await this.prisma.product.findMany({
        where: {
          isDeleted: false,
        },
      });
      products.forEach((product) => {
        delete product.isDeleted;
      });
      return {
        success: true,
        message: "Products found",
        data: products,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async getProductsByType(type: string): Promise<Res<Product[] | null>> {
    try {
      const products = await this.prisma.product.findMany({
        where: {
          type: type,
          isDeleted: false,
        },
      });
      return {
        success: true,
        message: "Products found",
        data: products,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }

  async getProductsByName(productName: string): Promise<Res<Product[] | null>> {
    try {
      const products = await this.prisma.product.findMany({
        where: {
          isDeleted: false,
          name: {
            contains: productName,
          },
        },
      });
      return {
        success: true,
        message: "Products found",
        data: products,
      };
    } catch (error: any) {
      return {
        success: false,
        message: "An Error Occurred",
        data: null,
      };
    }
  }
}
