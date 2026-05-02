import { useState, useEffect } from "react";
import axios from "axios";
import { basURL, MICE } from "../../API/api";

export function useMice() {
  const [Mice, setMice] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios
      .get(`${basURL}${MICE}`)
      .then((res) => {
        setMice(res.data.data);
        console.log(res.data.data);
      })

      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, []);

  return { Mice, loading, error };
}
