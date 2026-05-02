import { useState, useEffect } from "react";
import axios from "axios";
import { basURL, PCS } from "../../API/api";

export function usePCs() {
  const [PCs, setPCs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios
      .get(`${basURL}${PCS}`)
      .then((res) => {
        setPCs(res.data.data);
        console.log(res.data.data);
      })

      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, []);

  return { PCs, loading, error };
}
