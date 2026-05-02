import { Col, Row } from "react-bootstrap";
import Sidebar from "../Components/SiadBar/SideBare";
import ProductCard from "../Components/cards/ProductCard";
import { usePCs } from "../Components/hooks/usePCs";

export default function PCs() {
  const { PCs, loading, error } = usePCs();

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
        <h1>PCs</h1>
        <p className="text-secondary pb-4">
          Curated engineering marvels for professionals who demand precision.
          Assembled with <br />
          enterprise-grade components for architectural rendering, scientific
          computing, and <br />
          creative production.
        </p>

        {loading && <p>Loading...</p>}
        {error && <p>Error loading products</p>}

        {/* Products Grid */}
        <Row className="g-4">
          {PCs.map((PCs) => (
            <Col key={PCs.id} xs={12} sm={6} md={4} lg={3}>
              <ProductCard item={PCs} conditionConfig={conditionConfig} />
            </Col>
          ))}
        </Row>
      </div>
    </div>
  );
}
