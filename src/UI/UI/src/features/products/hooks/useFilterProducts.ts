import { useQuery } from "@tanstack/react-query";
import { getAllFilterProducts } from "../../../Api/Product";
import type { ProductSpecs } from "../schemas/productSchema";

export function useFilterProducts(filters: ProductSpecs) {
  return useQuery({
    queryKey: ["filterproducts", filters],
    queryFn: () => getAllFilterProducts(filters),
    staleTime: 60 * 1000,
  });
}
