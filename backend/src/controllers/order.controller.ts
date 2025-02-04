import { v4 } from "uuid";
import { OrderService } from "../services/order.service";
import { Request, Response } from "express";
import { Order } from "../interfaces/order";
import { Res } from "../interfaces/res";
import { getIdFromToken } from "../helpers/get_id_from_token";
import { Cart, CartItem } from "../interfaces/cart";
import { CartService } from "../services/cart.service";

export const createOrder = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const orderService = new OrderService();
  const userId = getIdFromToken(req);
  if (!userId) {
    return res.status(200).json({
      success: false,
      message: "Unauthorized",
      data: null,
    });
  }
  const response: Res<null> = await orderService.createOrder(userId);
  if (response.success) {
    return res.status(201).json(response);
  } else if (response.message !== "An error occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getAllOrders = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const orderService = new OrderService();
  const response: Res<Order[] | null> = await orderService.getAllOrders();
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getCompletedOrders = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const orderService = new OrderService();
  const response: Res<Order[] | null> = await orderService.getCompletedOrders();
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getIncompleteOrders = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const orderService = new OrderService();
  const response: Res<Order[] | null> =
    await orderService.getIncompleteOrders();
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getOrdersByProductId = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const orderService = new OrderService();
  const productId = req.params.productId;
  const response: Res<Order[] | null> = await orderService.getOrdersByProductId(
    productId
  );
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getOrdersByUserId = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const orderService = new OrderService();
  const userId = req.params.userId;
  if (!userId) {
    return res.status(200).json({
      success: false,
      message: "Invalid data",
      data: null,
    });
  }
  const response: Res<Order[] | null> = await orderService.getOrdersByUserId(
    userId
  );
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};

export const getUserOrders = async (
  req: Request,
  res: Response
): Promise<Response> => {
  const orderService = new OrderService();
  const userId = getIdFromToken(req);
  if (!userId) {
    return res.status(200).json({
      success: false,
      message: "Unauthorized",
      data: null,
    });
  }
  const response: Res<Order[] | null> = await orderService.getOrdersByUserId(
    userId
  );
  if (response.success) {
    return res.status(200).json(response);
  } else if (response.message !== "An Error Occurred") {
    return res.status(200).json(response);
  }
  return res.status(200).json(response);
};
