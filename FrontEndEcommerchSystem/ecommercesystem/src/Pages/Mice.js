import { Col, Row } from "react-bootstrap";
import Sidebar from "../Components/SiadBar/SideBare";
import ProductCard from "../Components/cards/ProductCard";
import { useMice } from "../Components/hooks/useMice";
import { useState } from "react";

export default function Mice() {
  const [page, setPage] = useState(1);
  const { Mice, loading, error, totalPages } = useMice(page, 8);

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
