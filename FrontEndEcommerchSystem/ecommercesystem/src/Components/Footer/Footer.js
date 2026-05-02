import {
  faEarthAfrica,
  faEnvelope,
  faLink,
  faPhone,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Col, Container, Row } from "react-bootstrap";
import { Link, Links } from "react-router-dom";

export default function Footer() {
  return (
    <div className="mt-5" style={{ backgroundColor: "var(  --brand-main-2)" }}>
      <Container>
        <Row>
          <Col>
            <h6 style={{ color: "#1A1C1C", letterSpacing: "3px" }}>OUR TEAM</h6>
            <div className="mt-4">
              <span style={{ color: "#0054A9" }}>Mohamed El shazly</span>
              <span className="d-flex align-items-center ">
                <FontAwesomeIcon
                  style={{ color: "#6B7280" }}
                  className="p-2"
                  icon={faEarthAfrica}
                />
                <FontAwesomeIcon
                  style={{ color: "#6B7280" }}
                  className="p-2"
                  icon={faLink}
                />
                <FontAwesomeIcon
                  style={{ color: "#6B7280" }}
                  className="p-2"
                  icon={faEnvelope}
                />
              </span>
            </div>
            <div>
              <p style={{ color: "#0054A9" }}> Mazen Mohamed</p>
              <span className="d-flex align-items-center ">
                <FontAwesomeIcon
                  style={{ color: "#6B7280" }}
                  className="px-2"
                  icon={faEarthAfrica}
                />
                <FontAwesomeIcon
                  style={{ color: "#6B7280" }}
                  className="px-2"
                  icon={faLink}
                />
                <FontAwesomeIcon
                  style={{ color: "#6B7280" }}
                  className="px-2"
                  icon={faEnvelope}
                />
              </span>
            </div>
          </Col>
          <Col>
            <h6 style={{ color: "#1A1C1C", letterSpacing: "3px" }}>
              INFORMATION
            </h6>

            <div className="mt-4">
              <p style={{ color: "#6B7280" }}>Contact Info</p>
              <p style={{ color: "#6B7280" }}>Our Branches</p>
              <p style={{ color: "#6B7280" }}>Shipping Policy</p>
              <p style={{ color: "#6B7280" }}>Terms of Service</p>
            </div>
          </Col>
          <Col>
            <h6 style={{ color: "#1A1C1C", letterSpacing: "3px" }}>
              DIRECT LINE
            </h6>
            <div className="mt-4">
              <p style={{ color: "#6B7280" }}>
                <FontAwesomeIcon icon={faPhone} />
                01156391914
              </p>
              <p style={{ color: "#6B7280" }}>
                <FontAwesomeIcon icon={faPhone} /> 01285381616
              </p>
            </div>
          </Col>
          <Col>
            <h6 style={{ color: "#1A1C1C", letterSpacing: "3px" }}>
              SHOP CATEGORIES
            </h6>
            <div
              className="mt-4"
              style={{ display: "flex", flexDirection: "column", gap: "10px" }}
            >
              <Link
                to="/Laptop"
                style={{ color: "#6B7280", textDecoration: "none" }}
              >
                Laptop
              </Link>
              <Link
                to="/PCs"
                style={{ color: "#6B7280", textDecoration: "none" }}
              >
                PCs
              </Link>
              <Link
                to="/Mice"
                style={{ color: "#6B7280", textDecoration: "none" }}
              >
                Mice
              </Link>
              <Link
                to="/Accessories"
                style={{ color: "#6B7280", textDecoration: "none" }}
              >
                Accessories
              </Link>
              <Link
                to="/Support"
                style={{ color: "#6B7280", textDecoration: "none" }}
              >
                Support
              </Link>
            </div>
          </Col>
        </Row>
        <div class="border-top my-4 p-3">
          <h6 className="text-center  fs-6" style={{ color: "#6B7280" }}>
            © 2026 Silicon & Silica. All rights reserved. | Credits: Full Stack
            Developer Mohamed El shazly & UI/UX Designer Mazen Mohamed.
          </h6>
        </div>
      </Container>
    </div>
  );
}
