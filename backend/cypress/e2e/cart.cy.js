import { user } from "./details";
const productsApi = "http://localhost:3000/products";
const cartApi = "http://localhost:3000/cart";
let cartItems;
let products;

describe("Products", () => {
  it("should login", () => {
    cy.request("POST", "http://localhost:3000/auth/login", user.login).then(
      (res) => {
        const response = res.body;
        if (response.success && response.data.token) {
          user.token = response.data.token;
          user.headers.Authorization = `${user.token}`;
        }
        expect(response.success).to.be.true;
      }
    );
  });

  it("should get products", () => {
    cy.request("GET", `${productsApi}/all`).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.success).to.be.true;
      if (response.success) {
        products = response.data;
      }
    });
  });

  it("should add item to cart", () => {
    cy.request({
      method: "POST",
      url: `${cartApi}/add`,
      body: {
        productId: products[0].id,
        productNumber: 1,
      },
      headers: user.headers,
    }).then((res) => {
      expect(res.status).to.eq(201);
      const response = res.body;
      expect(response.success).to.be.true;
      expect(response.message).to.eq("Item successfully added to cart");
    });
  });

  it("should get cart", () => {
    cy.request({
      method: "GET",
      url: `${cartApi}/all`,
      headers: user.headers,
    }).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      if (response.success && response.data) {
        cartItems = response.data;
      }
      expect(response.success).to.be.true;
      expect(response.data).length.to.be.greaterThan(0);
    });
  });

  it("should increment product in cart", () => {
    cy.request({
      method: "PUT",
      url: `${cartApi}/increment/${cartItems[0].id}`,
      headers: user.headers,
    }).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.success).to.be.true;
    });
  });

  it("should decrement product in cart", () => {
    cy.request({
      method: "PUT",
      url: `${cartApi}/decrement/${cartItems[0].id}`,
      headers: user.headers,
    }).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.success).to.be.true;
    });
  });

  it("should delete item from cart", () => {
    cy.request({
      method: "DELETE",
      url: `${cartApi}/delete/${cartItems[0].id}`,
      headers: user.headers,
    }).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.success).to.be.true;
      expect(response.message).to.eq("Item successfully removed from cart");
    });
  });

  it("should empty cart", () => {
    cy.request({
      method: "DELETE",
      url: `${cartApi}/delete/all`,
      headers: user.headers,
    }).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.message).to.eq("Cart successfully emptied");
      expect(response.success).to.be.true;
    });
  });
});
