import { Navigate, Outlet } from "react-router-dom";
import { isAdmin } from "../Utils/localStorage";
import Sidebar from "../components/Sidebar";

function ProtectedAdmin() {
  const isUserAdmin = isAdmin();

  if (!isUserAdmin) {
    return <Navigate to="/" replace />;
  }

  return (
    <div className="min-h-screen flex">
      <Sidebar />
      <div className="flex-1 p-4">
        <Outlet />
      </div>
    </div>
  );
}

export default ProtectedAdmin;
