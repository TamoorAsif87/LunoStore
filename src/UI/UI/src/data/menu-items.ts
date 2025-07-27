import {
  FiHome,
  FiPackage,
  FiList,
  FiUsers,
  FiShoppingCart,
} from "react-icons/fi";
import type { IconType } from "react-icons";

type MenuItem = {
  name: string;
  to: string;
  icon: IconType;
};

export const menuItems: MenuItem[] = [
  { name: "Home", to: "/admin/dashboard", icon: FiHome },
  { name: "Products", to: "/admin/products", icon: FiPackage },
  { name: "Categories", to: "/admin/categories", icon: FiList },
  { name: "Users", to: "/admin/users", icon: FiUsers },
  { name: "Orders", to: "/admin/orders", icon: FiShoppingCart },
];
