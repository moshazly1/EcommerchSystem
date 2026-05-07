import { Route, Routes } from "react-router-dom";

import Layout from "./Components/Layout/Layout";

// Public Pages
import HomePage from "./Pages/HomePage";
import Laptop from "./Pages/Laptop";
import PCs from "./Pages/PCs";
import Mice from "./Pages/Mice";
import Accessoriers from "./Pages/Accessoriers";
import Supports from "./Pages/Support";

// Auth Pages
import LoginPage from "./Features/Auth/Components/LoginPage";
import Register from "./Features/Auth/Components/Register";
import ResetPassword from "./Features/Auth/Components/Pages/ResetPassword";
import ForgetPassword from "./Features/Auth/Components/Pages/ForgetPassword";

// Protected Pages
import Profile from "./Pages/Profile";
import Card from "./Pages/Card";

// Auth Protection
import RequireAuth from "./Features/Auth/Components/RequirAuth";
import PersistLogin from "./Features/Auth/hooks/PersistLogin";

// Error Pages
import NotFound from "./Pages/error/NotFont";
import UnAuthorized from "./Pages/error/unauthorized";

const ROLES = {
  Admin: 1,
  User: 2,
};

export default function App() {
  return (
    <Routes>
      {/* Layout */}
      <Route element={<Layout />}>
        {/* ================= PUBLIC ROUTES ================= */}
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<Register />} />

        <Route path="/Laptop" element={<Laptop />} />
        <Route path="/PCs" element={<PCs />} />
        <Route path="/Mice" element={<Mice />} />
        <Route path="/Accessories" element={<Accessoriers />} />
        <Route path="/Support" element={<Supports />} />

        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="/forgetPassword" element={<ForgetPassword />} />

        {/* ================= PROTECTED ROUTES ================= */}
        <Route element={<PersistLogin />}>
          {/* ===== USER + ADMIN ===== */}
          <Route
            element={<RequireAuth allowedRoles={[ROLES.User, ROLES.Admin]} />}
          >
            <Route path="/Profile" element={<Profile />} />
            <Route path="/Card" element={<Card />} />
          </Route>

          {/* ===== ADMIN ONLY ===== */}
          <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
            <Route path="/dashboard" element={<div>Dashboard</div>} />
          </Route>
        </Route>
      </Route>

      {/* ================= ERROR ROUTES ================= */}
      <Route path="/unauthorized" element={<UnAuthorized />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}
