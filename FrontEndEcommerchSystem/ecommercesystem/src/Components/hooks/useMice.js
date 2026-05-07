import { useState, useEffect } from "react";
import axios from "axios";
import { basURL, MICE } from "../../API/api";

export function useMice(page = 1, pageSize = 8) {
  const [Mice, setMice] = useState([]);
  const [loading, setLoading] = useState(true);
  const [totalPages, setTotalPages] = useState(0);
  const [error, setError] = useState(null);

  useEffect(() => {
    setLoading(true);
    axios
      .get(`${basURL}${MICE}?page=${page}&pageSize=${pageSize}`)
      .then((res) => {
        setMice(res.data.data.items);
        setTotalPages(res.data.data.totalPages);
      })
      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, [page, pageSize]); // ✅

  return { Mice, loading, error, totalPages };
}
