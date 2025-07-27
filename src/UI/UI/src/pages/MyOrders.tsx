import Loading from "../components/Loading";
import { useAuth } from "../features/auth/hooks/useAuth";
import { useQueryOrdersByCustomerId } from "../features/orders/hooks/useOrdersByCustomerId";
import { useTitle } from "../hooks/useTitle";

function MyOrders() {
  const { user } = useAuth();
  useTitle(`${user?.name} - Orders | LunoStore`);
  const {
    data: orders,
    isLoading,
    isError,
  } = useQueryOrdersByCustomerId(user?.id || "");

  if (isLoading) return <Loading />;
  if (isError)
    return <div className="text-red-500 p-4">Failed to load your orders.</div>;

  return (
    <div className="max-w-[1200px] mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6 text-center">My Orders</h1>

      {orders?.length === 0 ? (
        <p className="text-center text-gray-500">No orders found.</p>
      ) : (
        orders?.map((order, i) => (
          <div
            key={order.orderId}
            className="border rounded-lg p-5 mb-8 shadow-sm bg-white"
          >
            <div className="mb-4">
              <h2 className="text-xl font-semibold text-gray-800">
                Order #{i + 1}
              </h2>
              <p className="text-sm text-gray-500">Order ID: {order.orderId}</p>
              <p className="text-sm text-gray-500">
                Total Items: {order.items.length}
              </p>
              <p className="text-sm text-gray-500">
                Total: $
                {order.items
                  .reduce((sum, item) => sum + item.price * item.quantity, 0)
                  .toFixed(2)}
              </p>
            </div>

            <div className="grid gap-4">
              {order.items.map((item) => (
                <div
                  key={item.productId}
                  className="flex flex-col md:flex-row items-start gap-4 border-t pt-4"
                >
                  {item.productImages.length > 0 && (
                    <img
                      src={item.productImages[0]}
                      alt={item.productName}
                      className="w-24 h-24 object-cover rounded"
                    />
                  )}
                  <div className="flex-1">
                    <h3 className="text-md font-semibold text-gray-800">
                      {item.productName}
                    </h3>
                    <p className="text-sm text-gray-600">
                      Quantity: {item.quantity}
                    </p>
                    <p className="text-sm text-gray-600">
                      Price: ${item.price.toFixed(2)}
                    </p>
                    {item.productColors?.length > 0 && (
                      <p className="text-sm text-gray-600">
                        Colors: {item.productColors.join(", ")}
                      </p>
                    )}
                  </div>
                </div>
              ))}
            </div>

            {/* Shipping Address */}
            <div className="mt-6">
              <h3 className="font-semibold text-lg mb-2 text-gray-800">
                Shipping Address
              </h3>
              <p className="text-sm text-gray-600">
                {order.shippingAddress.firstName}{" "}
                {order.shippingAddress.lastName}
              </p>
              <p className="text-sm text-gray-600">
                {order.shippingAddress.emailAddress}
              </p>
              <p className="text-sm text-gray-600">
                {order.shippingAddress.addressLine},{" "}
                {order.shippingAddress.city}, {order.shippingAddress.state},{" "}
                {order.shippingAddress.country} -{" "}
                {order.shippingAddress.zipCode}
              </p>
            </div>

            {/* Billing Address */}
            <div className="mt-6">
              <h3 className="font-semibold text-lg mb-2 text-gray-800">
                Billing Address
              </h3>
              <p className="text-sm text-gray-600">
                {order.billingsAddress.firstName}{" "}
                {order.billingsAddress.lastName}
              </p>
              <p className="text-sm text-gray-600">
                {order.billingsAddress.emailAddress}
              </p>
              <p className="text-sm text-gray-600">
                {order.billingsAddress.addressLine},{" "}
                {order.billingsAddress.city}, {order.billingsAddress.state},{" "}
                {order.billingsAddress.country} -{" "}
                {order.billingsAddress.zipCode}
              </p>
            </div>

            {/* Payment Details */}
            <div className="mt-6">
              <h3 className="font-semibold text-lg mb-2 text-gray-800">
                Payment Details
              </h3>
              <p className="text-sm text-gray-600">
                Card Name: {order.payment.cardName || "N/A"}
              </p>
              <p className="text-sm text-gray-600">
                Card Number: **** **** **** {order.payment.cardNumber.slice(-4)}
              </p>
              <p className="text-sm text-gray-600">
                Expiration: {order.payment.expiration}
              </p>
            </div>
          </div>
        ))
      )}
    </div>
  );
}

export default MyOrders;
