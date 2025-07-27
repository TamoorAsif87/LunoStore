import { useQuery } from "@tanstack/react-query";
import { getAllCategories } from "../../../Api/Category";

export function useCategories() {
  return useQuery({
    queryKey: ["categories"],
    queryFn: getAllCategories,
  });
}
