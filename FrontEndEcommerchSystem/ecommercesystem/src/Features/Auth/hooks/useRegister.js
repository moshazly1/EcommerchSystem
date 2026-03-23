import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { RegisterRequast } from "../Service/authApi";
import useAuth from "./useAuth";
import { User } from "../../../Pages/Context/Context";

export const useRegister = () => {
  const [formData, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
  });
  const [Message, setMessage] = useState("");
  const [isValid, setIsvaled] = useState(false);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const { setAuth } = useAuth();
  const UserNow = useContext(User);
  function HandelChange(e) {
    setForm({ ...formData, [e.target.name]: e.target.value });
  }
  console.log(formData.name);
  async function HandelSubmit(e) {
    e.preventDefault();
    setLoading(true);
    setMessage("");
    try {
      const dataToSend = {
        name: formData.firstName + " " + formData.lastName,
        email: formData.email,
        password: formData.password,
      };

      const res = await RegisterRequast(dataToSend);
      const token = res.data.token;
      const userDetalse = res.data;

      UserNow.setAuth({ accessToken: token, user: userDetalse });
      console.log(userDetalse);
      console.log(token);
      if (res && res.data) {
        console.log(res.data);
        setMessage(res.data.mesage);
        setIsvaled(res.data.isAuthentication || false);

        if (res.data.isAuthentication) {
          navigate("/");
        }
      }
    } catch (err) {
      setIsvaled(false);
      setIsvaled(false);
      if (err.response && err.response.data) {
        setMessage(err.response.data);
      } else {
        setMessage("Server not reachable");
      }

      console.log(err);
    } finally {
      setLoading(false);
    }
  }

  return { HandelChange, HandelSubmit, Message, isValid, loading, formData };
};
