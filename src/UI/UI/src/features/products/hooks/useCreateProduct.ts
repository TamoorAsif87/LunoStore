import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { createProduct } from "../../../Api/Product";
import type { AxiosError } from "axios";

export function useCreateProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (formData: FormData) => createProduct(formData),
    onSuccess: () => {
      toast.success("Product created successfully!");
      queryClient.invalidateQueries({ queryKey: ["admin:products"] });
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      toast.error(error.response?.data.detail || "Failed to create product");
    },
  });
}
