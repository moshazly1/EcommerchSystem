import { Button, Col, Container, Form, Row } from "react-bootstrap";
import Loader from "../../../Components/Loader/Loading";
import { Link } from "react-router-dom";
import "./Form.css";
import imge1 from "../../../Assets/imge1.jpeg";
import Laptop from "../../../Assets/Laptop.png";
import Computer from "../../../Assets/Computer.png";
import Speaker from "../../../Assets/Speaker.png";
import Mouse from "../../../Assets/Mouse.png";
import { faEnvelope, faLock, faUser } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faGoogle } from "@fortawesome/free-brands-svg-icons";
import { useLogin } from "../hooks/useLogin";

export default function LoginPage() {
  const { HandelChange, HandelSubmit, Message, formData, isValid, loading } =
    useLogin();
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
            <Col lg={6} className="d-none d-lg-block  ">
              <h1 className="fw-bold" style={{ color: "var(--brand-100)" }}>
                Sign in to Tekstore
              </h1>
              <Row>
                <Col>
                  <div
                    className="fw-medium fs-6 p-4 "
                    style={{ color: "var( --secondary-color)" }}
                  >
                    Your gateway to the latest tech. Shop powerful PCs, sleek
                    laptops, and premium accessories with ease.
                  </div>
                </Col>
                <Col>
                  <div>
                    <img
                      style={{ width: "125px", height: "125px" }}
                      alt="cat1"
                      className="m-1"
                      src={Laptop}
                    />
                    <img
                      style={{ width: "125px", height: "125px" }}
                      alt="cat2"
                      src={Computer}
                      className="m-1"
                    />
                    <img
                      style={{ width: "125px", height: "125px" }}
                      alt="cat3"
                      src={Mouse}
                      className="m-1"
                    />
                    <img
                      style={{ width: "125px", height: "125px" }}
                      alt="cat4"
                      src={Speaker}
                      className="m-1"
                    />
                  </div>
                </Col>
              </Row>
            </Col>

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
                      <h2 className="fw-bold pb-1 fs-1">Login</h2>
                    </div>

                    <Form onSubmit={HandelSubmit}>
                      <Form.Group className="mb-3">
                        <Form.Label className="small fw-bold text-black">
                          Email <span className="text-danger">*</span>
                        </Form.Label>
                        <div className="position-relative ">
                          <FontAwesomeIcon
                            icon={faEnvelope}
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
                          className="small  text-decoration-none fw-bold"
                        >
                          Forgot Password?
                        </a>
                      </div>

                      <Button
                        variant="primary"
                        type="submit"
                        className="w-100 py-2 mb-3 shadow-sm fw-bold fs-5"
                        style={{
                          backgroundColor: "var(--brand-500)",
                          border: "none",
                        }}
                      >
                        Log in
                      </Button>

                      <div
                        className={`${isValid ? "text-success" : "text-danger"} fs-6 mb-2`}
                      >
                        {Message}
                      </div>

                      <div className="text-center my-3 position-relative">
                        <hr />
                        <span className="position-absolute top-50 start-50 translate-middle bg-white px-3 text-muted small">
                          or
                        </span>
                      </div>

                      <Button
                        variant="outline-light"
                        className="w-100 p-3 mb-4 text-dark shadow-sm border fw-bold "
                        style={{
                          backgroundColor: "#fff",
                          fontSize: "14px",
                          backgroundColor: "var( --brand-200)",
                        }}
                      >
                        <FontAwesomeIcon
                          icon={faGoogle}
                          style={{
                            marginRight: "20px",
                            fontSize: "20px",
                            color: "var(--brand-500)",
                          }}
                        />
                        Continue with google
                      </Button>
                    </Form>
                  </div>
                </Col>
                <Col className="d-flex align-items-center justify-content-center">
                  {/* <div className="Circal">
                    <div className="up"></div>
                    <div className="down"></div>
                  </div> */}

                  <div>
                    <div className="m-2 mt-2">
                      <p className="small fw-bold text-muted">
                        Don't have an account ?{" "}
                        <Link
                          to="/register"
                          className="text-primary text-decoration-none"
                        >
                          <div>Sign Up</div>
                        </Link>
                      </p>
                    </div>
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
