import { createContext, useEffect, useState } from "react";
import { basURL, CART, CARTITEM, DELETITEMSCART } from "../API/api";
import useAxiosPrivate from "../Features/Auth/hooks/useAxiosPrivate";
import useAuth from "../Features/Auth/hooks/useAuth";
export const CartContext = createContext();

export function CartProvider({ children }) {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const [cartItems, setCartItems] = useState([]);

  const cartCount = cartItems.reduce((sum, item) => sum + item.quantity, 0);

  // Get Cart
  const fetchCart = async () => {
    try {
      const res = await axiosPrivate.get(`${basURL}${CART}`);
      console.log("Response =", res.data);
      setCartItems(res.data.items);
    } catch (err) {
      console.log(err);
    }
  };

  // Add Item
  const addToCart = async (productId) => {
    try {
      await axiosPrivate.post(`${basURL}${CARTITEM}`, {
        productId,
        quantity: 1,
      });

      await fetchCart();
    } catch (err) {
      console.log(err);
    }
  };

  // Remove Item
  const removeFromCart = async (cartItemId) => {
    try {
      await axiosPrivate.delete(`${basURL}${DELETITEMSCART}/${cartItemId}`);

      await fetchCart();
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    if (auth?.accessToken) {
      fetchCart();
    }
  }, [auth?.accessToken]);

  return (
    <CartContext.Provider
      value={{
        cartItems,
        cartCount,
        fetchCart,
        addToCart,
        removeFromCart,
      }}
    >
      {children}
    </CartContext.Provider>
  );
}
