import { Button, Container, Form, Nav, Navbar } from "react-bootstrap";
import Logo from "../../Assets/Logo.png";
import {
  faCartArrowDown,
  faSearch,
  faUser,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link, NavLink } from "react-router-dom";
import useAuth from "../../Features/Auth/hooks/useAuth";
import "./Header.css";

const NAV_LINKS = [
  { label: "Laptops", to: "/Laptop" },
  { label: "PCs", to: "/PCs" },
  { label: "Mice", to: "/Mice" },
  { label: "Accessories", to: "/Accessories" },
  // { label: "Support", to: "/Support" },
];

export default function Header() {
  const { auth } = useAuth();
  const isLoggedIn = !!auth?.accessToken;

  return (
    <Navbar expand="lg" className="bg-body-tertiary shadow-sm sticky-top">
      <Container>
        <Navbar.Brand as={Link} to="/" className="d-flex align-items-center">
          <img width={45} height={45} src={Logo} alt="not-found" />
          <span style={{ color: "var(--brand-500)" }} className="fw-bold fs-2">
            Tekstore
          </span>
        </Navbar.Brand>

        <Navbar.Toggle aria-controls="basic-navbar-nav" />

        <Navbar.Collapse id="basic-navbar-nav">
          {/* Nav Links */}
          <Nav className="ms-auto">
            {NAV_LINKS.map(({ label, to }) => (
              <Nav.Link
                key={to}
                as={NavLink}
                to={to}
                className="m-2 nav-link-animated"
                style={({ isActive }) => ({
                  fontWeight: isActive ? "700" : "400",
                  color: isActive ? "var(--brand-500)" : "#6B7280",
                })}
              >
                {label}
              </Nav.Link>
            ))}
          </Nav>

          {/* Search */}
          <div
            className="position-relative me-3 my-2"
            style={{ width: "250px" }}
          >
            <Form.Control
              type="text"
              placeholder="Search curated tech..."
              aria-label="Search"
              className="search-input"
              style={{ paddingLeft: "2.5rem" }}
            />
            <FontAwesomeIcon
              icon={faSearch}
              style={{
                position: "absolute",
                left: "0.75rem",
                top: "50%",
                transform: "translateY(-50%)",
                color: "gray",
                pointerEvents: "none",

                fontSize: "0.85rem",
                zIndex: 10,
              }}
            />
          </div>

          {/* Auth */}
          {isLoggedIn ? (
            <div className="d-flex align-items-center gap-2 my-2">
              <Nav.Link
                as={NavLink}
                to="/Profile"
                className="m-2 nav-link-animated"
                style={({ isActive }) => ({
                  fontWeight: isActive ? "700" : "400",
                  color: isActive ? "var(--brand-500)" : " #6B7280",
                })}
              >
                <FontAwesomeIcon className="fs-5 mx-2" icon={faUser} />
              </Nav.Link>
              <Nav.Link
                as={NavLink}
                to="/Card"
                className="m-2 nav-link-animated"
                style={({ isActive }) => ({
                  fontWeight: isActive ? "700" : "400",
                  color: isActive ? "var(--brand-500)" : "#6B7280",
                })}
              >
                <FontAwesomeIcon className="fs-5" icon={faCartArrowDown} />
              </Nav.Link>
            </div>
          ) : (
            <div className="d-flex gap-2 my-2">
              <Button as={Link} to="/login" className="mx-1 w-100">
                Login
              </Button>
              <Button as={Link} to="/register" className="mx-1 w-100">
                Register
              </Button>
            </div>
          )}
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}
