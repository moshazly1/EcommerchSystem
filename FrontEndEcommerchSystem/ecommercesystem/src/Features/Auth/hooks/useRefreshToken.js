import axios from "axios";
import useAuth from "./useAuth";
import { basURL, REFRESHTOKEN } from "../../../API/api";

const useRefreshToken = () => {
  const { auth, setAuth } = useAuth();
  const refresh = async () => {
    const response = await axios.post(
      `${basURL}${REFRESHTOKEN}`,
      {},
      {
        headers: {
          "Content-Type": "application/json",
        },
        withCredentials: true,
      },
    );
    setAuth((prev) => {
      return {
        ...prev,
        accessToken: response.data.token,

        user: { ...prev.user, refreshToken: response.data.refreshToken },
      };
    });
    return response.data.token;
  };
  return refresh;
};
export default useRefreshToken;
