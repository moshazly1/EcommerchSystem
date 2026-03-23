import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { User } from "../../../Pages/Context/Context";
import { LoginRequast } from "../Service/authApi";

export const useLogin = () => {
  const [formData, setForm] = useState({ email: "", password: "" });
  const [Message, setMessage] = useState("");
  const [isValid, setIsvaled] = useState(false);
  const [loading, setLoading] = useState(false);
  const UserNow = useContext(User);
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
      const Token = res.data.token;
      const userDetalse = res.data;

      console.log(res);
      console.log(Token);
      UserNow.setAuth({ accessToken: Token, user: userDetalse });

      if (res && res.data) {
        console.log(res.data);
        setMessage(res.data.mesage);
        setIsvaled(res.data.isAuthentication || false);

        if (res.data.isAuthentication) {
          navigate("/");
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
