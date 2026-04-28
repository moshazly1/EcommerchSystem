import { Button, Col, Container, Row, Form } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import ResetYourPassword from "../../../../Assets/ResetYourPassword.jpg";
import { faLock, faUnlock } from "@fortawesome/free-solid-svg-icons";
import Loader from "../../../../Components/Loader/Loading";
import useResetPassword from "../../hooks/useResetPassword";

export default function ResetPassword() {
  const {
    newPassword,
    setNewPass,
    ConfirmPassword,
    setConfirmPassword,
    loading,
    message,
    isValid,
    HandelFunction,
  } = useResetPassword();

  return (
    <>
      {loading && <Loader />}
      <div className="my-5 d-flex align-items-center">
        <Container>
          <Row className="align-items-center g-5">
            <Col xs={12} lg={6} className="order-2 order-lg-1">
              <h1
                className="fw-bold py-3 display-5 text-center"
                style={{ color: "var(--brand-500)" }}
              >
                <FontAwesomeIcon icon={faUnlock} />
              </h1>
              <h1 className="fw-bold py-3 display-5 text-center m-4">
                Reset Password
              </h1>

              <Form onSubmit={HandelFunction}>
                {/* ✅ New Password */}
                <Form.Group className="mb-5">
                  <div className="position-relative">
                    <FontAwesomeIcon
                      icon={faLock}
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
                      type="password"
                      placeholder="New Password"
                      value={newPassword}
                      onChange={(e) => setNewPass(e.target.value)}
                      required
                      className="py-3 border-0 bg-light"
                      style={{
                        paddingLeft: "45px",
                        borderRadius: "12px",
                        backgroundColor: "var(--brand-400)",
                      }}
                    />
                  </div>
                </Form.Group>

                {/* ✅ Confirm Password */}
                <Form.Group className="mb-2">
                  <div className="position-relative">
                    <FontAwesomeIcon
                      icon={faLock}
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
                      type="password"
                      placeholder="Confirm Password"
                      value={ConfirmPassword}
                      onChange={(e) => setConfirmPassword(e.target.value)}
                      required
                      className="py-3 border-0 bg-light"
                      style={{
                        paddingLeft: "45px",
                        borderRadius: "12px",
                        backgroundColor: "var(--brand-400)",
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
                  {loading ? "Loading..." : "Confirm"}
                </Button>

                {/* ✅ Message */}
                <div
                  className={`${isValid ? "text-success" : "text-danger"} fs-6 mb-2`}
                >
                  {typeof message === "string" &&
                    message
                      .split("\n")
                      .map((line, i) => <div key={i}>{line}</div>)}
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
                src={ResetYourPassword}
                style={{ maxHeight: "600px", objectFit: "cover" }}
              />
            </Col>
          </Row>
        </Container>
      </div>
    </>
  );
}
