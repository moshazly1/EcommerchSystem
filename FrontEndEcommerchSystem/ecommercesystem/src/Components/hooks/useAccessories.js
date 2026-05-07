import { useState, useEffect } from "react";
import axios from "axios";
import { ACCESSORIES, basURL } from "../../API/api";

export function useAccessoriers(page = 1, pageSize = 8) {
  const [Accessories, setAccessories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [totalPages, setTotalPages] = useState(0);
  const [error, setError] = useState(null);

  useEffect(() => {
    setLoading(true);
    axios
      .get(`${basURL}${ACCESSORIES}?page=${page}&pageSize=${pageSize}`)
      .then((res) => {
        setAccessories(res.data.data.items);
        setTotalPages(res.data.data.totalPages);
        console.log(res.data.data);
      })
      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, [page, pageSize]);

  return { Accessories, loading, error, totalPages };
}
