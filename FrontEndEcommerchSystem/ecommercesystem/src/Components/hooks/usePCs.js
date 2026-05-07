import { useState, useEffect } from "react";
import axios from "axios";
import { basURL, PCS } from "../../API/api";

export function usePCs(page = 1, pageSize = 8) {
  const [PCs, setPCs] = useState([]);
  const [totalPages, setTotalPages] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    setLoading(true);
    axios
      .get(`${basURL}${PCS}?page=${page}&pageSize=${pageSize}`)
      .then((res) => {
        setPCs(res.data.data.items);
        setTotalPages(res.data.data.totalPages);
        console.log(res.data.data);
      })

      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, [page, pageSize]);

  return { PCs, loading, totalPages, error };
}
