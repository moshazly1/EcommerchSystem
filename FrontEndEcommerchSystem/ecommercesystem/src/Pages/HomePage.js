import { useNavigate } from "react-router-dom";
import Loading from "../Components/Loader/Loading";
import useAuth from "../Features/Auth/hooks/useAuth";
import useLogout from "../Features/Auth/hooks/useLogout";
import Users from "./Users";

export default function HomePage() {
  const logout = useLogout();
  const { auth } = useAuth();
  const navegate = useNavigate();

  const signOut = async () => {
    await logout();
    // navegate("/login");
  };

  console.log("Current Auth State:", auth?.accessToken);

  return (
    <>
      <h1>HomePage</h1>
      <br />
      <Users />
      <button onClick={signOut}>Sign Out</button>
    </>
  );
}
