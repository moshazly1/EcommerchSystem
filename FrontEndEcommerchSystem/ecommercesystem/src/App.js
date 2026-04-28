import { Route, Routes } from "react-router-dom";
import LoginPage from "./Features/Auth/Components/LoginPage";
import HomePage from "./Pages/HomePage";
import Register from "./Features/Auth/Components/Register";
import RequireAuth from "./Features/Auth/Components/RequirAuth";
import PersistLogin from "./Features/Auth/hooks/PersistLogin";
import Unauthorized from "./Features/Auth/Components/Unauthorized";
import ResetPassword from "./Features/Auth/Components/Pages/ResetPassword";
import ForgetPassword from "./Features/Auth/Components/Pages/ForgetPassword";
import Layout from "./Components/Layout/Layout";
import Laptop from "./Pages/Laptop";
import PCs from "./Pages/PCs";
import Mice from "./Pages/Mice";
import Accessoriers from "./Pages/Accessoriers";
import Supports from "./Pages/Support";
import Profile from "./Pages/Profile";
import Card from "./Pages/Card";

const ROLES = {
  Admin: 1,
  User: 2,
};
export default function App() {
  return (
    <Routes>
      {/* public page  */}
      <Route element={<Layout />}>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<Register />} />
        <Route path="/" element={<HomePage />} />
        <Route path="/Laptop" element={<Laptop />} />
        <Route path="/PCs" element={<PCs />} />
        <Route path="/Mice" element={<Mice />} />
        <Route path="/Accessories" element={<Accessoriers />} />
        <Route path="/Support" element={<Supports />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="/forgetPassword" element={<ForgetPassword />} />

        {/* protected page  */}
        <Route element={<PersistLogin />}>
          <Route element={<RequireAuth allowedRoles={[ROLES.User]} />}>
            <Route path="/Profile" element={<Profile />} />
            <Route path="/Card" element={<Card />} />
          </Route>
        </Route>
        <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
          <Route path="/dashboard" element={<div>dashbord</div>} />
        </Route>
      </Route>
      <Route path="/unauthorized" element={<Unauthorized />} />
      <Route path="*" element={<div>404 - Page Not Found</div>} />
    </Routes>
  );
}
