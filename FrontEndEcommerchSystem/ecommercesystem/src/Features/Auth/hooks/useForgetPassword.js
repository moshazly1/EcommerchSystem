import { useState } from "react";

export const useForgotPassword = () => {
  const [email, setEmail] = useState();
  const [loading, setLoading] = useState(false);
  const [Message, setMessage] = useState();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    const Requast;

    setLoading(false);
  };
};
