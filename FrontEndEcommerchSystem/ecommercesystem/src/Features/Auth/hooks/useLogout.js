import useAuth from "./useAuth";
import { LogoutRequast } from "../Service/authApi";

const useLogout = () => {
  const { setAuth } = useAuth();
  const Logout = async () => {
    try {
      const response = LogoutRequast();
      setAuth({});
      console.log(response);
    } catch (err) {
      console.error(err);
    }
  };
  return Logout;
};
export default useLogout;
