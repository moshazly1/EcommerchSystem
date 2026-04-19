import useAuth from "./useAuth";
import { RefreshTokenRequast } from "../Service/authApi";

const useRefreshToken = () => {
  const { setAuth } = useAuth();

  const refresh = async () => {
    try {
      const response = await RefreshTokenRequast();
      console.log(response);
      setAuth((prev) => ({
        ...prev,
        accessToken: response.data.accessToken,
        roles: response.data.role,
        user: {
          ...prev.user,
          username: response.data.username,
          email: response.data.email,
          role: response.data.role,
        },
      }));

      return response.data.accessToken;
    } catch (err) {
      console.log("Refresh Token Error:", err);
      return null;
    }
  };

  return refresh;
};

export default useRefreshToken;
