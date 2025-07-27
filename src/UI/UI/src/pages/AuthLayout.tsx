import { Navigate, Outlet } from "react-router-dom";
import { getToken } from "../Utils/localStorage";

function AuthLayout() {
  const token = getToken();
  if (token) {
    return <Navigate to="/" replace />;
  }

  return (
    <div
      className="min-h-screen bg-cover bg-center flex items-center justify-center"
      style={{ backgroundImage: "url('/auth.jpg')" }}
    >
      <div className="bg-white/90 rounded-xl shadow-lg p-8 w-full max-w-md">
        <Outlet />
      </div>
    </div>
  );
}

export default AuthLayout;
