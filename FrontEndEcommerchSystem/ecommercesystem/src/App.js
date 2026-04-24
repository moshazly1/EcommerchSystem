import { Route, Routes } from "react-router-dom";
import LoginPage from "./Features/Auth/Components/LoginPage";
import HomePage from "./Pages/HomePage";
import Register from "./Features/Auth/Components/Register";
import RequireAuth from "./Features/Auth/Components/RequirAuth";
import PersistLogin from "./Features/Auth/hooks/PersistLogin";
import Unauthorized from "./Features/Auth/Components/Unauthorized";
import ResetPassword from "./Features/Auth/Components/Pages/ResetPassword";
import ForgetPassword from "./Features/Auth/Components/Pages/ForgetPassword";

const ROLES = {
  Admin: 1,
  User: 2,
};
export default function App() {
  return (
    <Routes>
      {/* public page  */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<Register />} />
      {/* <Route path="/" element={<HomePage />} /> */}
      <Route path="/reset-password" element={<ResetPassword />} />
      <Route path="/forgetPassword" element={<ForgetPassword />} />
      <Route path="/unauthorized" element={<Unauthorized />} />

      {/* protected page  */}
      <Route element={<PersistLogin />}>
        <Route element={<RequireAuth allowedRoles={[ROLES.User]} />}>
          <Route path="/profile" element={<div> Profile Page </div>} />
        </Route>
        <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/dashboard" element={<div>dashbord</div>} />
        </Route>
      </Route>
      <Route path="*" element={<div>404 - Page Not Found</div>} />
    </Routes>
  );
}
