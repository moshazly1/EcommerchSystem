import { useState, useEffect } from "react";
import axios from "axios";
import { basURL, LAPTOP, PRODUCT } from "../../API/api";

export function useLaptops() {
  const [laptops, setLaptops] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios
      .get(`${basURL}${LAPTOP}`)
      .then((res) => {
        setLaptops(res.data.data);
        console.log(res.data.data);
      })

      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, []);

  return { laptops, loading, error };
}
