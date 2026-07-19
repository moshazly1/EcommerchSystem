import { Container } from "react-bootstrap";
export default function CheckOut() {
  return (
    <Container className="d-flex align-content-center justify-content-between min-vh-100 mt-5">
      <h1 className="fw-bold" style={{ color: "var(--brand-600)" }}>
        PRECISION
      </h1>
      <div className="d-flex align-content-center justify-content-center">
        <div
          className="rounded-circle bg-primary d-flex align-items-center justify-content-center"
          style={{ width: "30px", height: "30px" }}
        >
          <span>1</span>
        </div>
        <p
          className="text-secondary fw-bold small mx-2 my-1 "
          style={{ letterSpacing: "2px" }}
        >
          SHIPPING
        </p>
      </div>
      <div className="d-flex align-content-center justify-content-center">
        <div
          className="rounded-circle bg-primary d-flex align-items-center justify-content-center"
          style={{ width: "30px", height: "30px" }}
        >
          <span>2</span>
        </div>
        <p
          className="text-secondary fw-bold small mx-2 my-1 "
          style={{ letterSpacing: "2px" }}
        >
          PAYMENT
        </p>
      </div>
      <div className="d-flex align-content-center justify-content-center">
        <div
          className="rounded-circle bg-primary d-flex align-items-center justify-content-center"
          style={{ width: "30px", height: "30px" }}
        >
          <span>3</span>
        </div>
        <p
          className="text-secondary fw-bold small mx-2 my-1 "
          style={{ letterSpacing: "2px" }}
        >
          CONFIRM
        </p>
      </div>
    </Container>
  );
}
