import { Col, Container, Row } from "react-bootstrap";
import "./error.css";
import { Card } from "react-bootstrap";
import { faHouse, faLock } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";
export default function UnAuthorized() {
  return (
    <div
      style={{ backgroundColor: "var(--brand-main-3)" }}
      className="min-vh-100"
    >
      <Container>
        <div className="pt-5 ">
          <Row>
            <Col>
              <h1 className="big-404 m-0">401</h1>
              <h2 className=" tracking">UN-Authorized </h2>
            </Col>

            <Col className="d-flex flex-column align-items-center gap-3">
              <Card className="p-5">
                <FontAwesomeIcon
                  icon={faLock}
                  style={{ color: "#0050CB", fontSize: "90px" }}
                />
              </Card>
              <div className="btn btn-primary">SYSTEM LOCKOUT</div>
              <h1 className="fw-bold">401</h1>
              <span>ACCESS DENIED</span>
              <h1 className="fw-bold">
                Authentication required to access this secure sector.
              </h1>
              <p>
                Our Precision Curator protocols identify this area as
                restricted. To browse technical specifications, premium
                inventory, or manage your curation dashboard, a valid security
                handshake is required.
              </p>
              <div className="d-flex align-items-center justify-content-center">
                <Link
                  to="/login"
                  className="btn btn-primary px-5 py-3 fw-bold m-2"
                >
                  LOGIN TO AUTHENTICATE
                </Link>
                <Link
                  to="/"
                  style={{ backgroundColor: "#E2E2E2" }}
                  className="btn  px-5 py-3 fw-bold m-2 text-black"
                >
                  <FontAwesomeIcon icon={faHouse} /> RETURN TO Home
                </Link>
              </div>
            </Col>
          </Row>
        </div>
      </Container>
    </div>
  );
}
