import axios from "axios";

export const basURL = "http://localhost:5275/";
// export const basURL = "https://e-commerchsystem.runasp.net/";
export const LOGIN = "api/Auth/Login";
export const REGISTER = "api/Auth/register";
export const REFRESHTOKEN = "api/Auth/refreshToken";
export const USER = "api/User";
export const USERID = "api/User";
export const UPDATEUSER = "api/User/UpdateUser";
export const LOGOUT = "api/Auth/logout";
export const FORGETPASSWORD = "api/Auth/forgot-password";
export const RESETPASSWORD = "api/Auth/resetPassword";
export const PRODUCT = "api/Product";
export const LAPTOP = "api/Product/Laptop";
export const PCS = "api/Product/PCs";
export const MICE = "api/Product/Mice";
export const ACCESSORIES = "api/Product/Accessories";
export const CARTITEM = "api/Cart/items";
export const CART = "api/Cart";
export const DELETITEMSCART = "api/Cart/items";
export const UPDATECART = "api/Cart/items";
export const ADDWHITELIST = "api/WhiteList/AddWhiteList";
export const REMOVEWHITELIST = "api/WhiteList/Remove";
export const GETALLWL = "api/WhiteList/WhiteList";
export const PAYMENT = "api/Payment/create-payment-intent";
export const ADDORDER = "api/Order";
export const SHOWORDER = "api/Order/my-orders";
export const axiosPrivate = axios.create({
  baseURL: basURL,
  headers: { "Content-Type": "application/json" },
  withCredentials: true,
});
