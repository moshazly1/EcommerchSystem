import { useEffect } from "react";
import { Card as CartPro } from "react-bootstrap";
import { useProfile } from "../../Context/ProfileContext";

export default function Orders() {
  const { setStats } = useProfile();

  useEffect(() => {
    setStats(
      <div className="d-flex">
        <CartPro className="px-4 py-2 m-3 border-0">
          <p className="text-secondary fw-bold">Total Builds</p>
          <h2 className="fw-bold">12</h2>
        </CartPro>
        <CartPro className="px-4 py-2 m-3 border-0">
          <p className="text-secondary fw-bold">Active Orders</p>
          <h2 style={{ color: "var(--brand-700)" }} className="fw-bold">
            02
          </h2>
        </CartPro>
      </div>,
    );
  }, []);

  return <div>{/* Recent Order History */}</div>;
}
