import axios from "axios";

export const basURL = "http://localhost:5275/";
// export const basURL = "https://e-commerchsystem.runasp.net/";
export const LOGIN = "api/Auth/Login";
export const REGISTER = "api/Auth/register";
export const REFRESHTOKEN = "api/Auth/refreshToken";
export const USER = "api/User";
export const LOGOUT = "api/Auth/logout";
export const FORGETPASSWORD = "api/Auth/forgot-password";
export const RESETPASSWORD = "api/Auth/resetPassword";
export const axiosPrivate = axios.create({
  baseURL: basURL,
  headers: { "Content-Type": "application/json" },
  withCredentials: true,
});
