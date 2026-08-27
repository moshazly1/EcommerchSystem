import axios from "axios";
import { useEffect, useState } from "react";
import {
  basURL,
  GETALLBRAND,
  GETALLCATEGORY,
  GETALLSUBCATEGORY,
  GETRANGEPRICE,
} from "../../API/api";
import { Chat } from "react-bootstrap-icons";

export default function useSideBar() {
  const [brands, setBrand] = useState([]);
  const [category, setCategory] = useState([]);
  const [RangePrice, setRangePrice] = useState([]);
  const [loading, setLoding] = useState(true);
  const [SubCategory, SetSubCategory] = useState([]);
  const [error, seteror] = useState("");

  useEffect(() => {
    const featchBrand = async () => {
      setLoding(true);
      seteror("");
      try {
        const Brands = await axios.get(`${basURL}${GETALLBRAND}`);
        setBrand(Brands.data.data);
      } catch (err) {
        console.log("Error fetching brands:", err);
        seteror("Failed to load brands.");
      } finally {
        setLoding(false);
      }
    };
    featchBrand();
  }, []);

  useEffect(() => {
    const FethePriceReng = async () => {
      try {
        const res = await axios.get(`${basURL}${GETRANGEPRICE}`);
        console.log(res.data);
        setRangePrice(res.data);
      } catch (err) {
        console.log(err);
      }
    };

    FethePriceReng();
  }, []);

  useEffect(() => {
    const featchCategory = async () => {
      try {
        const res = axios.get(`${basURL}${GETALLCATEGORY}`);
        console.log(res.data);
        setCategory((await res).data.data);
      } catch (err) {
        console.log("Error fetching brands:", err);
      } finally {
      }
    };
    featchCategory();
  }, []);

  useEffect(() => {
    const featchSubCategory = async () => {
      try {
        const res = axios.get(`${basURL}${GETALLSUBCATEGORY}`);
        console.log(res.data);
        SetSubCategory((await res).data.data);
      } catch (err) {
        console.log("Error fetching brands:", err);
      } finally {
      }
    };
    featchSubCategory();
  }, []);
  return { brands, loading, error, category, SubCategory, RangePrice };
}
