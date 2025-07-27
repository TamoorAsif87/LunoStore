import { Link } from "react-router-dom";
import {
  FaUser,
  FaSignOutAlt,
  FaKey,
  FaTachometerAlt,
  FaClipboardList,
} from "react-icons/fa"; // import key icon
import {
  getUser,
  removeToken,
  removeUser,
  type UserDto,
} from "../Utils/localStorage";
import { useEffect, useState } from "react";

const ProfileDropdown = () => {
  const [user, setUser] = useState<UserDto | null>(null);

  useEffect(() => {
    const storeUser = () => {
      const userData = getUser();
      if (!userData) {
        setUser(null);
        return;
      }
      setUser({ ...userData });
    };

    storeUser();
    // Listen for the custom event to update user profile
    window.addEventListener("userProfileUpdated", storeUser);

    return () => window.removeEventListener("userProfileUpdated", storeUser);
  }, []);

  const handleLogout = () => {
    removeToken();
    removeUser();
    window.location.href = "/";
  };

  const profileImage = user?.profileImage || "/default.png";

  return (
    <div className="dropdown dropdown-end">
      <div
        tabIndex={0}
        role="button"
        className="btn btn-ghost btn-circle avatar"
      >
        <div className="w-8 rounded-full ring ring-primary ring-offset-base-100 ring-offset-2">
          <img
            src={profileImage}
            alt="profile"
            className="w-full h-full object-cover rounded-full"
          />
        </div>
      </div>
      <ul
        tabIndex={0}
        className="mt-3 z-[1] p-2 shadow menu menu-sm dropdown-content bg-base-100 rounded-box w-52 text-base"
      >
        <li className="font-bold text-center pointer-events-none truncate">
          {user?.name}
        </li>

        {user?.role === "Admin" && (
          <li>
            <Link
              to="/admin"
              className="text-base tracking-wider flex items-center"
            >
              <FaTachometerAlt className="mr-2" /> Dashboard
            </Link>
          </li>
        )}

        <li>
          <Link to="/user/profile" className="text-base tracking-wider">
            <FaUser className="mr-2" /> Profile
          </Link>
        </li>

        <li>
          <Link to="user/change-password" className="text-base tracking-wider">
            <FaKey className="mr-2" /> Change Password
          </Link>
        </li>
        <li>
          <Link
            to={`user/orders/${user?.id}`}
            className="text-base tracking-wider"
          >
            <FaClipboardList className="mr-2" /> My Orders
          </Link>
        </li>

        <li>
          <button onClick={handleLogout} className="text-base tracking-wider">
            <FaSignOutAlt className="mr-2" /> Logout
          </button>
        </li>
      </ul>
    </div>
  );
};

export default ProfileDropdown;
