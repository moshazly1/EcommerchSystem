import { createContext, useContext, useState, useEffect } from "react";
import useGetAllWhiteList from "../Components/hooks/useGetAllWhiteList";
import useAuth from "../Features/Auth/hooks/useAuth"; // عدّل المسار حسب مكانه

const WishlistContext = createContext();

export function WishlistProvider({ children }) {
  const { auth } = useAuth();
  const [wishlist, setWishlist] = useState([]);
  const { WList, loading } = useGetAllWhiteList(!!auth?.accessToken);

  useEffect(() => {
    if (!loading && WList) {
      setWishlist(WList.map((item) => item.productId ?? item.id));
    }
  }, [WList, loading]);

  const toggleWishlist = (id) => {
    setWishlist((prev) =>
      prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id],
    );
  };

  return (
    <WishlistContext.Provider value={{ wishlist, toggleWishlist, loading }}>
      {children}
    </WishlistContext.Provider>
  );
}

export function useWishlist() {
  return useContext(WishlistContext);
}
