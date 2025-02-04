const api = "http://localhost:3000/products";
let products;
describe("Products", () => {
  it("should get products", () => {
    cy.request("GET", `${api}/all`).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.success).to.be.true;
      if (response.success) {
        products = response.data;
      }
    });
  });

  it("should get product by id", () => {
    cy.request("GET", `${api}/${products[0].id}`).then((res) => {
      expect(res.status).to.eq(200);
      const response = res.body;
      expect(response.success).to.be.true;
    });
  });
});
