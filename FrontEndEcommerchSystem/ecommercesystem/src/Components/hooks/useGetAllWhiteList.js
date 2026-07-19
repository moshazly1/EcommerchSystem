import { useEffect, useState } from "react";
import { basURL, GETALLWL } from "../../API/api";
import useAxiosPrivate from "../../Features/Auth/hooks/useAxiosPrivate";

export default function useGetAllWhiteList(enabled = true) {
  const [loading, setLoading] = useState(true);
  const [WList, SetWList] = useState([]);
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    if (!enabled) {
      setLoading(false);
      return;
    }

    const fetchWishlist = async () => {
      setLoading(true);
      try {
        const res = await axiosPrivate.get(`${basURL}${GETALLWL}`);
        SetWList(res.data);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    fetchWishlist();
  }, [axiosPrivate, enabled]);

  return { WList, loading };
}
