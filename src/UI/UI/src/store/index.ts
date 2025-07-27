import { configureStore } from "@reduxjs/toolkit";
import basketReducer from "../features/basket/basket";
import basketCheckoutReducer from "../features/basket/basketCheckout";

export const store = configureStore({
  reducer: {
    basket: basketReducer,
    basketCheckout: basketCheckoutReducer,
  },
});

// Types for RootState and AppDispatch
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
