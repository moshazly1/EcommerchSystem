import axios from "axios";

export const basURL = "http://e-commerchsystem.runasp.net/";
export const LOGIN = "api/Auth/Login";
export const REGISTER = "api/Auth/register";
export const REFRESHTOKEN = "api/Auth/refreshToken";
export const axiosPrivate = axios.create({
  headers: { "Content-Type": "application/json" },
  withCredentials: true,
});
