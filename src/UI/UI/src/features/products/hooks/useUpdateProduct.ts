import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateProduct } from "../../../Api/Product";
import type { AxiosError } from "axios";

export function useUpdateProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, formData }: { id: string; formData: FormData }) =>
      updateProduct(id, formData),
    onSuccess: () => {
      toast.success("Product updated successfully!");
      queryClient.invalidateQueries({ queryKey: ["admin:products"] });
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      console.log(error);

      toast.error(error.response?.data.detail || "Failed to update product");
    },
  });
}
