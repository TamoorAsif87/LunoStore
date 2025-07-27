import { useQuery } from "@tanstack/react-query";
import { getAllOrders } from "../../../Api/Order";
import type { OrderDto } from "../schemas/orderSchema";

export function useGetAllOrders() {
  return useQuery<OrderDto[]>({
    queryKey: ["admin:orders"],
    queryFn: getAllOrders,
  });
}
