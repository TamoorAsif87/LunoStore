import { useParams } from "react-router-dom";
import { useTitle } from "../hooks/useTitle";

function CheckoutComplete() {
  useTitle("CheckoutComplete | LunoStore");
  const { id } = useParams<{ id: string }>();

  return (
    <div className="p-4 text-center">
      <h1 className="text-2xl font-bold text-green-600 mb-2">
        Checkout Complete
      </h1>
      <p className="text-lg">✅ Your order has been placed successfully!</p>
      {id && (
        <p className="mt-2 text-gray-700">
          🧾 Your Order ID is: <span className="font-mono">{id}</span>
        </p>
      )}
    </div>
  );
}

export default CheckoutComplete;
