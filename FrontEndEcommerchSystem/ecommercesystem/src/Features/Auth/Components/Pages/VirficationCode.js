import { Col, Container, Row, Form, Button } from "react-bootstrap";

import VirfyImage from "../../../../Assets/VirefiactionImag.jpg";
import VirficationIcone from "../../../../Assets/VerficationIcone.png";

import useVerificationCode from "../../hooks/useVirificationCode";

import "./Verification.css";

export default function VerificationCode() {
  const {
    email,
    code,
    resendLoading,
    secondsLeft,
    loading,
    message,
    isValid,
    inputsRef,
    CODE_LENGTH,
    formatTime,
    maskEmail,
    handleChange,
    handleKeyDown,
    handleNext,
    handleResend,
  } = useVerificationCode();

  return (
    <div className="verification-page">
      <Container className="min-vh-100">
        <Row className="w-100 min-vh-100 align-items-center">
          <Col md={5} className="verification-content">
            <div className="verification-box">
              {/* Icon */}

              <img
                src={VirficationIcone}
                alt="Verification"
                className="verification-icon"
              />

              {/* Title */}

              <h1 className="verification-title">
                Enter {CODE_LENGTH}-digit
                <br />
                Verification code
              </h1>

              {/* Description */}

              <p className="verification-description">
                Code sent to <strong>{maskEmail(email)}</strong>
                <br />
                This code will expire in{" "}
                <strong>{formatTime(secondsLeft)}</strong>
              </p>

              {/* =========================
                  Code Inputs
              ========================= */}

              <div className="code-inputs" dir="ltr">
                {Array.from({ length: CODE_LENGTH }).map((_, index) => (
                  <div className="code-input-wrapper" key={index}>
                    <Form.Control
                      type="text"
                      inputMode="numeric"
                      autoComplete="one-time-code"
                      maxLength={1}
                      className="code-input"
                      value={code[index]}
                      onChange={(e) => handleChange(e.target.value, index)}
                      onKeyDown={(e) => handleKeyDown(e, index)}
                      ref={(el) => (inputsRef.current[index] = el)}
                    />
                  </div>
                ))}
              </div>

              {message && (
                <p
                  className={
                    isValid ? "verification-success" : "verification-error"
                  }
                >
                  {message}
                </p>
              )}

              <p className="resend-text">
                Don't receive code?{" "}
                {secondsLeft === 0 ? (
                  <span
                    className="resend-link"
                    onClick={resendLoading ? undefined : handleResend}
                  >
                    {resendLoading ? "Sending..." : "Re-send"}
                  </span>
                ) : (
                  <span className="resend-link-disabled">Re-send</span>
                )}
              </p>

              <Button
                className="next-button"
                onClick={handleNext}
                disabled={loading}
              >
                {loading ? "Verifying..." : "Next"}
              </Button>
            </div>
          </Col>

          <Col md={7} className="verification-image-container">
            <img
              src={VirfyImage}
              alt="Verify your identity"
              className="verification-image"
            />
          </Col>
        </Row>
      </Container>
    </div>
  );
}
