import { useState } from "react";
import { useStripe, useElements, CardElement } from "@stripe/react-stripe-js";
import { useNavigate } from "react-router-dom";
import useAxiosPrivate from "../../Features/Auth/hooks/useAxiosPrivate";
import { basURL, PAYMENT, ADDORDER } from "../../API/api";

export default function useCheckout() {
  const [data, setData] = useState({
    FullName: "",
    EmailAddress: "",
    PhoneNumber: "",
    City: "",
    StreetAddress: "",
  });
  const [orderId, setOrderId] = useState(null);
  const [error, setError] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const stripe = useStripe();
  const elements = useElements();
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();

  function handleChange(e) {
    setData({ ...data, [e.target.name]: e.target.value });
    setError(null);
  }

  async function handleSubmit(e, cart, setCurrentStep) {
    e.preventDefault();
    if (
      !data.FullName ||
      !data.EmailAddress ||
      !data.PhoneNumber ||
      !data.City ||
      !data.StreetAddress
    ) {
      setError("Plase fill all shipping  fields first");
      return;
    }
    try {
      const {
        data: { clientSecret },
      } = await axiosPrivate.post(`${basURL}${PAYMENT}`, cart.total);

      const result = await stripe.confirmCardPayment(clientSecret, {
        payment_method: {
          card: elements.getElement(CardElement),
        },
      });

      if (result.paymentIntent.status === "succeeded") {
        const response = await axiosPrivate.post(`${basURL}${ADDORDER}`, {
          items: cart.items.map((item) => ({
            productId: item.productId,
            quantity: item.quantity,
            unitPrice: item.unitPrice,
          })),
          shippingAddress: `${data.StreetAddress}, ${data.City}`,
          totalAmount: cart.total,
        });

        setOrderId(response.data.orderId);
        setCurrentStep(3);
        setShowModal(true);
      }
    } catch (err) {
      setError("Payment failed. Please try again.");
      console.log(err);
    }
  }

  return {
    data,
    handleChange,
    handleSubmit,
    orderId,
    error,
    showModal,
    setShowModal,
  };
}
