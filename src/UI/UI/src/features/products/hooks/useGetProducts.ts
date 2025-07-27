import { useQuery } from "@tanstack/react-query";
import { getAllProducts } from "../../../Api/Product";

export function useGetProducts() {
  return useQuery({
    queryKey: ["admin:products"],
    queryFn: getAllProducts,
  });
}
