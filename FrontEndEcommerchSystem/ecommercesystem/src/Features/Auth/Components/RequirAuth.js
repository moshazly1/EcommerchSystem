import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth"; // الـ Hook اللي عملناه عشان نوصل للـ Context

const RequireAuth = ({ allowedRoles }) => {
  const { auth } = useAuth(); // بنجيب بيانات الـ Auth من الـ Context
  const location = useLocation(); // عشان نعرف اليوزر كان رايح فين قبل ما نطرده

  return allowedRoles.includes(auth?.roles) ? (
    <Outlet />
  ) : auth?.user ? (
    <Navigate to="/unauthorized" state={{ from: location }} replace />
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
};

export default RequireAuth;
