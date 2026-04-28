import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { RegisterRequast } from "../Service/authApi";
import useAuth from "./useAuth";
import { AuthContext, User } from "../../../Context/AuthProvider";

export const useRegister = () => {
  const { setAuth } = useContext(AuthContext);
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

  // const UserNow = useContext(User);
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
      console.log(res);
      const Token = res?.data?.accessToken;
      const userDetalse = res.data;
      const roles = res?.data?.role;

      console.log(res);
      console.log(Token);
      setAuth({ accessToken: Token, user: userDetalse, roles });

      console.log(JSON.stringify(res));

      console.log(userDetalse);

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
