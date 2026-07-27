import { useEffect } from "react";
import { Card as CartPro } from "react-bootstrap";
import { useProfile } from "../../Context/ProfileContext";
import useOrder from "../../Components/hooks/useOrder";

export default function Orders() {
  const { setStats } = useProfile();
  const { Orders, loading } = useOrder();

  useEffect(() => {
    setStats(
      <div className="d-flex">
        <CartPro className="px-4 py-2 m-3 border-0">
          <p className="text-secondary fw-bold">Total Builds</p>
          <h2 className="fw-bold">{Orders?.length || 0}</h2>
        </CartPro>
        <CartPro className="px-4 py-2 m-3 border-0">
          <p className="text-secondary fw-bold">Active Orders</p>
          <h2 style={{ color: "var(--brand-700)" }} className="fw-bold">
            {Orders?.filter((o) => o.status === "Pending").length || 0}
          </h2>
        </CartPro>
      </div>,
    );
  }, [Orders]);

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <div className="d-flex align-items-center justify-content-between mb-4">
        <h1 className="fw-bold">Recent Order History</h1>
        <h6 className="fw-bold" style={{ color: "var(--brand-600)" }}>
          View All Orders
        </h6>
      </div>

      {Orders?.map((order) => (
        <div
          key={order.orderId}
          className="p-4  rounded-3 mb-3"
          style={{ backgroundColor: "#FFFF" }}
        >
          <div className="d-flex justify-content-between align-items-center">
            <div>
              <p className="text-secondary mb-1" style={{ fontSize: "12px" }}>
                ORDER NUMBER
              </p>
              <p className="fw-bold mb-0">TC-{order.orderId}</p>
            </div>

            <div>
              <p className="text-secondary mb-1" style={{ fontSize: "12px" }}>
                PLACED ON
              </p>
              <p className="fw-bold mb-0">
                {new Date(order.orderDate).toLocaleDateString()}
              </p>
            </div>

            <div>
              <p className="text-secondary mb-1" style={{ fontSize: "12px" }}>
                TOTAL AMOUNT
              </p>
              <p className="fw-bold mb-0">${order.totalAmount}</p>
            </div>

            <span
              className="badge d-flex align-items-center gap-2"
              style={{
                backgroundColor:
                  order.status === "Pending" ? "#FFF3CD" : "#D4EDDA",
                color: order.status === "Pending" ? "#856404" : "#155724",
              }}
            >
              <span
                style={{
                  width: "8px",
                  height: "8px",
                  borderRadius: "50%",
                  backgroundColor:
                    order.status === "Pending" ? "#856404" : "#155724",
                  display: "inline-block",
                }}
              ></span>
              {order.status === 1 ? "Delivered" : "In Transit"}
            </span>

            <button
              className="btn text-white"
              style={{ backgroundColor: "var(--brand-700)" }}
            >
              {order.status === "Pending" ? "TRACK ORDER" : "INVOICE"}
            </button>
          </div>
        </div>
      ))}
    </div>
  );
}
