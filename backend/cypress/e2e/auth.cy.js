import { user } from "./details";
describe("Backend", () => {
  it("should register", () => {
    cy.request(
      "POST",
      "http://localhost:3000/auth/register",
      user.register
    ).then((res) => {
      const response = res.body;
      if (response.success && response.token) {
        user.token = response.token;
        user.headers.Authorization = `${user.token}`;
      }
      expect(res.status).to.eq(201);
    });
  });
  it("should login", () => {
    cy.request("POST", "http://localhost:3000/auth/login", user.login).then(
      (res) => {
        const response = res.body;
        if (response.success && response.token) {
          user.token = response.token;
          user.headers.Authorization = `${user.token}`;
        }
        expect(response.success).to.be.true;
      }
    );
  });
  it("should update name", () => {
    cy.request(
      "PUT",
      "http://localhost:3000/auth/update_details",
      user_details,
      {
        headers: user.headers,
      }
    ).then((res) => {
      const response = res.body;
      expect(res.status).to.eq(200);
    });
  });
  it("should update password", () => {
    cy.request(
      "PUT",
      "http://localhost:3000/auth/update_password",
      user_details,
      { headers: user.headers }
    ).then((res) => {
      const response = res.body;
      expect(res.status).to.eq(200);
    });
  });
});
