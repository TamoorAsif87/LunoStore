import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteOrder } from "../../../Api/Order";
import toast from "react-hot-toast";
import type { AxiosError } from "axios";

export function useDeleteOrder() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: string) => deleteOrder(id),
    onSuccess: () => {
      toast.success("Order deleted successfully");
      // Invalidate the orders list to refresh it
      queryClient.invalidateQueries({ queryKey: ["admin:orders"] });
    },
    onError: (error: AxiosError<{ detail?: string }>) => {
      console.error("Delete order error:", error);
      toast.error("Failed to delete order");
    },
  });
}
