import { useParams } from "react-router-dom";
import useUsersID from "../Components/hooks/useUsers";
import useAuth from "../Features/Auth/hooks/useAuth";
import { Col, Card as CartPro, Container, Row } from "react-bootstrap";
import ImageProfile from "../Assets/2e648c43715eea402bd007e4e700afba0b609986.jpg";
import Card from "./Card";
export default function UserID() {
  const { auth } = useAuth();

  const EmailUser = auth.user.email;
  const name = auth.user.username;
  return (
    <Container>
      <div>
        <Row>
          <Col className="d-flex py-5">
            <div className="px-3">
              <img
                src={ImageProfile}
                alt="Profile"
                className="border rounded"
                style={{ width: "180px", height: "180px", objectFit: "cover" }}
              />
            </div>
            <div>
              <h1 className="fw-bold">{name}</h1>
              <p>{EmailUser}</p>
              <button
                className="btn text-light"
                style={{ backgroundColor: "var(--brand-700)" }}
              >
                Elite Member
              </button>
            </div>
          </Col>
          <Col className="d-flex align-items-center justify-content-center">
            <CartPro className="px-4 py-2 m-3 border-0">
              {" "}
              <p className="text-secondary fw-bold">Total Builds</p>
              <h2 className="fw-bold">12</h2>
            </CartPro>
            <CartPro className="px-4 py-2 m-3 border-0 ">
              <p className="text-secondary fw-bold">Active Orders</p>
              <h2 style={{ color: "var(  --brand-700)" }} className="fw-bold ">
                02
              </h2>
            </CartPro>
          </Col>
        </Row>
      </div>
    </Container>
  );
}
