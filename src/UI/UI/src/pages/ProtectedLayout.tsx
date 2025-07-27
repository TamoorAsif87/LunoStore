import { Navigate, Outlet, useLocation } from "react-router-dom";
import { getToken } from "../Utils/localStorage";

export function ProtectedLayout() {
  const token = getToken();
  const location = useLocation();

  if (!token) {
    return <Navigate to="/auth/login" state={{ from: location }} replace />;
  }

  return <Outlet />;
}
