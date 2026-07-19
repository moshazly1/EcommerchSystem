import { useEffect, useState } from "react";
import {
  ADDWHITELIST,
  axiosPrivate,
  basURL,
  REMOVEWHITELIST,
} from "../../API/api";

export default function useWishlistApi() {
  const addtoWhiteList = async (productId) => {
    try {
      const res = await axiosPrivate.post(`${basURL}${ADDWHITELIST}`, {
        productId,
      });

      return res.data;
    } catch (error) {
      console.log("Status:", error.response?.status);
      console.log("Response:", error.response?.data);
      console.log(error);

      throw error;
    }
  };

  const RemoveWhiteList = async (productId) => {
    try {
      const res = await axiosPrivate.delete(`${basURL}${REMOVEWHITELIST}`, {
        data: { productId },
      });
      return res.data;
    } catch (error) {
      console.log("Status:", error.response?.status);
      console.log("Response:", error.response?.data);
      console.log(error);
      throw error;
    }
  };

  return { addtoWhiteList, RemoveWhiteList };
}
