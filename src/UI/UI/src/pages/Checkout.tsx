import { useEffect, useState } from "react";

import { useSelector } from "react-redux";
import type { RootState } from "../store";
import { MdLocalShipping, MdPayment, MdHome } from "react-icons/md";
import AddressForm from "../components/AddressForm";
import PaymentForm from "../components/PaymentFrom";
import { useAppDispatch } from "../store/hook";
import {
  loadFromBasket,
  setCustomerId,
} from "../features/basket/basketCheckout";
import { getUserId, removeBasketUserName } from "../Utils/localStorage";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import { clearBasket } from "../features/basket/basket";
import { useTitle } from "../hooks/useTitle";

export default function Checkout() {
  useTitle("CheckOut | LunoStore");
  const [step, setStep] = useState(1);
  const dispatch = useAppDispatch();
  const { userName, items, totalPrice } = useSelector(
    (state: RootState) => state.basket.cart
  );
  const customerId = getUserId();
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(
      loadFromBasket({
        userName: userName,
        totalPrice,
        items,
      })
    );
  }, [dispatch, items, totalPrice, userName]);

  useEffect(() => {
    if (customerId) {
      dispatch(setCustomerId({ customerId: customerId }));
    }
  }, [customerId, dispatch]);

  const next = () => setStep((s) => s + 1);
  const back = () => setStep((s) => s - 1);

  const steps = [
    { label: "Shipping", icon: <MdLocalShipping size={20} /> },
    { label: "Billing", icon: <MdHome size={20} /> },
    { label: "Payment", icon: <MdPayment size={20} /> },
  ];

  const renderStep = () => {
    switch (step) {
      case 1:
        return <AddressForm type="shipping" onNext={next} />;
      case 2:
        return <AddressForm type="billing" onNext={next} onBack={back} />;
      case 3:
        return (
          <PaymentForm
            onNext={(orderId: string) => {
              dispatch(clearBasket());
              removeBasketUserName();
              toast.success("Your order is completed");
              navigate(`/checkout/complete/${orderId}`);
            }}
            onBack={back}
          />
        );
      default:
        return null;
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-4">
      <div className="flex justify-between mb-8">
        {steps.map((s, idx) => {
          const stepNum = idx + 1;
          const isActive = step === stepNum;
          const isComplete = step > stepNum;

          return (
            <div
              key={s.label}
              className="flex flex-col items-center text-center w-full relative"
            >
              <div
                className={`rounded-full w-10 h-10 flex items-center justify-center text-white z-10 ${
                  isComplete
                    ? "bg-green-500"
                    : isActive
                    ? "bg-primary"
                    : "bg-gray-300"
                }`}
              >
                {s.icon}
              </div>
              <p className="text-sm mt-2">{s.label}</p>

              {stepNum < steps.length && (
                <div
                  className={`absolute top-5 left-1/2 right-0 h-1 ${
                    step > stepNum ? "bg-green-500" : "bg-gray-300"
                  }`}
                ></div>
              )}
            </div>
          );
        })}
      </div>

      {/* Step Content */}
      <div className="bg-base-100 p-6 rounded-lg shadow">{renderStep()}</div>
    </div>
  );
}
