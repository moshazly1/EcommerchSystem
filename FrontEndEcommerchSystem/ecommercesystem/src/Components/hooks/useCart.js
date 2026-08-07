import { useContext } from "react";
import { CartContext } from "../../Context/CartContext";
export default function useCart() {
  return useContext(CartContext);
}
