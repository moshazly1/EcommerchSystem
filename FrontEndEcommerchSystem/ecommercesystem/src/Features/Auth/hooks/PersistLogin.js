import { useEffect, useState } from "react";
import useRefreshToken from "./useRefreshToken";
import useAuth from "./useAuth";
import Loader from "../../../Components/Loader/Loading";
import { Outlet } from "react-router-dom";

const PersistLogin = () => {
  const [isLoading, setIsLoading] = useState(true);
  const refresh = useRefreshToken();
  const { auth } = useAuth();

  useEffect(() => {
    const verifyRefreshToken = async () => {
      try {
        await refresh();
      } catch (err) {
        console.error("Refresh token failed or expired:", err);
      } finally {
        setIsLoading(false);
      }
    };

    if (!auth?.accessToken) {
      verifyRefreshToken();
    } else {
      setIsLoading(false);
    }
  }, []);

  return isLoading ? (
    <div className="text-center mt-5">
      <Loader />
    </div>
  ) : (
    <Outlet />
  );
};

export default PersistLogin;
