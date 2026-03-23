import { Route, Routes } from "react-router-dom";
import LoginPage from "./Features/Auth/Components/LoginPage";
import HomePage from "./Pages/HomePage";
import Register from "./Features/Auth/Components/Register";
import RequireAuth from "./Features/Auth/Components/RequirAuth";
import PersistLogin from "./Features/Auth/hooks/PersistLogin";

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<Register />} />
      <Route element={<PersistLogin />}>
        <Route element={<RequireAuth />}>
          <Route path="/profile" element={<div> Profile Page </div>} />
        </Route>
        <Route path="/" element={<HomePage />} />
      </Route>
      <Route path="*" element={<div>404 - Page Not Found</div>} />
    </Routes>
  );
}
