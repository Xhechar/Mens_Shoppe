import { v4 } from "uuid";
import { Request, Response } from "express";
import { Res } from "../interfaces/res";
import { getIdFromToken } from "../helpers/get_id_from_token";
import { CartService } from "../services/cart.service";
import { Cart, CartItem } from "../interfaces/cart";

export const addToCart = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const cart: CartItem = req.body;

  if (!cart.productId && cart.productNumber) {
    return res.status(200).json({
      success: false,
      message: "Invalid data",
      data: null,
    });
  }
  cart.userId = getIdFromToken(req);
  if (!cart.userId) {
    return res.status(200).json({
      success: false,
      message: "Unauthorized",
      data: null,
    });
  }
  cart.id = v4();
  const cartService = new CartService();
  cart.userId = getIdFromToken(req);
  cart.id = v4();
  const response: Res<null> = await cartService.addToCart(cart);
  if (response.success) {
    return res.status(201).json(response);
  }
  return res.status(200).json(response);
};

export const getCart = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const userId: string = getIdFromToken(req);
  if (!userId) {
    return res.status(200).json({
      success: false,
      message: "Unauthorized",
      data: null,
    });
  }
  const cartService = new CartService();
  const response: Res<Cart[] | null> = await cartService.getCart(userId);
  if (response.success) {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const deleteFromCart = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const id: string = req.params.id;
  if (!id) {
    return res.status(200).json({
      success: false,
      message: "Invalid data",
      data: null,
    });
  }
  const cartService = new CartService();
  const response: Res<null> = await cartService.deleteFromCart(id);
  if (response.success) {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const emptyCart = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const userId: string = getIdFromToken(req);
  if (!userId) {
    return res.status(200).json({
      success: false,
      message: "Unauthorized",
      data: null,
    });
  }

  const cartService = new CartService();
  const response: Res<null> = await cartService.emptyCart(userId);
  if (response.success) {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const incrementCartItem = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const id: string = req.params.id;
  if (!id) {
    return res.status(200).json({
      success: false,
      message: "Invalid data",
      data: null,
    });
  }
  const cartService = new CartService();
  const response: Res<null> = await cartService.incrementCartItem(id);
  if (response.success) {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const decrementCartItem = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const id: string = req.params.id;
  if (!id) {
    return res.status(200).json({
      success: false,
      message: "Invalid data",
      data: null,
    });
  }
  const cartService = new CartService();
  const response: Res<null> = await cartService.decrementCartItem(id);
  if (response.success) {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};
