import Loader from "../../../Components/Loader/Loading";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faLock, faUser } from "@fortawesome/free-solid-svg-icons";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import imge1 from "../../../Assets/imge1.jpeg";
import { useRegister } from "../hooks/useRegister";

export default function Register() {
  const { HandelChange, HandelSubmit, Message, isValid, loading } =
    useRegister();
  return (
    <>
      {loading && <Loader />}
      <div className="my-5 d-flex align-items-center">
        <Container>
          <Row className="align-items-center g-5">
            <Col xs={12} lg={6} className="order-2 order-lg-1">
              <h3 className="fw-bold">Start for free</h3>
              <h1
                className="fw-bold py-3 display-5 display-md-4"
                style={{ color: "var( --brand-500)" }}
              >
                {" "}
                Create new account{" "}
              </h1>
              <h6 className="fw-bold text-muted  py-4">
                Already have an account?
                <Link
                  to={"/login"}
                  className="text-primary text-decoration-none"
                >
                  {" "}
                  Login
                </Link>
              </h6>
              <Form onSubmit={HandelSubmit}>
                <div className="row g-3">
                  {/* First Name */}
                  <div className="col-md-6">
                    <Form.Group>
                      <Form.Label className="small fw-bold text-black">
                        First Name <span className="text-danger">*</span>
                      </Form.Label>

                      <div className="position-relative">
                        <FontAwesomeIcon
                          icon={faUser}
                          className="position-absolute"
                          style={{
                            top: "50%",
                            left: "15px",
                            transform: "translateY(-50%)",
                            zIndex: 10,
                            color: "var(--brand-300)",
                          }}
                        />

                        <Form.Control
                          type="text"
                          name="firstName"
                          placeholder=" First Name"
                          onChange={HandelChange}
                          required
                          className="py-3 border-0"
                          style={{
                            paddingLeft: "45px",
                            borderRadius: "12px",
                            backgroundColor: "var(--brand-400)",
                          }}
                        />
                      </div>
                    </Form.Group>
                  </div>

                  {/* Last Name */}
                  <div className="col-md-6">
                    <Form.Group>
                      <Form.Label className="small fw-bold text-black">
                        Last Name <span className="text-danger">*</span>
                      </Form.Label>

                      <div className="position-relative">
                        <FontAwesomeIcon
                          icon={faUser}
                          className="position-absolute"
                          style={{
                            top: "50%",
                            left: "15px",
                            transform: "translateY(-50%)",
                            zIndex: 10,
                            color: "var(--brand-300)",
                          }}
                        />

                        <Form.Control
                          type="text"
                          name="lastName"
                          placeholder=" Last Name"
                          onChange={HandelChange}
                          required
                          className="py-3 border-0"
                          style={{
                            paddingLeft: "45px",
                            borderRadius: "12px",
                            backgroundColor: "var(--brand-400)",
                          }}
                        />
                      </div>
                    </Form.Group>
                  </div>
                </div>

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

                <Button
                  disabled={loading}
                  variant="primary"
                  type="submit"
                  className="w-100 py-2 my-5 shadow-sm fw-bold fs-5"
                  style={{
                    backgroundColor: "var(--brand-500)",
                    border: "none",
                  }}
                >
                  {loading ? "Creating account..." : "Sign up"}
                </Button>

                <div
                  className={`${isValid ? "text-success" : "text-danger"} fs-6 mb-2`}
                >
                  {typeof Message === "string" &&
                    Message.split("\n").map((line, i) => (
                      <div key={i}>{line}</div>
                    ))}
                </div>
              </Form>
            </Col>
            <Col
              xs={12}
              lg={6}
              className="d-none d-md-block order-1 order-lg-2"
            >
              <img
                className="img-fluid"
                alt="register-illustration"
                src={imge1}
                style={{ maxHeight: "600px", objectFit: "cover" }}
              ></img>
            </Col>
          </Row>
        </Container>
      </div>
    </>
  );
}
