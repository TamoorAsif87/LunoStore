import { useParams } from "react-router-dom";
import { useGetOrderById } from "../features/orders/hooks/useGetOrderById";
import { useTitle } from "../hooks/useTitle";

function ViewOrder() {
  useTitle("Order | LunoStore");
  const { id } = useParams<{ id: string }>();
  const { data: order, isPending: isLoading, isError } = useGetOrderById(id!);

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <span className="loading loading-spinner loading-lg text-primary"></span>
      </div>
    );
  }

  if (isError || !order) {
    return (
      <div className="min-h-screen flex items-center justify-center text-red-500">
        Failed to load order details.
      </div>
    );
  }

  return (
    <div className="max-w-[1100px] mx-auto px-4 py-8 space-y-10">
      <h1 className="text-3xl font-bold text-primary">Order Details</h1>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
        {/* Shipping Address */}
        <div className="card bg-base-100 shadow p-5">
          <h2 className="text-xl font-semibold mb-3 text-primary">
            Shipping Address
          </h2>
          <p>
            {order.shippingAddress.firstName} {order.shippingAddress.lastName}
          </p>
          <p>{order.shippingAddress.emailAddress}</p>
          <p>{order.shippingAddress.addressLine}</p>
          <p>
            {order.shippingAddress.city}, {order.shippingAddress.state},{" "}
            {order.shippingAddress.zipCode}
          </p>
          <p>{order.shippingAddress.country}</p>
        </div>

        {/* Billing Address */}
        <div className="card bg-base-100 shadow p-5">
          <h2 className="text-xl font-semibold mb-3 text-primary">
            Billing Address
          </h2>
          <p>
            {order.billingsAddress.firstName} {order.billingsAddress.lastName}
          </p>
          <p>{order.billingsAddress.emailAddress}</p>
          <p>{order.billingsAddress.addressLine}</p>
          <p>
            {order.billingsAddress.city}, {order.billingsAddress.state},{" "}
            {order.billingsAddress.zipCode}
          </p>
          <p>{order.billingsAddress.country}</p>
        </div>

        {/* Payment Info */}
        <div className="card bg-base-100 shadow p-5 md:col-span-2">
          <h2 className="text-xl font-semibold mb-3 text-primary">
            Payment Information
          </h2>
          <p>
            <strong>Card Name:</strong> {order.payment.cardName || "N/A"}
          </p>
          <p>
            <strong>Card Number:</strong> **** **** ****{" "}
            {order.payment.cardNumber.slice(-4)}
          </p>
          <p>
            <strong>Expiration:</strong> {order.payment.expiration}
          </p>
          <p>
            <strong>CVV:</strong> ***
          </p>
        </div>
      </div>

      {/* Order Items */}
      <div className="card bg-base-100 shadow p-5">
        <h2 className="text-xl font-semibold mb-5 text-primary">Items</h2>
        <div className="space-y-6">
          {order.items.map((item) => (
            <div
              key={item.productId}
              className="flex flex-col md:flex-row items-center gap-5 border-b pb-4"
            >
              <div className="w-24 h-24 flex-shrink-0 overflow-hidden rounded">
                <img
                  src={item.productImages[0]}
                  alt={item.productName}
                  className="object-cover w-full h-full"
                />
              </div>
              <div className="flex-1">
                <h3 className="text-lg font-semibold">{item.productName}</h3>
                <p className="text-sm text-gray-500">
                  Product ID: {item.productId}
                </p>
                <p className="text-sm text-gray-500">
                  Order ID: {item.orderId}
                </p>
                <p className="text-sm text-gray-500">
                  Quantity: {item.quantity}
                </p>
                <p className="text-sm text-gray-500">
                  Price: ${item.price.toFixed(2)}
                </p>
                <div className="flex gap-2 mt-2">
                  {item.productColors.map((color, idx) => (
                    <span
                      key={idx}
                      className="w-5 h-5 rounded-full border"
                      style={{ backgroundColor: color }}
                    />
                  ))}
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default ViewOrder;
