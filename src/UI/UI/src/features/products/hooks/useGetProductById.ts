import { useQuery } from "@tanstack/react-query";
import { getProductById } from "../../../Api/Product";

export function useProductById(id: string) {
  return useQuery({
    queryKey: ["product", id],
    queryFn: () => getProductById(id),
    enabled: !!id, // prevent fetch when id is undefined
  });
}
