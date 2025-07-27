import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateCategory } from "../../../Api/Category";
import type { AxiosError } from "axios";
import type { CategoryDto } from "../schemas/categorySchema";

export function useUpdateCategory() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      id,
      categoryDto,
    }: {
      id: string;
      categoryDto: CategoryDto;
    }) => updateCategory(id, categoryDto),
    onSuccess: () => {
      toast.success("Category updated successfully!");
      queryClient.invalidateQueries({ queryKey: ["categories"] });
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      toast.error(error.response?.data.detail || "Failed to update category");
    },
  });
}
