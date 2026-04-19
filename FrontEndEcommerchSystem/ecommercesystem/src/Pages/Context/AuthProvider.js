import { createContext, useState } from "react";

export const AuthContext = createContext({});
export default function Authprovider({ children }) {
  const [auth, setAuth] = useState({});
  return (
    <AuthContext.Provider value={{ auth, setAuth }}>
      {children}
    </AuthContext.Provider>
  );
}
