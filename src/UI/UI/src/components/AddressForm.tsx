import { useForm, FormProvider } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import type { RootState } from "../store";
import {
  setBillingAddress,
  setShippingAddress,
  type Address,
} from "../features/basket/basketCheckout";
import FormInput from "../components/FormInput";

interface AddressFormProps {
  type: "shipping" | "billing";
  onNext: () => void;
  onBack?: () => void;
}

export default function AddressForm({
  type,
  onNext,
  onBack,
}: AddressFormProps) {
  const dispatch = useDispatch();
  const address = useSelector((state: RootState) =>
    type === "shipping"
      ? state.basketCheckout.shippingAddress
      : state.basketCheckout.billingAddress
  );

  const methods = useForm<Address>({
    defaultValues: address,
    mode: "onBlur",
  });

  const { handleSubmit } = methods;

  const onSubmit = (data: Address) => {
    if (type === "shipping") dispatch(setShippingAddress(data));
    else dispatch(setBillingAddress(data));
    onNext();
  };

  return (
    <FormProvider {...methods}>
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <h2 className="text-lg font-bold">
          {type === "shipping" ? "Shipping Address" : "Billing Address"}
        </h2>

        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <FormInput
            name="firstName"
            label="First Name"
            type="text"
            required
            maxLength={50}
          />
          <FormInput
            name="lastName"
            label="Last Name"
            type="text"
            required
            maxLength={50}
          />
        </div>

        <FormInput
          name="emailAddress"
          label="Email"
          type="email"
          required
          maxLength={50}
        />
        <FormInput
          name="addressLine"
          label="Address"
          type="text"
          required
          maxLength={180}
        />

        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <FormInput
            name="city"
            label="City"
            type="text"
            required
            maxLength={50}
          />
          <FormInput
            name="state"
            label="State"
            type="text"
            required
            maxLength={50}
          />
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <FormInput
            name="zipCode"
            label="ZIP Code"
            type="text"
            required
            maxLength={5}
          />
          <FormInput
            name="country"
            label="Country"
            type="text"
            required
            maxLength={50}
          />
        </div>

        <div className="flex justify-between mt-4">
          {onBack && (
            <button type="button" onClick={onBack} className="btn btn-outline">
              Back
            </button>
          )}
          <button type="submit" className="btn btn-primary">
            {onBack ? "Next" : "Continue"}
          </button>
        </div>
      </form>
    </FormProvider>
  );
}
