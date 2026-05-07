import { Button, Container } from "react-bootstrap";
import "./error.css";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeadset, faHouse } from "@fortawesome/free-solid-svg-icons";
export default function NotFound() {
  return (
    <div className="m-5 d-flex align-items-center justify-content-center">
      <Container className="text-center">
        <div
          className="p-2 d-flex align-items-center justify-content-center rounded-5 mx-auto"
          style={{
            backgroundColor: "var(--brand-main-3)",
            width: "fit-content",
          }}
        >
          <div
            className="rounded-circle bg-danger mx-2"
            style={{ width: 10, height: 10 }}
          ></div>
          SYSTEM STATUS: DISCONNECTED
        </div>
        <h1 className="big-404 m-0">404</h1>
        <h2 className=" tracking">NOT FOUND </h2>
        <h4>
          The frequency you are looking for has been <br /> decommissioned or
          moved. Our sensors cannot <br /> locate this specific hardware path.
        </h4>

        <Link to="/" className="btn btn-primary px-5 py-3 fw-bold m-2">
          <FontAwesomeIcon icon={faHouse} /> RETURN TO HOME PAGE
        </Link>
        <a
          href="mailto:algoronix@gmail.com"
          style={{ backgroundColor: "#E2E2E2" }}
          className="btn  px-5 py-3 fw-bold m-2 text-black"
        >
          <FontAwesomeIcon icon={faHeadset} /> GET TECHNICAL SUPPORT
        </a>
      </Container>
    </div>
  );
}
