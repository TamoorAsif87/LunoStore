import { useQuery } from "@tanstack/react-query";
import { getOrdersByCustomerId } from "../../../Api/Order";

export function useQueryOrdersByCustomerId(customerId: string) {
  return useQuery({
    queryKey: ["orders", customerId],
    queryFn: () => getOrdersByCustomerId(customerId),
    enabled: !!customerId, // Only run if customerId is defined
  });
}
