import { useForm, FormProvider } from "react-hook-form";
import { useSelector } from "react-redux";
import {
  setPaymentInfo,
  submitCheckout,
  type PaymentInfo,
} from "../features/basket/basketCheckout";
import { store, type RootState } from "../store";
import FormInput from "../components/FormInput";
import { useAppDispatch } from "../store/hook";
import Loading from "./Loading";

interface PaymentFormProps {
  onNext: (orderId: string) => void;
  onBack: () => void;
}

export default function PaymentForm({ onNext, onBack }: PaymentFormProps) {
  const dispatch = useAppDispatch();
  const payment = useSelector(
    (state: RootState) => state.basketCheckout.payment
  );

  const loading = useSelector(
    (state: RootState) => state.basketCheckout.loading
  );

  const methods = useForm<PaymentInfo>({
    defaultValues: payment,
    mode: "onBlur",
  });

  const { handleSubmit } = methods;

  const onSubmit = async (data: PaymentInfo) => {
    dispatch(setPaymentInfo({ ...data, expiration: `${data.expiration}-01` }));

    // Wait one tick to ensure state is updated
    await new Promise((resolve) => setTimeout(resolve, 0));

    const updated = store.getState().basketCheckout;

    const resultAction = await dispatch(submitCheckout(updated));

    if (submitCheckout.fulfilled.match(resultAction)) {
      const orderId = resultAction.payload as string;
      onNext(orderId);
    } else {
      console.error("Checkout failed:", resultAction.payload);
    }
  };

  if (loading) return <Loading />;

  return (
    <FormProvider {...methods}>
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <h2 className="text-lg font-bold">Payment Information</h2>

        <FormInput
          name="cardName"
          label="Name on Card"
          type="text"
          maxLength={50}
          placeholder="John Doe"
        />

        <FormInput
          name="cardNumber"
          label="Card Number"
          type="text"
          required
          maxLength={24}
          placeholder="1234 5678 9012 3456"
        />

        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <FormInput
            name="expiration"
            label="Expiration Date"
            type="month"
            required
          />
          <FormInput
            name="cvv"
            label="CVV"
            type="password"
            required
            maxLength={3}
            placeholder="123"
          />
        </div>

        <div className="flex justify-between mt-4">
          <button type="button" onClick={onBack} className="btn btn-outline">
            Back
          </button>
          <button type="submit" className="btn btn-primary">
            Next
          </button>
        </div>
      </form>
    </FormProvider>
  );
}
