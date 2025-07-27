import {
  paginatedProductsSchema,
  productsDtoSchema,
  type PaginatedProducts,
  type ProductDto,
  type ProductSpecs,
} from "../features/products/schemas/productSchema";
import { authFetch, axiosFetch } from "./AxiosFetch";

export async function getAllProducts(): Promise<ProductDto[]> {
  const response = await axiosFetch.get("/product");

  const parsedData = productsDtoSchema.safeParse(response.data);
  console.log(parsedData.error?.message);

  return parsedData.data as ProductDto[];
}

export async function getAllFilterProducts(
  data: ProductSpecs
): Promise<PaginatedProducts> {
  const response = await axiosFetch.get("/product/filter", {
    params: data,
  });

  const parsed = paginatedProductsSchema.parse(response.data);
  return parsed;
}

export async function createProduct(data: FormData): Promise<string> {
  const response = await authFetch.post("/product", data, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data;
}

export async function getProductById(id: string): Promise<ProductDto> {
  const response = await axiosFetch.get(`/product/${id}`);
  return response.data;
}

export async function updateProduct(id: string, data: FormData): Promise<void> {
  const response = await authFetch.put(`/product/${id}`, data, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data;
}

export async function deleteProduct(id: string): Promise<void> {
  const response = await authFetch.delete(`/product/${id}`);
  return response.data;
}
