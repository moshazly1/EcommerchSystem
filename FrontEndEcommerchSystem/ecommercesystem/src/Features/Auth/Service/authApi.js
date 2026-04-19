import axios from "axios";
import {
  basURL,
  LOGIN,
  REGISTER,
  LOGOUT,
  REFRESHTOKEN,
} from "../../../API/api";

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

export const LogoutRequast = async () => {
  return await axios.post(
    `${basURL}${LOGOUT}`,
    {},
    {
      withCredentials: true,
    },
  );
};

export const RefreshTokenRequast = async () => {
  return await axios.post(
    `${basURL}${REFRESHTOKEN}`,
    {},
    { withCredentials: true },
  );
};
