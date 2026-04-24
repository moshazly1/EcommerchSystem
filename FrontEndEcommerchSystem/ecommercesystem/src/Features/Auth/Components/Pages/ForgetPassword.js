import { Button, Col, Container, Row, Form } from "react-bootstrap";
import { useForgotPassword } from "../../hooks/useForgetPassword";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import forgetpassword from "../../../../Assets/ForgetPassword.jpg";
import { faEnvelope, faLock } from "@fortawesome/free-solid-svg-icons";
import Loader from "../../../../Components/Loader/Loading";
import { Link } from "react-router-dom";

export default function ForgetPassword() {
  const { Message, handleSubmit, loading, HandelChange, isValid } =
    useForgotPassword();
  return (
    <>
      {loading && <Loader />}
      <div className="my-5 d-flex align-items-center">
        <Container>
          <Row className="align-items-center g-5">
            <Col xs={12} lg={6} className="order-2 order-lg-1">
              <h1
                className="fw-bold py-3 display-5 text-center"
                style={{ color: "var( --brand-500)" }}
              >
                <FontAwesomeIcon icon={faLock} />
              </h1>
              <h1 className="fw-bold py-3 display-5 text-center display-md-4">
                Forget password
              </h1>
              <h6 className="fw-bold text-muted  py-4">
                Enter your email to get reset code
              </h6>
              <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3">
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
                  {loading ? "Sending Email" : "Next"}
                </Button>

                <div
                  className={`${isValid ? "text-success" : "text-danger"} fs-6 mb-2`}
                >
                  {typeof Message === "string" &&
                    Message.split("\n").map((line, i) => (
                      <div key={i}>{line}</div>
                    ))}
                </div>

                <h4 className="text-center fw-bold">
                  <Link to="/login">Back to login</Link>
                </h4>
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
                src={forgetpassword}
                style={{ maxHeight: "600px", objectFit: "cover" }}
              ></img>
            </Col>
          </Row>
        </Container>
      </div>
    </>
  );
}
