import {
  createAsyncThunk,
  createSlice,
  // type PayloadAction,
} from "@reduxjs/toolkit";
import { getBasketUserName } from "../../Utils/localStorage";
import basketService from "./basketService";
import type { AxiosError } from "axios";
import toast from "react-hot-toast";

export const createBasket = createAsyncThunk<
  string,
  ShoppingCart,
  { rejectValue: string }
>("basket/create", async (basketData, { rejectWithValue, dispatch }) => {
  try {
    const id = await basketService.createBasket(basketData);

    await dispatch(getBasket(basketData.userName));

    return id;
  } catch (err) {
    const error = err as AxiosError<{ detail?: string }>;
    const message = error.response?.data?.detail || "Failed to create basket";
    return rejectWithValue(message);
  }
});

export const addItem = createAsyncThunk<
  ShoppingCart,
  { userName: string; item: ShoppingCartItem },
  { rejectValue: string }
>("cart/addItem", async ({ userName, item }, { rejectWithValue, dispatch }) => {
  try {
    const shoppingCart = await basketService.addItem(userName, item);

    await dispatch(getBasket(userName));

    return shoppingCart;
  } catch (err) {
    const error = err as AxiosError<{ detail?: string }>;
    const message = error.response?.data?.detail || "Failed to create basket";
    return rejectWithValue(message);
  }
});

export const getBasket = createAsyncThunk<
  ShoppingCart,
  string,
  { rejectValue: string }
>("basket/get", async (userName, { rejectWithValue }) => {
  try {
    return await basketService.getBasket(userName);
  } catch (err) {
    const error = err as AxiosError<{ detail?: string }>;
    const message = error.response?.data?.detail || "Failed to fetch basket";
    return rejectWithValue(message);
  }
});

export const removeItem = createAsyncThunk<
  ShoppingCart,
  { userName: string; productId: string },
  { rejectValue: string }
>(
  "cart/removeItem",
  async ({ userName, productId }, { rejectWithValue, dispatch }) => {
    try {
      const shoppingCart = await basketService.removeItem(userName, productId);
      await dispatch(getBasket(userName));
      return shoppingCart;
    } catch (err) {
      const error = err as AxiosError<{ detail?: string }>;
      const message = error.response?.data?.detail || "Failed to remove item";
      return rejectWithValue(message);
    }
  }
);

// Thunk to increase item quantity
export const increaseItemQuantity = createAsyncThunk<
  ShoppingCart,
  { userName: string; productId: string; quantity: number },
  { rejectValue: string }
>(
  "basket/increaseItemQuantity",
  async ({ userName, productId, quantity }, thunkAPI) => {
    try {
      const res = await basketService.increaseItemQuantity(
        userName,
        productId,
        quantity
      );

      await thunkAPI.dispatch(getBasket(userName));

      return res;
    } catch (err) {
      const error = err as AxiosError<{ detail?: string }>;
      const message = error.response?.data?.detail || "Failed to remove item";
      return thunkAPI.rejectWithValue(message);
    }
  }
);

// Thunk to decrease item quantity
export const decreaseItemQuantity = createAsyncThunk<
  ShoppingCart,
  { userName: string; productId: string; quantity: number },
  { rejectValue: string }
>(
  "basket/decreaseItemQuantity",
  async ({ userName, productId, quantity }, thunkAPI) => {
    try {
      const res = await basketService.decreaseItemQuantity(
        userName,
        productId,
        quantity
      );

      await thunkAPI.dispatch(getBasket(userName));

      return res;
    } catch (err) {
      const error = err as AxiosError<{ detail?: string }>;
      const message = error.response?.data?.detail || "Failed to remove item";
      return thunkAPI.rejectWithValue(message);
    }
  }
);

export interface ShoppingCartItem {
  id?: string;
  productId: string;
  shoppingCartId?: string;
  quantity: number;
  price: number;
  productName: string;
  productColors: string[];
  productImages: string[];
}

export interface ShoppingCart {
  id?: string;
  userName: string;
  items: ShoppingCartItem[];
  totalPrice?: number;
  totalItems?: number;
}

export interface IBasketService {
  getBasket(userName: string): Promise<ShoppingCart>;
  addItem(userName: string, item: ShoppingCartItem): Promise<ShoppingCart>;
  removeItem(userName: string, productId: string): Promise<ShoppingCart>;
  increaseItemQuantity(
    userName: string,
    productId: string,
    quantity: number
  ): Promise<ShoppingCart>;
  decreaseItemQuantity(
    userName: string,
    productId: string,
    quantity: number
  ): Promise<ShoppingCart>;
  deleteBasket(userName: string): Promise<void>;

  createBasket(data: ShoppingCart): Promise<string>;
}

export interface BasketState {
  cart: ShoppingCart;
  loading: boolean;
  error: string | null;
}

const initialState: BasketState = {
  cart: {
    userName: getBasketUserName(),
    items: [],
    totalItems: 0,
    totalPrice: 0,
  },
  loading: false,
  error: null,
};

const basketSlice = createSlice({
  name: "basket",
  initialState,
  reducers: {
    clearBasket: (state) => {
      state.cart = { userName: "", items: [], totalItems: 0, totalPrice: 0 };
      state.loading = false;
    },
  },

  extraReducers: (builder) => {
    builder
      .addCase(createBasket.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(createBasket.fulfilled, (state, action) => {
        state.loading = false;
        state.error = null;
        state.cart.id = action.payload;
        toast.success(`Item  is added to basket`);
      })
      .addCase(createBasket.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "Failed to create basket";
        toast.error(`Failed to add item to basket`);
      })

      .addCase(getBasket.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(getBasket.fulfilled, (state, action) => {
        state.loading = false;
        state.cart.id = action.payload.id;
        state.cart.items = action.payload.items;

        state.cart.totalPrice = action.payload.items.reduce(
          (acc, item) => acc + item.quantity * item.price,
          0
        );
        state.cart.userName = action.payload.userName;
        state.cart.totalItems = action.payload.items.length;
      })
      .addCase(getBasket.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "Something went wrong";
      })

      .addCase(addItem.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(addItem.fulfilled, (state, action) => {
        state.loading = false;
        state.cart.items = action.payload.items;
        toast.success(`Item  is added to basket`);
      })
      .addCase(addItem.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "Something went wrong";
      })

      .addCase(increaseItemQuantity.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(increaseItemQuantity.fulfilled, (state) => {
        state.loading = false;
        toast.success(`Item quantity is increased`);
      })
      .addCase(increaseItemQuantity.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "Something went wrong";
      })
      .addCase(decreaseItemQuantity.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(decreaseItemQuantity.fulfilled, (state) => {
        state.loading = false;
        toast.success(`Item quantity is decreased`);
      })
      .addCase(decreaseItemQuantity.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "Something went wrong";
      });
  },
});

export const { clearBasket } = basketSlice.actions;

export default basketSlice.reducer;
