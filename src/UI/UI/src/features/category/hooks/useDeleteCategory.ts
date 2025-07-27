import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteCategory } from "../../../Api/Category";
import toast from "react-hot-toast";
import type { AxiosError } from "axios";

export function useDeleteCategory() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteCategory,
    onSuccess: () => {
      toast.success("Category deleted successfully!");
      queryClient.invalidateQueries({ queryKey: ["categories"] });
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      toast.error(error.response?.data.detail || "Failed to delete category");
    },
  });
}
