import { AxiosError } from "axios";
import { axiosFetch } from "../../Api/AxiosFetch";
import type { IBasketService, ShoppingCart, ShoppingCartItem } from "./basket";

class BasketService implements IBasketService {
  async createBasket(data: ShoppingCart): Promise<string> {
    try {
      const response = await axiosFetch.post<string>("/basket", {
        ShoppingCart: data,
      });
      return response.data;
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        console.error(error);
        throw error;
      } else {
        console.error("Unknown error", error);
        throw error;
      }
    }
  }

  async getBasket(userName: string): Promise<ShoppingCart> {
    const response = await axiosFetch.get<ShoppingCart>(`/basket/${userName}`);
    return response.data;
  }

  async addItem(
    userName: string,
    item: ShoppingCartItem
  ): Promise<ShoppingCart> {
    try {
      const response = await axiosFetch.post<ShoppingCart>(
        `/basket/${userName}/items`,
        {
          userName,
          item,
        }
      );
      return response.data;
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        console.error(error);
        throw error;
      } else {
        console.error("Unknown error", error);
        throw error;
      }
    }
  }

  async removeItem(userName: string, productId: string): Promise<ShoppingCart> {
    try {
      const response = await axiosFetch.delete<ShoppingCart>(
        `/basket/${userName}/items/${productId}`
      );
      return response.data;
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        console.error(error);
        throw error;
      } else {
        console.error("Unknown error", error);
        throw error;
      }
    }
  }

  async increaseItemQuantity(
    userName: string,
    productId: string,
    quantity: number
  ): Promise<ShoppingCart> {
    try {
      const response = await axiosFetch.put<ShoppingCart>(
        `/basket/${userName}/items/${productId}/increase?quantity=${quantity}`
      );
      return response.data;
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        console.error(error);
        throw error;
      } else {
        console.error("Unknown error", error);
        throw error;
      }
    }
  }

  async decreaseItemQuantity(
    userName: string,
    productId: string,
    quantity: number
  ): Promise<ShoppingCart> {
    try {
      const response = await axiosFetch.put<ShoppingCart>(
        `/basket/${userName}/items/${productId}/decrease?quantity=${quantity}`,
        { quantity }
      );
      return response.data;
    } catch (error) {
      if (error instanceof AxiosError) {
        console.error(error);
        throw error;
      } else {
        console.error("Unknown error", error);
        throw error;
      }
    }
  }

  async deleteBasket(userName: string): Promise<void> {
    await axiosFetch.delete(`/basket/${userName}`);
  }
}

export default new BasketService();
