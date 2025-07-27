import { useQuery } from "@tanstack/react-query";
import { getCategoryById } from "../../../Api/Category";

export function useCategoryById(id: string) {
  return useQuery({
    queryKey: ["category", id],
    queryFn: () => getCategoryById(id),
    enabled: !!id,
  });
}
