import { Link } from "react-router-dom";
import { FaUsers, FaClipboardList, FaBoxOpen, FaTags } from "react-icons/fa"; // FaTags for categories
import { useTitle } from "../hooks/useTitle";

function AdminHome() {
  useTitle("Admin | Home | LunoStore");
  const cards = [
    {
      title: "Manage Users",
      icon: <FaUsers className="w-8 h-8 text-blue-600" />,
      to: "/admin/users",
    },
    {
      title: "Manage Orders",
      icon: <FaClipboardList className="w-8 h-8 text-green-600" />,
      to: "/admin/orders",
    },
    {
      title: "Manage Products",
      icon: <FaBoxOpen className="w-8 h-8 text-purple-600" />,
      to: "/admin/products",
    },
    {
      title: "Manage Categories",
      icon: <FaTags className="w-8 h-8 text-pink-600" />,
      to: "/admin/categories",
    },
  ];

  return (
    <div className="max-w-[1100px] mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-8">Admin Dashboard</h1>

      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
        {cards.map((card) => (
          <Link
            to={card.to}
            key={card.title}
            className="p-6 border rounded-2xl shadow hover:shadow-md transition bg-white hover:bg-gray-50"
          >
            <div className="flex items-center gap-4">
              {card.icon}
              <span className="text-lg font-medium text-gray-800">
                {card.title}
              </span>
            </div>
          </Link>
        ))}
      </div>
    </div>
  );
}

export default AdminHome;
