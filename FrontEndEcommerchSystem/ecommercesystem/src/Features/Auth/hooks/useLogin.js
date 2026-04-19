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
      console.log(res);
      const Token = res?.data?.accessToken;
      const userDetalse = res.data;
      const roles = res?.data?.role;

      console.log(res);
      console.log(Token);
      setAuth({ accessToken: Token, user: userDetalse, roles });

      if (res && res.data) {
        console.log(res.data);
        setMessage(res.data.mesage);
        setIsvaled(res.data.isAuthentication || false);

        if (res.data.isAuthentication) {
          if (Number(res.data.role) === 1) {
            navigate("/");
          } else {
            navigate("/");
          }
        }
      }
    } catch (err) {
      if (err.response && err.response.data) {
        setMessage(err.response.data);
      } else {
        setMessage("Server not reachable");
      }
      setIsvaled(false);
      console.log(err);
    } finally {
      setLoading(false);
    }
  };
  return { formData, HandelSubmit, HandelChange, loading, Message, isValid };
};
