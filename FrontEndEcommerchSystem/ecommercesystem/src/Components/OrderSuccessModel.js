import { Modal } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleCheck } from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";
export default function OrderSuccessModel({ show, setShow, orderId }) {
  const navigate = useNavigate();
  return (
    <Modal show={show} onHide={() => setShow(false)} centered>
      <Modal.Body className="text-center p-5">
        <FontAwesomeIcon
          icon={faCircleCheck}
          style={{ color: "green", fontSize: "60px" }}
        />

        <h3 className="mt-3">Order Confirmed!</h3>
        <p>Your order has been placed successfully.</p>
        <p>Order #{orderId}</p>

        <button
          className="btn btn-primary w-100 mb-2"
          onClick={() => navigate("/Profile")}
        >
          View Orders
        </button>

        <button
          className="btn btn-outline-secondary w-100"
          onClick={() => navigate("/")}
        >
          Continue Shopping
        </button>
      </Modal.Body>
    </Modal>
  );
}
