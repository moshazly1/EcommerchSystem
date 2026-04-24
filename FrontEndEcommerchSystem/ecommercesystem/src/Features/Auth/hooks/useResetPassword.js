import { useState } from "react";
import { ResetPasswordRequest } from "../Service/authApi";
import { useNavigate, useSearchParams } from "react-router-dom";

const useResetPassword = () => {
  const [newPassword, setNewPass] = useState("");
  const [ConfirmPassword, setConfirmPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");
  const [isValid, setIsValid] = useState(false);
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const token = searchParams.get("token");
  const email = searchParams.get("email");
  console.log(token);
  console.log(email);

  const HandelFunction = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");

    if (newPassword.length < 8) {
      setMessage("Password must be at least 8 characters!");
      setIsValid(false);
      setLoading(false);
      return;
    }

    if (!/[A-Z]/.test(newPassword)) {
      setMessage("Password must contain at least one uppercase letter!");
      setIsValid(false);
      setLoading(false);
      return;
    }

    if (!/[a-z]/.test(newPassword)) {
      setMessage("Password must contain at least one lowercase letter!");
      setIsValid(false);
      setLoading(false);
      return;
    }

    if (!/[0-9]/.test(newPassword)) {
      setMessage("Password must contain at least one number!");
      setIsValid(false);
      setLoading(false);
      return;
    }

    if (newPassword !== ConfirmPassword) {
      setMessage("Passwords do not match!");
      setIsValid(false);
      setLoading(false);
      return;
    }

    try {
      const response = await ResetPasswordRequest({
        email,
        token,
        newPassword,
      });
      setMessage(response.data.message || "Password reset successfully!");
      setIsValid(true);
      console.log(response.data);
      setTimeout(() => navigate("/login"), 2000);
    } catch (err) {
      console.log(err.response.data);
      setMessage(err.response?.data || "Something went wrong!");
      setIsValid(false);
    } finally {
      setLoading(false);
    }
  };

  return {
    newPassword,
    setNewPass,
    ConfirmPassword,
    setConfirmPassword,
    loading,
    message,
    isValid,
    HandelFunction,
  };
};

export default useResetPassword;
