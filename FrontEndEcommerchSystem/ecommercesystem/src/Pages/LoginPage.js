import axios from "axios";
import { useContext, useState } from "react";
import { basURL, LOGIN } from "../API/api";
import {
  Button,
  Card,
  Col,
  Container,
  Form,
  InputGroup,
  Row,
} from "react-bootstrap";
import Logo from "../Assets/Gemini_Generated_Image_gtj53ggtj53ggtj5.png";
import Loader from "../Components/Loader/Loading";
import { useNavigate } from "react-router-dom";
import { User } from "./Context/Context";
import "./Form.css";
import imge1 from "../Assets/imge1.jpeg";
import { faEnvelope, faLock, faUser } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
export default function LoginPage() {
  const [FormData, setForm] = useState({ email: "", password: "" });
  const [Message, setMessage] = useState("");
  const [isValid, setIsvaled] = useState(false);
  const [loading, setLoading] = useState(false);
  const UserNow = useContext(User);
  const navigate = useNavigate();

  function HandelChange(e) {
    setForm({ ...FormData, [e.target.name]: e.target.value });
  }
  async function HandelSubmit(e) {
    e.preventDefault();
    setLoading(true);

    try {
      const res = await axios.post(`${basURL}${LOGIN}`, FormData, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true, // مهم لو هتتعامل مع HttpOnly cookies
      });

      const Token = res.data.token;
      const userDetalse = res.data;
      console.log(res.data);
      console.log(Token);
      UserNow.setAuth({ Token, userDetalse });

      if (res && res.data) {
        setMessage(res.data.mesage || "Login success");
        setIsvaled(res.data.isAuthentication || false);

        if (res.data.isAuthentication) {
          // navigate("/");
        }
      }
    } catch (err) {
      if (err.response && err.response.data) {
        setMessage(err.response.data.mesage || "Login failed");
      } else {
        setMessage("Server not reachable");
      }
      setIsvaled(false);
      console.log(err);
    } finally {
      setLoading(false);
    }
  }

  return (
    <>
      {loading && <Loader />}
      <div
        style={{
          minHeight: "100vh",

          background: "linear-gradient(to bottom, #006DFF 55%, #f8f9fa 55%)",
          display: "flex",
          alignItems: "center",
        }}
      >
        <Container>
          <Row className="g-0 ">
            <Col lg={6} className="d-none d-lg-block  "></Col>

            <Col lg={6} md={12} className=" bg-white p-4 rounded-5">
              <div className="text-center fw-bold fs-4">
                Welcome to
                <span
                  className="fw-bold fs-3"
                  style={{ color: "var(--brand-500)" }}
                >
                  {" "}
                  Tekstore
                </span>
              </div>
              <Row>
                <Col>
                  <div
                    style={{
                      maxWidth: "450px",
                      width: "100%",
                    }}
                  >
                    <div className=" mb-4">
                      <h2 className="fw-bold pb-1">Login</h2>
                    </div>

                    <Form onSubmit={HandelSubmit}>
                      <Form.Group className="mb-3">
                        <Form.Label className="small fw-bold text-black">
                          Email <span className="text-danger">*</span>
                        </Form.Label>
                        <div className="position-relative ">
                          <FontAwesomeIcon
                            icon={faUser}
                            className="position-absolute "
                            style={{
                              top: "50%",
                              left: "15px",
                              transform: "translateY(-50%)",
                              zIndex: 10,
                              color: "var(--brand-300)",
                            }}
                          />
                          <Form.Control
                            type="email"
                            name="email"
                            placeholder="Enter Your Email"
                            onChange={HandelChange}
                            required
                            className="py-3 border-0"
                            style={{
                              paddingLeft: "45px",
                              borderRadius: "12px",
                              backgroundColor: "var( --brand-400)",
                            }}
                          />
                        </div>
                      </Form.Group>

                      <Form.Group className="mb-2">
                        <Form.Label className="small fw-bold ">
                          Password <span className="text-danger">*</span>
                        </Form.Label>
                        <div className="position-relative ">
                          <FontAwesomeIcon
                            icon={faLock}
                            className="position-absolute "
                            style={{
                              top: "50%",
                              left: "15px",
                              transform: "translateY(-50%)",
                              zIndex: 10,
                              color: "var(--brand-300)",
                            }}
                          />
                          <Form.Control
                            type="password"
                            name="password"
                            placeholder="Enter Your Password"
                            onChange={HandelChange}
                            required
                            className="py-3 border-0 bg-light"
                            style={{
                              paddingLeft: "45px",
                              borderRadius: "12px",
                              backgroundColor: "var( --brand-400)",
                            }}
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
                        style={{
                          backgroundColor: "var(--brand-500)",
                          border: "none",
                        }}
                      >
                        Sign In
                      </Button>

                      <div
                        className={`${isValid ? "text-success" : "text-danger"} fs-6 mb-2`}
                      >
                        {Message}
                      </div>

                      <div className="text-center my-3 position-relative">
                        <hr />
                        <span className="position-absolute top-50 start-50 translate-middle bg-white px-3 text-muted small">
                          OR
                        </span>
                      </div>

                      <Button
                        variant="outline-light"
                        className="w-100 py-2 mb-4 text-dark shadow-sm border"
                        style={{ backgroundColor: "#fff" }}
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
                  </div>
                </Col>
                <Col className="d-flex align-items-center justify-content-center">
                  <div>
                    <img
                      style={{ width: "307px", height: "307px" }}
                      src={imge1}
                      alt="not-found"
                    />
                  </div>
                </Col>
              </Row>
            </Col>
          </Row>
        </Container>
      </div>
    </>
  );
}
