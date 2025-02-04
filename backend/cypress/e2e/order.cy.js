import { user } from "./details";
const productsApi = "http://localhost:3000/products";
const cartApi = "http://localhost:3000/cart";
const orderApi = "http://localhost:3000/orders";
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
    });
  });

  it("should place an order", () => {
    cy.request({
      method: "POST",
      url: `${orderApi}/create`,
      headers: user.headers,
    }).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.success).to.be.true;
    });
  });

  it("should get orders", () => {
    cy.request({
      method: "GET",
      url: `${orderApi}/user`,
      headers: user.headers,
    }).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.success).to.be.true;
    });
  });
});
