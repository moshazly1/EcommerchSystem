import { Col, Row } from "react-bootstrap";
import ProductCard from "../../Components/cards/ProductCard";
import useGetAllWhiteList from "../../Components/hooks/useGetAllWhiteList";
import { useProfile } from "../../Context/ProfileContext";
import { useEffect } from "react";

export default function WhitsList() {
  const { WList, loading } = useGetAllWhiteList();
  const { setStats } = useProfile();
  console.log(WList);
  const conditionConfig = {
    0: { label: "NEW ARRIVAL", color: "#CC4204" },
    1: { label: "USED GRADE A", color: "#0000FF" },
    2: { label: "USED GRADE B", color: "#000000" },
    3: { label: "USED GRADE C", color: "#808080" },
  };
  useEffect(() => {
    setStats(
      <div className="d-flex flex-column justify-content-center">
        <h1 className="fw-bold">Your Curated Collection</h1>
        <p className="text-secondary">
          A precision-selected list of electronics you're watching.
        </p>
      </div>,
    );
  }, []);
  return (
    <div className="d-flex min-vh-100">
      <div
        className="p-5 flex-grow-1"
        style={{ backgroundColor: "var(--brand-main)" }}
      >
        {loading && <p>Loading...</p>}
        {!loading && WList.length === 0 && <p>Your wishlist is empty.</p>}

        <Row className="g-4">
          {WList.map((item) => (
            <Col key={item.id} xs={12} md={6} lg={4}>
              <ProductCard
                item={item}
                conditionConfig={conditionConfig}
                forceWishlisted={true}
              />
            </Col>
          ))}
        </Row>
      </div>
    </div>
  );
}
