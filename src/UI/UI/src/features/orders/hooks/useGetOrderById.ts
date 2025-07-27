import { useQuery } from "@tanstack/react-query";
import { getOrderById } from "../../../Api/Order";
import type { OrderDto } from "../schemas/orderSchema";

export function useGetOrderById(orderId: string) {
  return useQuery<OrderDto>({
    queryKey: ["admin:order", orderId],
    queryFn: () => getOrderById(orderId),
    enabled: !!orderId, // only runs when orderId is truthy
  });
}
