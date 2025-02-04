import { Router } from "express";
import {
  addToCart,
  decrementCartItem,
  deleteFromCart,
  emptyCart,
  getCart,
  incrementCartItem,
} from "../controllers/cart.controller";

const CartRouter = Router();

CartRouter.post("/add", addToCart);
CartRouter.get("/all", getCart);
CartRouter.delete("/delete/all", emptyCart);
CartRouter.delete("/delete/:id", deleteFromCart);
CartRouter.put("/increment/:id", incrementCartItem);
CartRouter.put("/decrement/:id", decrementCartItem);

export default CartRouter;
