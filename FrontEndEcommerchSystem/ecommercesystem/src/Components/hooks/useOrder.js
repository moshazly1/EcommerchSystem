import { useEffect, useState } from "react";
import useAxiosPrivate from "../../Features/Auth/hooks/useAxiosPrivate";
import { basURL, SHOWORDER } from "../../API/api";

export default function useOrder() {
  const [Orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    const GetAllOrder = async () => {
      try {
        const res = await axiosPrivate.get(`${basURL}${SHOWORDER}`);
        setOrders(res.data);
      } catch (err) {
        console.log(err);
      } finally {
        setLoading(false);
      }
    };

    GetAllOrder();
  }, []);

  return { Orders, loading };
}
