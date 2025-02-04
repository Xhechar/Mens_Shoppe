import { Request, Response } from "express";
import { Res } from "../interfaces/res";
import { ProductService } from "../services/product.service";
import { Product, ProductImagesArray } from "../interfaces/product";
import { v4 } from "uuid";

export const createProduct = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const productService = new ProductService();
  const productInput: ProductImagesArray = req.body;
  const product: Product = {
    ...productInput,
    images: productInput.images.join(":::::"),
  };
  product.id = v4();
  if (
    !product.id ||
    !product.name ||
    !product.price ||
    !product.description ||
    !product.type ||
    !product.size ||
    !product.quantity ||
    !product.stockLimit ||
    !product.images
  ) {
    return res.status(200).json({
      success: false,
      message: "Invalid data",
      data: null,
    });
  }
  const response: Res<null> = await productService.createProduct(product);
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};
export const createProducts = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const productService = new ProductService();
  const productsInput: ProductImagesArray[] = req.body;
  const products: Product[] = productsInput.map((productInput) => {
    return {
      ...productInput,
      images: productInput.images.join(":::::"),
    };
  });
  products.filter(
    (product) =>
      product.name &&
      product.price &&
      product.description &&
      product.type &&
      product.size &&
      product.quantity &&
      product.stockLimit &&
      product.images
  );

  products.forEach((product) => {
    product.id = v4();
  });
  const response: Res<null> = await productService.createProducts(products);
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const updateProduct = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const productService = new ProductService();
  const productImagesArray: ProductImagesArray = req.body;
  const product: Product = {
    ...productImagesArray,
    images: productImagesArray.images.join(":::::"),
  };
  const response: Res<null> = await productService.updateProduct(product);
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const deleteProduct = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const productService = new ProductService();
  const id: string = req.params.id;
  if (!id) {
    return res.status(200).json({
      success: false,
      message: "Please provide an id",
      data: null,
    });
  }
  const response: Res<null> = await productService.deleteProduct(id);
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getProduct = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const productService = new ProductService();
  const id: string = req.params.id;
  const response: Res<Product | null> = await productService.getProduct(id);
  if (response.success && response.data) {
    const updatedResponse: Res<ProductImagesArray> = {
      ...response,
      data: {
        ...response.data,
        images: response.data.images.split(":::::"),
      },
    };
    return res.status(200).json(updatedResponse);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getAllProducts = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const productService = new ProductService();
  const response: Res<Product[] | null> = await productService.getAllProducts();
  if (response.success && response.data) {
    const updatedResponse: Res<ProductImagesArray[]> = {
      success: response.success,
      message: response.message,
      data: response.data.map((product) => {
        return {
          ...product,
          images: product.images.split(":::::"),
        };
      }),
    };
    return res.status(200).json(updatedResponse);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getProductsByTourType = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const productService = new ProductService();
  const tourType: string = req.params.tourType;
  const response: Res<Product[] | null> =
    await productService.getProductsByType(tourType);
  if (response.success && response.data) {
    const updatedResponse: Res<ProductImagesArray[]> = {
      success: response.success,
      message: response.message,
      data: response.data.map((product) => {
        return {
          ...product,
          images: product.images.split(":::::"),
        };
      }),
    };
    return res.status(200).json(updatedResponse);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getProductsByName = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const productService = new ProductService();
  const productName: string = req.params.productName;
  const response: Res<Product[] | null> =
    await productService.getProductsByName(productName);
  if (response.success && response.data) {
    const updatedResponse: Res<ProductImagesArray[]> = {
      success: response.success,
      message: response.message,
      data: response.data.map((product) => {
        return {
          ...product,
          images: product.images.split(":::::"),
        };
      }),
    };
    return res.status(200).json(updatedResponse);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};
