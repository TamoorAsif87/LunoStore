import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteProduct } from "../../../Api/Product";
import type { AxiosError } from "axios";

export function useDeleteProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: string) => deleteProduct(id),
    onSuccess: () => {
      toast.success("Product deleted successfully");
      queryClient.invalidateQueries({ queryKey: ["admin:products"] });
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      toast.error(error.response?.data.detail || "Failed to delete product");
    },
  });
}
