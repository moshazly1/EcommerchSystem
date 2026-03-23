import { useContext } from "react";
import { User } from "../../../Pages/Context/Context";

const useAuth = () => {
  return useContext(User);
};
export default useAuth;
