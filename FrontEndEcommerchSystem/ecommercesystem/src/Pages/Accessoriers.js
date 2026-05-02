import { Col, Row } from "react-bootstrap";
import Sidebar from "../Components/SiadBar/SideBare";
import ProductCard from "../Components/cards/ProductCard";
import { useAccessoriers } from "../Components/hooks/useAccessories";

export default function Accessoriers() {
  const { Accessories, loading, error } = useAccessoriers();

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
        <h1>Accessories</h1>
        <p className="text-secondary pb-4">
          Curated accessories designed to enhance performance and productivity.
          Crafted for <br /> professionals who demand reliability, comfort, and
          precision in every detail.
        </p>

        {loading && <p>Loading...</p>}
        {error && <p>Error loading products</p>}

        {/* Products Grid */}
        <Row className="g-4">
          {Accessories.map((Accessories) => (
            <Col key={Accessories.id} xs={12} sm={6} md={4} lg={3}>
              <ProductCard
                item={Accessories}
                conditionConfig={conditionConfig}
              />
            </Col>
          ))}
        </Row>
      </div>
    </div>
  );
}
