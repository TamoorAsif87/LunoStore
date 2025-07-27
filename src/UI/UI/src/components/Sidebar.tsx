import { NavLink } from "react-router-dom";
import { useState } from "react";

import { menuItems } from "../data/menu-items";
import { FiHome } from "react-icons/fi";

function Sidebar() {
  const [open, setOpen] = useState(false);

  return (
    <>
      <div className={`bg-base-200 p-4 w-64 space-y-4 hidden md:block`}>
        <h2 className="text-2xl font-bold mb-4">Admin Panel</h2>

        {/* Back to Site link */}
        <NavLink
          to="/"
          className="btn btn-sm btn-outline mb-4 flex items-center gap-2"
        >
          <FiHome /> Back to Site
        </NavLink>

        <ul className="menu text-lg">
          {menuItems.map((item) => (
            <li key={item.name}>
              <NavLink
                to={item.to}
                className={({ isActive }) =>
                  isActive ? "text-primary font-semibold" : "hover:text-primary"
                }
              >
                <span className="flex items-center gap-2">
                  <item.icon /> {item.name}
                </span>
              </NavLink>
            </li>
          ))}
        </ul>
      </div>

      {/* Mobile Sidebar Toggle */}
      <div className="md:hidden p-4">
        <button className="btn btn-outline" onClick={() => setOpen(!open)}>
          Menu
        </button>
        {open && (
          <div className="absolute bg-base-100 shadow-md p-4 mt-2 z-50 rounded w-60">
            <ul className="menu text-lg">
              {menuItems.map((item) => (
                <li key={item.name}>
                  <NavLink
                    to={item.to}
                    className={({ isActive }) =>
                      isActive
                        ? "text-primary font-semibold"
                        : "hover:text-primary"
                    }
                  >
                    <span className="flex items-center gap-2">
                      <item.icon /> {item.name}
                    </span>
                  </NavLink>
                </li>
              ))}
            </ul>
          </div>
        )}
      </div>
    </>
  );
}
export default Sidebar;
