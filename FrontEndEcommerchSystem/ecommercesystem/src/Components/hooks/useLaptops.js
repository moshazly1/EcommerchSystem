import { useState, useEffect } from "react";
import axios from "axios";
import { basURL, LAPTOP, PRODUCT } from "../../API/api";

export function useLaptops(page = 1, pageSize = 8) {
  const [laptops, setLaptops] = useState([]);
  const [loading, setLoading] = useState(true);
  const [totalPages, setTotalPages] = useState(0);
  const [error, setError] = useState(null);

  useEffect(() => {
    setLoading(true);
    axios
      .get(`${basURL}${LAPTOP}?page=${page}&pageSize=${pageSize}`)
      .then((res) => {
        setLaptops(res.data.data.items);
        setTotalPages(res.data.data.totalPages);
        console.log(res.data.data);
      })

      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, [page, pageSize]);

  return { laptops, error, loading, totalPages };
}
