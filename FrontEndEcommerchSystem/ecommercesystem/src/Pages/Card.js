import { Col, Row, Card as BsCard } from "react-bootstrap";
import {
  faCube,
  faShieldAlt,
  faTrash,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Fragment, useState } from "react";
import useCartItems from "../Components/hooks/useCartItems";
import { useNavigate } from "react-router-dom";
export default function Card() {
  const { Cart, error, loding, DeleteItemsCard, Update } = useCartItems();
  const [promoCode, setPromoCode] = useState("");
  const navigate = useNavigate();
  if (loding) return <p>Loading...</p>;
  if (!Cart) return <p>Cart is empty</p>;

  return (
    <>
      <div className="d-flex min-vh-100">
        <div
          className="p-5 flex-grow-1"
          style={{ backgroundColor: "var(--brand-main)" }}
        >
          <h6 className="text-uppercase text-primary fw-bold">
            Precision Selection
          </h6>
          <h1>Your Cart</h1>

          <Row className="mt-4">
            {/* المنتجات */}
            <Col xs={12} md={8}>
              {Cart.items?.map((item) => (
                <Fragment key={item.cartItemId}>
                  <BsCard className="p-4 border-0 mb-3">
                    <div className="d-flex justify-content-between align-items-stretch">
                      {/* يسار */}
                      <div className="d-flex gap-3">
                        <div className="d-flex align-items-center justify-content-center">
                          <img
                            src={item.imageUrl}
                            alt={item.productName}
                            style={{
                              borderRadius: "12px",
                              width: "140px",
                              height: "140px",
                            }}
                          />
                        </div>
                        <div>
                          <h2 className="fw-bold">{item.productName}</h2>
                          <div className="d-flex align-items-center gap-2">
                            <span
                              className="rounded-circle"
                              style={{
                                backgroundColor: "var(--brand-600)",
                                width: "12px",
                                height: "12px",
                                display: "inline-block",
                              }}
                            ></span>
                            <p className="text-secondary mb-0">
                              {item.productDescription}
                            </p>
                          </div>
                          <div className="d-flex align-items-center gap-3 mt-2">
                            <div
                              style={{
                                backgroundColor: "var(--brand-main-3)",
                                width: "fit-content",
                              }}
                              className="rounded-2 d-flex align-items-center"
                            >
                              <button
                                className="btn btn-sm px-3 fs-4"
                                style={{ outline: "none", boxShadow: "none" }}
                                onClick={() =>
                                  Update(
                                    item.cartItemId,
                                    Math.max(1, item.quantity - 1),
                                  )
                                }
                              >
                                -
                              </button>
                              <span className="px-2 fw-bold">
                                {item.quantity}
                              </span>
                              <button
                                className="btn btn-sm px-3 fs-4"
                                style={{ outline: "none", boxShadow: "none" }}
                                onClick={() =>
                                  Update(item.cartItemId, item.quantity + 1)
                                }
                              >
                                +
                              </button>
                            </div>
                          </div>
                        </div>
                      </div>

                      {/* يمين */}
                      <div className="d-flex flex-column justify-content-between align-items-end">
                        <h5 className="fw-bold">${item.unitPrice}</h5>
                        <button
                          style={{
                            cursor: "pointer",
                            color: "red",
                            border: "none",
                            background: "none",
                          }}
                          onClick={() => DeleteItemsCard(item.cartItemId)}
                        >
                          <FontAwesomeIcon icon={faTrash} /> Remove
                        </button>
                      </div>
                    </div>
                  </BsCard>
                </Fragment>
              ))}
            </Col>

            {/* Summary */}
            <Col xs={12} md={4}>
              <BsCard className="border-0 p-3">
                <h4 className="fw-bold mb-4">Summary</h4>

                <div className="d-flex justify-content-between py-3">
                  <span className="text-uppercase text-secondary fw-bold">
                    Subtotal
                  </span>
                  <span className="fw-bold">${Cart.subtotal}</span>
                </div>

                <div className="d-flex justify-content-between py-3">
                  <span className="text-uppercase text-secondary fw-bold">
                    Estimated Shipping
                  </span>
                  <span className="fw-bold">${Cart.shipping}</span>
                </div>

                <div
                  className="d-flex justify-content-between py-3"
                  style={{ borderBottom: "2px solid var(--brand-300)" }}
                >
                  <span className="text-uppercase text-secondary fw-bold">
                    Tax (8%)
                  </span>
                  <span className="fw-bold">${Cart.tax}</span>
                </div>

                <div className="d-flex justify-content-between py-3">
                  <span className="text-uppercase text-secondary fw-bold">
                    Total
                  </span>
                  <h1 className="fw-bold">${Cart.total}</h1>
                </div>

                <button
                  className="btn w-100 mt-3 text-white"
                  style={{ backgroundColor: "var(--brand-700)" }}
                  onClick={() => navigate("/checkout")}
                >
                  PROCEED TO CHECKOUT
                </button>

                <div
                  className="d-flex justify-content-between py-3"
                  style={{ borderBottom: "2px solid var(--brand-300)" }}
                ></div>

                <div>
                  <div className="py-3 d-flex align-items-center gap-2">
                    <FontAwesomeIcon
                      icon={faShieldAlt}
                      style={{ color: "var(--brand-500)" }}
                    />
                    <p className="mb-0">
                      Encrypted, secure checkout processing
                    </p>
                  </div>
                  <div className="py-3 d-flex align-items-center gap-2">
                    <FontAwesomeIcon
                      icon={faCube}
                      style={{ color: "var(--brand-500)" }}
                    />
                    <p className="mb-0">
                      Insured shipping with real-time tracking
                    </p>
                  </div>
                </div>

                {/* Promo Code */}
                <div
                  style={{
                    backgroundColor: "var(--brand-main-3)",
                    borderRadius: "8px",
                    padding: "16px",
                    marginTop: "16px",
                  }}
                >
                  <p
                    className="text-uppercase fw-bold text-secondary mb-2"
                    style={{ fontSize: "12px" }}
                  >
                    Have a promo code?
                  </p>
                  <div className="d-flex gap-2">
                    <input
                      type="text"
                      className="form-control"
                      placeholder="CODE2024"
                      value={promoCode}
                      onChange={(e) => setPromoCode(e.target.value)}
                    />
                    <button
                      className="btn text-white"
                      style={{
                        backgroundColor: "var(--brand-900)",
                        whiteSpace: "nowrap",
                      }}
                    >
                      Apply
                    </button>
                  </div>
                </div>
              </BsCard>
            </Col>
          </Row>
        </div>
      </div>
    </>
  );
}
