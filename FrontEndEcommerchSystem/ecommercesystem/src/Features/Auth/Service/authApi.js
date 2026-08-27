import axios from "axios";
import {
  basURL,
  LOGIN,
  REGISTER,
  LOGOUT,
  REFRESHTOKEN,
  FORGETPASSWORD,
  RESETPASSWORD,
  SENDCODETOWFACTOR,
  RESENDCODEFACTOR,
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

export const ForgetPassword = async (email) => {
  return await axios.post(
    `${basURL}${FORGETPASSWORD}`,
    { email },
    {
      headers: { "Content-Type": "application/json" },
      withCredentials: true,
    },
  );
};

export const ResetPasswordRequest = async (Data) => {
  return await axios.post(`${basURL}${RESETPASSWORD}`, Data, {
    headers: { "Content-Type": "application/json" },
    withCredentials: true,
  });
};
export const VerifyTwoFactorCode = async (email, code) => {
  try {
    const res = await axios.post(`${basURL}${SENDCODETOWFACTOR}`, {
      email,
      code,
    });

    return res.data;
  } catch (err) {
    console.log("Verify 2FA error:", err);
    throw err;
  }
};
export const ResendTwoFactorcode = async (email) => {
  try {
    const res = await axios.post(`${basURL}${RESENDCODEFACTOR}`, {
      email,
    });
    console.log(res.data);
    return res.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};
