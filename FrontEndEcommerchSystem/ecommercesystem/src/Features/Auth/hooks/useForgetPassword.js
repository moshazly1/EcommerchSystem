import { useState } from "react";
import { ForgetPassword } from "../Service/authApi";
import { useNavigate } from "react-router-dom";

export const useForgotPassword = () => {
  const [email, setEmail] = useState();
  const [loading, setLoading] = useState(false);
  const [isValid, setIsvaled] = useState(false);
  const [Message, setMessage] = useState();
  const navigate = useNavigate();
  const HandelChange = (e) => {
    setEmail(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    setLoading(true);
    try {
      const Requast = await ForgetPassword(email);
      setMessage(Requast.data.message);
      setIsvaled(true);
      console.log(Requast);
      setTimeout(() => navigate("/login"), 2000);
    } catch (err) {
      setMessage(err.response?.data?.message || "Something went wrong");
      setIsvaled(false);
      console.log(err);
    } finally {
      setLoading(false);
    }
  };
  return { loading, Message, handleSubmit, HandelChange, isValid };
};
