import { useState } from "react";
import { useGetAllOrders } from "../features/orders/hooks/useGetAllOrders"; // create this
import Loading from "../components/Loading";
import Pagination from "../components/Pagination";
import { Link } from "react-router-dom";
import { FaEdit, FaTrash } from "react-icons/fa";
import { useDeleteOrder } from "../features/orders/hooks/useDeleteOrder";
import { useTitle } from "../hooks/useTitle";

const MAX_SIZE = 5;

const Orders = () => {
  useTitle("Orders | LunoStore");
  const [page, setPage] = useState<number>(1);
  const { data: orders, isLoading } = useGetAllOrders();
  const { mutate: deleteOrder } = useDeleteOrder();
  const totalPages = orders ? Math.ceil(orders.length / MAX_SIZE) : 1;
  const paginatedOrders = orders?.slice((page - 1) * MAX_SIZE, page * MAX_SIZE);

  function handleDelete(id: string): void {
    if (confirm("Are you sure you want to delete this order?")) {
      deleteOrder(id);
    }
  }

  return (
    <div className="p-4 mt-5">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-2xl font-bold">Orders</h2>
      </div>

      {isLoading ? (
        <Loading />
      ) : (
        <>
          <div className="overflow-x-auto">
            <table className="table table-zebra w-full">
              <thead>
                <tr>
                  <th>#</th>
                  <th>Order ID</th>
                  <th>Customer</th>
                  <th>Total Items</th>
                  <th>Shipping City</th>
                  <th className="text-right">Actions</th>
                </tr>
              </thead>
              <tbody>
                {paginatedOrders?.map((order, index) => (
                  <tr key={order.orderId}>
                    <td>{(page - 1) * MAX_SIZE + index + 1}</td>
                    <td>{order.orderId}</td>
                    <td>{order.customerId || "Guest"}</td>
                    <td>{order.items.length}</td>
                    <td>{order.shippingAddress.city}</td>
                    <td className="text-right space-x-2">
                      <Link
                        className="btn btn-sm btn-outline btn-info"
                        to={`/admin/orders/${order.orderId}`}
                      >
                        <FaEdit />
                      </Link>

                      <button
                        onClick={() => handleDelete(order.orderId)}
                        className="btn btn-sm btn-outline btn-error"
                      >
                        <FaTrash />
                      </button>
                    </td>
                  </tr>
                ))}
                {orders?.length === 0 && (
                  <tr>
                    <td colSpan={6} className="text-center py-4">
                      No orders found.
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>

          {/* Pagination */}
          <Pagination
            totalPages={totalPages}
            page={page}
            onPageChange={setPage}
          />
        </>
      )}
    </div>
  );
};

export default Orders;
