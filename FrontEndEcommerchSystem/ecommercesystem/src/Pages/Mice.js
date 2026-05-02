import { Col, Row } from "react-bootstrap";
import Sidebar from "../Components/SiadBar/SideBare";
import ProductCard from "../Components/cards/ProductCard";
import { useMice } from "../Components/hooks/useMice";

export default function Mice() {
  const { Mice, loading, error } = useMice();

  const conditionConfig = {
    0: { label: "NEW ARRIVAL", color: "#CC4204" },
    1: { label: "USED GRADE A", color: "#0000FF" },
    2: { label: "USED GRADE B", color: "#000000" },
    3: { label: "USED GRADE C", color: "#808080" },
  };

  return (
    <div className="d-flex min-vh-100">
      {/* Sidebar */}
      <Sidebar />

      {/* Main Content */}
      <div
        className="p-5 flex-grow-1"
        style={{ backgroundColor: "var(--brand-main)" }}
      >
        {/* 🔥 نفس الشكل القديم */}
        <h1>Mice</h1>
        <p className="text-secondary pb-4">
          Precision-crafted mice designed for professionals who demand accuracy
          and control. <br /> Built with high-performance sensors for design,
          engineering, and creative workflows.
        </p>

        {loading && <p>Loading...</p>}
        {error && <p>Error loading products</p>}

        {/* Products Grid */}
        <Row className="g-4">
          {Mice.map((Mice) => (
            <Col key={Mice.id} xs={12} sm={6} md={4} lg={3}>
              <ProductCard item={Mice} conditionConfig={conditionConfig} />
            </Col>
          ))}
        </Row>
      </div>
    </div>
  );
}
