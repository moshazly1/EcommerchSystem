import { useEffect, useState, useRef } from "react";
import useRefreshToken from "./useRefreshToken";
import useAuth from "./useAuth";
import Loader from "../../../Components/Loader/Loading";
import { Outlet } from "react-router-dom";

const PersistLogin = () => {
  const [isLoading, setIsLoading] = useState(true);
  const refresh = useRefreshToken();
  const { auth } = useAuth();
  const effectRan = useRef(false); // ✅ منع التكرار

  useEffect(() => {
    if (effectRan.current === true) return; // ✅ لو اتشغل قبل كده، وقف

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

    return () => {
      effectRan.current = true; // ✅ علّم إنه اتشغل
    };
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
