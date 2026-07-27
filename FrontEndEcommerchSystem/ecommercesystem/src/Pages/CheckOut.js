import {
  faCreditCard,
  faMoneyBill,
  faTruck,
  faWallet,
  faShieldAlt,
  faRotateLeft,
  faHeadset,
  faLock,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Col, Container, Form, Row } from "react-bootstrap";
import useCartItems from "../Components/hooks/useCartItems";
import useCheckout from "../Components/hooks/useCheckout";
import { CardElement } from "@stripe/react-stripe-js";
import { useState, useEffect } from "react";
import OrderSuccessModel from "../Components/OrderSuccessModel";
import "./CheackOut.css";
export default function CheckOut() {
  const { Cart } = useCartItems();
  const [currentStep, setCurrentStep] = useState(0);
  const {
    data,
    handleChange,
    handleSubmit,
    error,
    orderId,
    setShowModal,
    showModal,
  } = useCheckout();

  useEffect(() => {
    if (
      data.FullName &&
      data.EmailAddress &&
      data.PhoneNumber &&
      data.City &&
      data.StreetAddress
    ) {
      setCurrentStep(1);
    } else {
      setCurrentStep(0);
    }
  }, [data]);
  return (
    <>
      <Container className="  min-vh-100 mt-5">
        <div className="d-flex  justify-content-between align-items-center">
          <h1 className="fw-bold" style={{ color: "var(--brand-600)" }}>
            PRECISION
          </h1>
          <div className="d-flex align-content-center justify-content-center">
            <div
              className={`rounded-circle d-flex align-items-center justify-content-center ${currentStep === 1 ? "step-active" : ""}`}
              style={{
                width: "30px",
                height: "30px",
                backgroundColor: currentStep >= 1 ? "var(--brand-700)" : "#ccc",
              }}
            >
              <span className="text-white">1</span>
            </div>
            <p
              className="text-secondary fw-bold small mx-2 my-1 "
              style={{ letterSpacing: "2px" }}
            >
              SHIPPING
            </p>
          </div>
          <div className="d-flex align-content-center justify-content-center">
            <div
              className={`rounded-circle d-flex align-items-center justify-content-center ${currentStep === 2 ? "step-active" : ""}`}
              style={{
                width: "30px",
                height: "30px",
                backgroundColor: currentStep >= 2 ? "var(--brand-700)" : "#ccc",
              }}
            >
              <span className="text-white">2</span>
            </div>
            <p
              className="text-secondary fw-bold small mx-2 my-1 "
              style={{ letterSpacing: "2px" }}
            >
              PAYMENT
            </p>
          </div>
          <div className="d-flex align-content-center justify-content-center">
            <div
              className={`rounded-circle d-flex align-items-center justify-content-center ${currentStep === 3 ? "step-active" : ""}`}
              style={{
                width: "30px",
                height: "30px",
                backgroundColor: currentStep >= 3 ? "var(--brand-700)" : "#ccc",
              }}
            >
              <span className="text-white">3</span>
            </div>
            <p
              className="text-secondary fw-bold small mx-2 my-1 "
              style={{ letterSpacing: "2px" }}
            >
              CONFIRM
            </p>
          </div>
          <div>
            <p>
              {" "}
              <FontAwesomeIcon icon={faLock} /> SECURE CHECKOUT
            </p>
          </div>
        </div>
        <Row>
          <Col xs={12} md={8}>
            <div className="d-flex align-items-center gap-2  mt-4 mb-4">
              <FontAwesomeIcon
                className="fs-4"
                style={{ color: "var(--brand-700)" }}
                icon={faTruck}
              />

              <p className="mb-0 fs-3 ">Shipping Address</p>
            </div>
            <Form
              className="p-4 rounded-3"
              style={{ border: "1px solid var(--brand-300)" }}
            >
              <div className="row g-3">
                {/* Full Name + Email */}
                <Form.Group className="col-md-6">
                  <Form.Label
                    className="text-uppercase text-secondary fw-bold"
                    style={{ fontSize: "12px" }}
                  >
                    Full Name
                  </Form.Label>
                  <Form.Control
                    name="FullName"
                    type="text"
                    placeholder="e.g., Alexander Wright "
                    style={{ backgroundColor: "#F5F3F3" }}
                    onChange={handleChange}
                    value={data.FullName}
                  />
                </Form.Group>

                <Form.Group className="col-md-6">
                  <Form.Label
                    className="text-uppercase text-secondary fw-bold"
                    style={{ fontSize: "12px" }}
                  >
                    Email Address
                  </Form.Label>
                  <Form.Control
                    type="email"
                    name="EmailAddress"
                    placeholder="alex@precision.com"
                    style={{ backgroundColor: "#F5F3F3" }}
                    onChange={handleChange}
                    value={data.EmailAddress}
                  />
                </Form.Group>

                {/* Phone + City */}
                <Form.Group className="col-md-6">
                  <Form.Label
                    className="text-uppercase text-secondary fw-bold"
                    style={{ fontSize: "12px" }}
                  >
                    Phone Number
                  </Form.Label>
                  <Form.Control
                    name="PhoneNumber"
                    type="tel"
                    placeholder="+1 (555) 000-0000"
                    style={{ backgroundColor: "#F5F3F3" }}
                    onChange={handleChange}
                    value={data.PhoneNumber}
                  />
                </Form.Group>

                <Form.Group className="col-md-6">
                  <Form.Label
                    className="text-uppercase text-secondary fw-bold"
                    style={{ fontSize: "12px" }}
                  >
                    City
                  </Form.Label>
                  <Form.Control
                    type="text"
                    name="City"
                    placeholder="San Francisco"
                    style={{ backgroundColor: "#F5F3F3" }}
                    onChange={handleChange}
                    value={data.City}
                  />
                </Form.Group>

                {/* Street Address */}
                <Form.Group className="col-12">
                  <Form.Label
                    className="text-uppercase text-secondary fw-bold"
                    style={{ fontSize: "12px" }}
                  >
                    Street Address
                  </Form.Label>
                  <Form.Control
                    name="StreetAddress"
                    type="text"
                    placeholder="Suite 400, Market St."
                    style={{ backgroundColor: "#F5F3F3" }}
                    onChange={handleChange}
                    value={data.StreetAddress}
                  />
                </Form.Group>
              </div>
            </Form>
            {error && <div className="alert alert-danger mt-3">{error}</div>}
            <div className="d-flex align-items-center gap-2  mt-5 mb-5">
              <FontAwesomeIcon
                className="fs-4"
                style={{ color: "var(--brand-700)" }}
                icon={faMoneyBill}
              />
              <p className="mb-0 fs-3  ">Payment Method </p>
            </div>
            <Form
              className="p-4 rounded-3"
              style={{ border: "1px solid var(--brand-300)" }}
            >
              <div className="d-flex justify-content-between align-items-center">
                {/* Credit Card */}
                <Form.Check
                  type="radio"
                  name="paymentMethod"
                  id="creditCard"
                  label="Credit / Debit Card"
                  defaultChecked
                />
                <div>
                  <FontAwesomeIcon icon={faWallet} />
                  <FontAwesomeIcon icon={faCreditCard} />
                </div>
              </div>
              <div
                className="p-3 mt-3 mb-3"
                style={{
                  border: "1px solid #ccc",
                  borderRadius: "8px",
                  backgroundColor: "#F5F3F3",
                }}
              >
                <CardElement
                  options={{
                    style: {
                      base: { fontSize: "16px", color: "#333" },
                    },
                  }}
                  onChange={(e) => {
                    if (e.complete) {
                      setCurrentStep(2);
                    } else {
                      setCurrentStep(1);
                    }
                  }}
                />
              </div>
              <div
                className="d-flex justify-content-between align-items-center p-4 rounded  "
                style={{ backgroundColor: "#F5F3F3" }}
              >
                <div>
                  <Form.Check
                    className="mt-3"
                    type="radio"
                    name="paymentMethod"
                    id="cashOnDelivery"
                    label="Cash on Delivery"
                  />
                  <h6 className="fs-6 ps-4">الدفع عند الاستلام </h6>
                </div>

                <FontAwesomeIcon icon={faTruck} />
              </div>
            </Form>
            <Row className="mt-4 text-center g-3">
              <Col xs={4}>
                <div className="p-3 border rounded-3">
                  <FontAwesomeIcon
                    icon={faShieldAlt}
                    className="fs-4 mb-2"
                    style={{ color: "var(--brand-500)" }}
                  />
                  <p
                    className="text-uppercase fw-bold mb-0"
                    style={{ fontSize: "12px", letterSpacing: "1px" }}
                  >
                    Secure SSL
                  </p>
                </div>
              </Col>

              <Col xs={4}>
                <div className="p-3 border rounded-3">
                  <FontAwesomeIcon
                    icon={faRotateLeft}
                    className="fs-4 mb-2"
                    style={{ color: "var(--brand-500)" }}
                  />
                  <p
                    className="text-uppercase fw-bold mb-0"
                    style={{ fontSize: "12px", letterSpacing: "1px" }}
                  >
                    30-Day Returns
                  </p>
                </div>
              </Col>

              <Col xs={4}>
                <div className="p-3 border rounded-3">
                  <FontAwesomeIcon
                    icon={faHeadset}
                    className="fs-4 mb-2"
                    style={{ color: "var(--brand-500)" }}
                  />
                  <p
                    className="text-uppercase fw-bold mb-0"
                    style={{ fontSize: "12px", letterSpacing: "1px" }}
                  >
                    Expert Help
                  </p>
                </div>
              </Col>
            </Row>
          </Col>
          <Col xs={12} md={4}>
            <div
              className="p-4 rounded-3 mt-5"
              style={{ backgroundColor: "var(--brand-main-3)" }}
            >
              <h5 className="fw-bold mb-4">Order Summary</h5>

              {/* المنتجات */}
              {Cart?.items?.map((item) => (
                <div
                  key={item.cartItemId}
                  className="d-flex align-items-center gap-3 mb-3"
                >
                  <img
                    src={item.imageUrl}
                    alt={item.productName}
                    style={{
                      width: "50px",
                      height: "50px",
                      objectFit: "contain",
                    }}
                  />
                  <div className="flex-grow-1">
                    <p className="fw-bold mb-0" style={{ fontSize: "14px" }}>
                      {item.productName}
                    </p>
                    <p
                      className="text-primary mb-0"
                      style={{ fontSize: "14px" }}
                    >
                      ${item.unitPrice}
                    </p>
                  </div>
                </div>
              ))}

              <hr />

              {/* Subtotal */}
              <div className="d-flex justify-content-between py-1">
                <span className="text-secondary">Subtotal</span>
                <span>${Cart?.subtotal}</span>
              </div>

              {/* Shipping */}
              <div className="d-flex justify-content-between py-1">
                <span className="text-secondary">Shipping</span>
                <span className="text-primary">Free</span>
              </div>

              {/* Tax */}
              <div className="d-flex justify-content-between py-1">
                <span className="text-secondary">Estimated Tax</span>
                <span>${Cart?.tax}</span>
              </div>

              <hr />

              {/* Total */}
              <div className="d-flex justify-content-between py-2">
                <span className="fw-bold">Total</span>
                <h5 className="fw-bold" style={{ color: "var(--brand-500)" }}>
                  ${Cart?.total}
                </h5>
              </div>

              {/* Place Order Button */}
              <button
                className="btn w-100 mt-3 text-white"
                style={{ backgroundColor: "var(--brand-700)" }}
                onClick={(e) => handleSubmit(e, Cart, setCurrentStep)}
              >
                Place Your Order →
              </button>

              <p
                className="text-secondary text-center mt-2"
                style={{ fontSize: "11px" }}
              >
                By placing your order, you agree to Precision Curator's Terms of
                Service and Privacy Policy.
              </p>

              {/* Promo Code */}
              <div className="d-flex gap-2 mt-3">
                <input
                  type="text"
                  className="form-control"
                  placeholder="Promo Code"
                  style={{ backgroundColor: "#fff" }}
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
          </Col>
        </Row>
      </Container>
      <OrderSuccessModel
        show={showModal}
        setShow={setShowModal}
        orderId={orderId}
      />
      ;
    </>
  );
}
