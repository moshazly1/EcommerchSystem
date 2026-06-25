import { useEffect, useState } from "react";
import useAxiosPrivate from "../../Features/Auth/hooks/useAxiosPrivate";
import { basURL, CART, DELETITEMSCART, UPDATECART } from "../../API/api";

export default function useCartItems() {
  const [Cart, setCart] = useState(null);

  const [loding, setLoding] = useState(true);
  const [error, setError] = useState(null);
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    const fetchCart = async () => {
      try {
        const res = await axiosPrivate.get(`${basURL}${CART}`);
        setCart(res.data);
      } catch (err) {
        setError(err);
      } finally {
        setLoding(false);
      }
    };
    fetchCart();
  }, []);

  const DeleteItemsCard = async (cartItemId) => {
    try {
      await axiosPrivate.delete(`${basURL}${DELETITEMSCART}/${cartItemId}`);

      const res = await axiosPrivate.get(`${basURL}${CART}`);

      setCart(res.data);
    } catch (err) {
      console.log(err);
    }
  };

  const Update = async (cartItemId, newQuantity) => {
    try {
      const res = await axiosPrivate.put(
        `${basURL}${UPDATECART}/${cartItemId}`,
        newQuantity,
        { headers: { "Content-Type": "application/json" } },
      );

      setCart(res.data);
    } catch (err) {
      console.log(err);
    }
  };

  return { DeleteItemsCard, Cart, loding, error, Update };
}
