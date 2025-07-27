import {
  createAsyncThunk,
  createSlice,
  type PayloadAction,
} from "@reduxjs/toolkit";
import type { ShoppingCartItem } from "./basket";
import { axiosFetch } from "../../Api/AxiosFetch";

export const submitCheckout = createAsyncThunk<
  string,
  CheckoutState,
  { rejectValue: string }
>("basketCheckout/submitCheckout", async (checkoutData, thunkAPI) => {
  try {
    const dto = {
      userName: checkoutData.userName,
      totalPrice: checkoutData.totalPrice,
      customerId: checkoutData.customerId,
      items: Object.values(checkoutData.items), // assuming items is an object

      // Prefer shipping address as required (you can adjust)
      firstName: checkoutData.shippingAddress.firstName,
      lastName: checkoutData.shippingAddress.lastName,
      emailAddress: checkoutData.shippingAddress.emailAddress,
      addressLine: checkoutData.shippingAddress.addressLine,
      country: checkoutData.shippingAddress.country,
      zipCode: checkoutData.shippingAddress.zipCode,
      city: checkoutData.shippingAddress.city,
      state: checkoutData.shippingAddress.state,

      cardName: checkoutData.payment.cardName ?? null,
      cardNumber: checkoutData.payment.cardNumber,
      expiration: checkoutData.payment.expiration,
      cvv: checkoutData.payment.cvv,
    };
    console.log(dto);

    const response = await axiosFetch.post("/basket/checkout", {
      CheckoutBasket: dto,
    });

    return response.data.orderId;
  } catch (error) {
    console.log(error);

    return thunkAPI.rejectWithValue(
      error instanceof Error ? error.message : "Unknown error"
    );
  }
});

export interface Address {
  firstName: string;
  lastName: string;
  emailAddress: string;
  addressLine: string;
  country: string;
  zipCode: string;
  city: string;
  state: string;
}

export interface PaymentInfo {
  cardName?: string;
  cardNumber: string;
  expiration: string; // ISO string like '2025-08-01'
  cvv: string;
}

export interface CheckoutState {
  userName: string;
  totalPrice: number | undefined;
  customerId?: string;
  items: ShoppingCartItem[];
  shippingAddress: Address;
  billingAddress: Address;
  payment: PaymentInfo;
  error: string;
  loading: boolean;
}

const initialState: CheckoutState = {
  userName: "",
  totalPrice: 0,
  error: "",
  loading: false,
  items: [], // Assuming this is an object. Adjust if it's an array.
  shippingAddress: {
    firstName: "",
    lastName: "",
    emailAddress: "",
    addressLine: "",
    country: "",
    zipCode: "",
    city: "",
    state: "",
  },
  billingAddress: {
    firstName: "",
    lastName: "",
    emailAddress: "",
    addressLine: "",
    country: "",
    zipCode: "",
    city: "",
    state: "",
  },
  payment: {
    cardNumber: "",
    expiration: "",
    cvv: "",
  },
};

const basketCheckoutSlice = createSlice({
  name: "basketCheckout",
  initialState,
  reducers: {
    setCustomerId(state, action: PayloadAction<{ customerId: string }>) {
      state.customerId = action.payload.customerId;
    },

    loadFromBasket(
      state,
      action: PayloadAction<{
        userName: string;
        totalPrice: number | undefined;
        items: ShoppingCartItem[];
      }>
    ) {
      state.userName = action.payload.userName;
      state.totalPrice = action.payload.totalPrice;
      state.items = action.payload.items;
    },

    setShippingAddress(state, action: PayloadAction<Address>) {
      state.shippingAddress = action.payload;
    },
    setBillingAddress(state, action: PayloadAction<Address>) {
      state.billingAddress = action.payload;
    },
    setPaymentInfo(state, action: PayloadAction<PaymentInfo>) {
      state.payment = action.payload;
    },
    clearCheckoutState(state) {
      Object.assign(state, initialState);
    },
  },

  extraReducers: (builder) => {
    builder
      .addCase(submitCheckout.pending, (state) => {
        state.loading = true;
      })
      .addCase(submitCheckout.fulfilled, (state) => {
        state.loading = false;
      })
      .addCase(submitCheckout.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export const {
  setCustomerId,
  loadFromBasket,
  setShippingAddress,
  setBillingAddress,
  setPaymentInfo,
  clearCheckoutState,
} = basketCheckoutSlice.actions;

export default basketCheckoutSlice.reducer;
