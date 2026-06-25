import axios from "axios";
import { basURL, CARTITEM, DELETITEMSCART } from "../../API/api";
import { useNavigate } from "react-router-dom";
import useAxiosPrivate from "../../Features/Auth/hooks/useAxiosPrivate";
import { useState } from "react";

export default function useCart() {
  const navigate = useNavigate();

  const axiosPrivate = useAxiosPrivate();
  const addToCart = async (productId) => {
    try {
      await axiosPrivate.post(
        `${basURL}${CARTITEM}`,
        { productId, quantity: 1 },
        { withCredentials: true },
      );
    } catch (err) {
      if (err.response?.status === 401) {
        navigate("/login");
      } else {
        alert("You must be logged in to add to cart");
      }
    }
  };

  return { addToCart };
}
