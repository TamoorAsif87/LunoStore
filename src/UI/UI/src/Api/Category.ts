import {
  categorySchema,
  getAllCategoriesSchema,
  type CategoryDto,
  type CreateCategoryDto,
  type GetAllCategoriesDto,
} from "../features/category/schemas/categorySchema";
import { authFetch, axiosFetch } from "./AxiosFetch";

export async function getAllCategories(): Promise<GetAllCategoriesDto> {
  const response = await axiosFetch.get<GetAllCategoriesDto>("/category");

  const parsedData = getAllCategoriesSchema.parse(response.data);
  return parsedData;
}

export async function createCategory(data: CreateCategoryDto): Promise<string> {
  const response = await authFetch.post("/category", data);
  return response.data;
}

export async function getCategoryById(id: string): Promise<CategoryDto> {
  const response = await authFetch.get(`/category/${id}`);
  return categorySchema.parse(response.data);
}

export async function updateCategory(
  id: string,
  category: CategoryDto
): Promise<void> {
  await authFetch.put(`/category/${id}`, category);
}

export async function deleteCategory(id: string): Promise<void> {
  await authFetch.delete(`/category/${id}`);
}
