import axios from "axios";
import { useState } from "react";
import { basURL, LOGIN } from "../API/api";
import { Button, Card, Col, Container, Form, Row } from "react-bootstrap";
import Logo from "../Assets/Gemini_Generated_Image_gtj53ggtj53ggtj5.png";
export default function LoginPage() {
  const [FormData, setForm] = useState({ email: "", password: "" });

  function HandelChange(e) {
    setForm({ ...FormData, [e.target.name]: e.target.value });
  }

  async function HandelSubmit(e) {
    e.preventDefault();
    try {
      const response = await axios.post(`${basURL}${LOGIN}`, FormData);
      console.log(response);
    } catch (err) {
      console.log(err.response);
    }
  }

  return (
    <Container className="d-flex align-items-center justify-content-center vh-100 bg-light">
      <Col lg={9} md={11} sm={12}>
        <Card
          className="shadow-lg border-0 overflow-hidden"
          style={{ borderRadius: "25px" }}
        >
          <Row className="g-0">
            <Col md={6} className="p-5 bg-white">
              <div className="text-center mb-4">
                <h2 className="fw-bold border-bottom border-primary border-3 d-inline-block pb-1">
                  Sign In
                </h2>
              </div>

              <Form onSubmit={HandelSubmit}>
                <Form.Group className="mb-3">
                  <Form.Label className="small fw-bold text-muted">
                    Email Address <span className="text-danger">*</span>
                  </Form.Label>
                  <Form.Control
                    type="email"
                    name="email"
                    placeholder="youremail@gmail.com"
                    onChange={HandelChange}
                    required
                    className="py-2"
                  />
                </Form.Group>

                <Form.Group className="mb-2">
                  <Form.Label className="small fw-bold text-muted">
                    Password <span className="text-danger">*</span>
                  </Form.Label>
                  <div className="position-relative">
                    <Form.Control
                      type="password"
                      name="password"
                      placeholder="****"
                      onChange={HandelChange}
                      required
                      className="py-2"
                    />
                  </div>
                </Form.Group>

                <div className="text-end mb-4">
                  <a
                    href="#"
                    className="small text-dark text-decoration-none fw-bold"
                  >
                    Forgot Password?
                  </a>
                </div>

                <Button
                  variant="primary"
                  type="submit"
                  className="w-100 py-2 mb-3 shadow-sm"
                  style={{ backgroundColor: "#1e5482" }}
                >
                  Sign In
                </Button>

                <div className="text-center my-3 position-relative">
                  <hr />
                  <span className="position-absolute top-50 start-50 translate-middle bg-white px-3 text-muted small">
                    OR
                  </span>
                </div>

                <Button
                  variant="outline-warning"
                  className="w-100 py-2 mb-4 text-dark shadow-sm border-light"
                  style={{ backgroundColor: "#fff9f0" }}
                >
                  <img
                    src="https://upload.wikimedia.org/wikipedia/commons/c/c1/Google_%22G%22_logo.svg"
                    alt="google"
                    width="20"
                    className="me-2"
                  />
                  Sign In with Google
                </Button>

                <div className="text-center mt-2">
                  <p className="small text-muted mb-1">
                    If this is your first time on our new site, reset the
                    password
                  </p>
                  <p className="small fw-bold">
                    Don't have an account ?{" "}
                    <a
                      href="/register"
                      className="text-primary text-decoration-none"
                    >
                      Create New Account
                    </a>
                  </p>
                </div>
              </Form>
            </Col>

            <Col
              md={6}
              className="d-none d-md-flex align-items-center justify-content-center"
              style={{ backgroundColor: "#f8faff" }}
            >
              <div className="text-center">
                <img src={Logo} alt="Sigma Logo" style={{ width: "200px" }} />

                <div
                  style={{
                    position: "absolute",
                    bottom: "-50px",
                    right: "-50px",
                    width: "300px",
                    height: "300px",
                    backgroundColor: "#e7f1ff",
                    borderRadius: "50%",
                    zIndex: -1,
                  }}
                ></div>
              </div>
            </Col>
          </Row>
        </Card>
      </Col>
    </Container>
  );
}
