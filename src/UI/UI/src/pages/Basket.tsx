import { useSelector } from "react-redux";
import type { RootState } from "../store";
import { Link } from "react-router-dom";
import {
  decreaseItemQuantity,
  getBasket,
  increaseItemQuantity,
  removeItem,
} from "../features/basket/basket";
import { useAppDispatch } from "../store/hook";
import { useEffect } from "react";
import Loading from "../components/Loading";
import { useTitle } from "../hooks/useTitle";

function Basket() {
  useTitle("basket | LunoStore");
  const dispatch = useAppDispatch();
  const { items, totalPrice, userName } = useSelector(
    (state: RootState) => state.basket.cart
  );
  const isLoading = useSelector((state: RootState) => state.basket.loading);

  useEffect(() => {
    if (userName) {
      dispatch(getBasket(userName));
    }
  }, [dispatch, userName]);

  const handleIncrease = async (productId: string) => {
    await dispatch(increaseItemQuantity({ userName, productId, quantity: 1 }));
  };

  const handleDecrease = async (productId: string) => {
    await dispatch(decreaseItemQuantity({ userName, productId, quantity: 1 }));
  };

  const handleRemove = (productId: string) => {
    dispatch(removeItem({ userName, productId }));
  };

  if (isLoading) return <Loading />;

  const hasItems = items?.length > 0;

  return (
    <section className="max-w-[1200px] mx-auto px-4 py-10">
      <h1 className="text-3xl font-bold text-gray-800 mb-8">Shopping Cart</h1>

      {hasItems ? (
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Cart Items */}
          <div className="lg:col-span-2 space-y-6">
            {[...items]
              .sort((a, b) => a.productId.localeCompare(b.productId))
              .map((item) => {
                return (
                  <div
                    key={item.productId}
                    className="flex flex-col sm:flex-row items-center justify-between gap-4 p-4 bg-white rounded-xl shadow-sm border"
                  >
                    <div className="flex items-center gap-4 w-full">
                      <img
                        src={item.productImages?.[0]}
                        alt={item.productName}
                        className="w-20 h-20 object-cover rounded-md border"
                      />
                      <div className="flex-1">
                        <h2 className="font-semibold text-lg">
                          {item.productName}
                        </h2>
                        <p className="text-sm text-gray-500">
                          Unit Price: ${item.price.toFixed(2)}
                        </p>
                        <div className="flex items-center gap-2 mt-2">
                          <button
                            onClick={() => handleDecrease(item.productId)}
                            className="btn btn-sm btn-outline"
                            disabled={item.quantity === 1 || isLoading}
                          >
                            -
                          </button>
                          <span className="font-semibold">{item.quantity}</span>
                          <button
                            onClick={() => handleIncrease(item.productId)}
                            className="btn btn-sm btn-outline"
                            disabled={isLoading}
                          >
                            +
                          </button>
                          <button
                            onClick={() => handleRemove(item.productId)}
                            className="btn btn-sm btn-error ml-4"
                          >
                            Remove
                          </button>
                        </div>
                      </div>
                    </div>
                    <div className="text-right min-w-[80px]">
                      <p className="text-lg font-medium text-primary">
                        ${(item.price * item.quantity).toFixed(2)}
                      </p>
                    </div>
                  </div>
                );
              })}
          </div>

          {/* Summary */}
          <div className="bg-base-100 rounded-xl p-6 shadow border">
            <h2 className="text-xl font-semibold mb-4">Order Summary</h2>
            <div className="space-y-3 text-sm text-gray-700">
              <div className="flex justify-between">
                <span>Total Items</span>
                <span>{items.length}</span>
              </div>
              <div className="flex justify-between font-semibold">
                <span>Total Price</span>
                <span>${totalPrice?.toFixed(2)}</span>
              </div>
            </div>
            <Link to="/checkout" className="btn btn-primary mt-6 w-full">
              Proceed to Checkout
            </Link>
          </div>
        </div>
      ) : (
        <div className="text-center py-20 text-gray-600">
          <p className="text-xl mb-4">Your basket is empty.</p>
          <Link to="/products" className="btn btn-primary">
            Continue Shopping
          </Link>
        </div>
      )}
    </section>
  );
}

export default Basket;
