import { useState } from "react";
import { useNavigate } from "react-router-dom";
import useAuth from "./useAuth";
import { LoginRequast } from "../Service/authApi";

export const useLogin = () => {
  const { setAuth } = useAuth();
  const [formData, setForm] = useState({ email: "", password: "" });
  const [Message, setMessage] = useState("");
  const [isValid, setIsvaled] = useState(false);
  const [loading, setLoading] = useState(false);
  // const UserNow = useContext(User);
  const navigate = useNavigate();

  const HandelChange = (e) => {
    setForm({ ...formData, [e.target.name]: e.target.value });
  };

  const HandelSubmit = async (e) => {
    e.preventDefault();

    setLoading(true);
    setMessage("");

    try {
      const res = await LoginRequast(formData);

      await console.log("Login response:", res.data);

      //  2FA Required

      if (res.data.requiresTwoFactor == true) {
        setMessage(res.data.mesage);
        console.log("🚀 Going to Verify 2FA");
        navigate("/verify-2fa", {
          state: {
            email: formData.email,
            expiresAt: res.data.twoFactorCodeExpiresAt,
          },
        });

        return;
      }

      //  Normal Login Success

      if (res.data.isAuthentication) {
        const token = res.data.accessToken;
        const userDetails = res.data;
        const roles = res.data.role;

        setAuth({
          accessToken: token,
          user: userDetails,
          roles,
        });

        setMessage(res.data.mesage);
        setIsvaled(true);

        if (Number(roles) === 1) {
          navigate("/dashboard");
        } else if (Number(roles) === 2) {
          navigate("/");
        } else {
          navigate("/unauthorized");
        }

        return;
      }

      //  Login Failed

      setMessage(res.data.mesage || "Login failed.");
      setIsvaled(false);
    } catch (err) {
      console.log("Login error:", err);

      if (err.response?.data) {
        setMessage(
          typeof err.response.data === "string"
            ? err.response.data
            : err.response.data.message || "Login failed.",
        );
      } else {
        setMessage("Server not reachable");
      }

      setIsvaled(false);
    } finally {
      setLoading(false);
    }
  };
  return { formData, HandelSubmit, HandelChange, loading, Message, isValid };
};
