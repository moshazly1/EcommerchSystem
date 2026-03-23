import axios from "axios";
import { basURL, LOGIN, REGISTER } from "../../../API/api";

export const LoginRequast = async (formData) => {
  return await axios.post(`${basURL}${LOGIN}`, formData, {
    headers: { "Content-Type": "application/json" },
    withCredentials: true,
  });
};

export const RegisterRequast = async (formData) => {
  return await axios.post(`${basURL}${REGISTER}`, formData, {
    headers: { "Content-Type": "application/json" },
    withCredentials: true,
  });
};
