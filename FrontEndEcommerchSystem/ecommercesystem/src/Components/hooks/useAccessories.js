import { useState, useEffect } from "react";
import axios from "axios";
import { ACCESSORIES, basURL } from "../../API/api";

export function useAccessoriers() {
  const [Accessories, setAccessories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios
      .get(`${basURL}${ACCESSORIES}`)
      .then((res) => {
        setAccessories(res.data.data);
        console.log(res.data.data);
      })
      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, []);

  return { Accessories, loading, error };
}
