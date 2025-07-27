import { NavLink } from "react-router-dom";
import { FiShoppingCart } from "react-icons/fi";
import ProfileDropdown from "./Profile";
import { links } from "../data/navbar-links";
import { useSelector } from "react-redux";
import type { RootState } from "../store";
import { useEffect } from "react";
import { useAppDispatch } from "../store/hook";
import { getBasket } from "../features/basket/basket";
import { useAuth } from "../features/auth/hooks/useAuth";

const leftLinks = links.slice(0, 4); // Home, Products, Contact, About
const rightLinks = links.slice(4); // Register, Login

function Navbar() {
  const { user } = useAuth();
  const dispatch = useAppDispatch();
  const cart = useSelector((state: RootState) => state.basket.cart);
  const cartItemsCount =
    useSelector((state: RootState) => state.basket.cart.totalItems) || 0;

  useEffect(() => {
    if (cart.userName) {
      dispatch(getBasket(cart.userName));
    }
  }, [cart.userName, dispatch]);

  return (
    <div className="bg-base-100 shadow">
      <div className="navbar max-w-[1200px] mx-auto px-4">
        {/* Left: Logo and Nav Links */}
        <div className="flex-1 flex items-center gap-6">
          <NavLink
            to="/"
            className="text-4xl font-extrabold tracking-tight uppercase transition duration-300 hover:scale-105"
            style={{
              color: "oklch(45% 0.24 277.023)",
              textShadow: "0 1px 8px oklch(45% 0.24 277.023)",
            }}
          >
            LunoStore
          </NavLink>
          <ul className="menu menu-horizontal hidden lg:flex gap-3 text-lg font-medium">
            {leftLinks.map((l) => (
              <li key={l.name}>
                <NavLink
                  to={l.link}
                  className={({ isActive }) =>
                    isActive
                      ? "text-primary font-semibold"
                      : "hover:text-primary"
                  }
                >
                  {l.name}
                </NavLink>
              </li>
            ))}
          </ul>
        </div>

        {/* Right: Cart, Register, Login */}
        <div className="flex-none flex items-center gap-2 text-lg">
          <NavLink
            to="/basket"
            className="btn btn-ghost btn-circle text-xl relative"
          >
            <FiShoppingCart className="text-2xl" />
            {cartItemsCount > 0 && (
              <span className="absolute -top-1 -right-1 bg-red-500 text-white text-[10px] leading-none rounded-full w-5 h-5 flex items-center justify-center border-2 border-base-100">
                {cartItemsCount}
              </span>
            )}
          </NavLink>

          {user ? (
            <ProfileDropdown />
          ) : (
            rightLinks.map((l) => (
              <NavLink
                key={l.name}
                to={l.link}
                className={({ isActive }) =>
                  `btn btn-ghost text-base ${
                    isActive ? "text-primary font-semibold" : ""
                  }`
                }
              >
                {l.name}
              </NavLink>
            ))
          )}
        </div>

        {/* Mobile Dropdown */}
        <div className="lg:hidden ml-2">
          <div className="dropdown dropdown-end">
            <label tabIndex={0} className="btn btn-ghost">
              <svg
                className="h-6 w-6"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M4 6h16M4 12h16M4 18h16"
                />
              </svg>
            </label>
            <ul
              tabIndex={0}
              className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52 text-base"
            >
              {[...leftLinks].map((l) => (
                <li key={l.name}>
                  <NavLink
                    to={l.link}
                    className={({ isActive }) =>
                      isActive ? "text-primary font-semibold" : ""
                    }
                  >
                    {l.name}
                  </NavLink>
                </li>
              ))}
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Navbar;
