import { Col, Row } from "react-bootstrap";
import Sidebar from "../Components/SiadBar/SideBare";
import ProductCard from "../Components/cards/ProductCard";
import { useAccessoriers } from "../Components/hooks/useAccessories";
import { useState } from "react";

export default function Accessoriers() {
  const [page, setPage] = useState(1);
  const { Accessories, loading, error, totalPages } = useAccessoriers(page, 8);

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
        <nav className="d-flex justify-content-center mt-4">
          <ul className="pagination">
            {/* Previous */}
            <li className={`page-item ${page === 1 ? "disabled" : ""}`}>
              <button className="page-link" onClick={() => setPage(page - 1)}>
                &lt;
              </button>
            </li>

            {/* Pages */}
            {Array.from({ length: totalPages }, (_, i) => i + 1)
              .filter(
                (num) =>
                  num === 1 ||
                  num === totalPages ||
                  (num >= page - 1 && num <= page + 1),
              )
              .map((num, index, arr) => (
                <>
                  {index > 0 && arr[index - 1] !== num - 1 && (
                    <li key={`dots-${num}`} className="page-item disabled">
                      <span className="page-link">...</span>
                    </li>
                  )}
                  <li
                    key={num}
                    className={`page-item ${page === num ? "active" : ""}`}
                  >
                    <button className="page-link" onClick={() => setPage(num)}>
                      {num}
                    </button>
                  </li>
                </>
              ))}

            {/* Next */}
            <li
              className={`page-item ${page === totalPages ? "disabled" : ""}`}
            >
              <button className="page-link" onClick={() => setPage(page + 1)}>
                &gt;
              </button>
            </li>
          </ul>
        </nav>
      </div>
    </div>
  );
}
