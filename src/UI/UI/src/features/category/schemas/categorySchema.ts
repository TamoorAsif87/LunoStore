import { z } from "zod";

export const categorySchema = z.object({
  id: z.string(),
  name: z
    .string()
    .nonempty("Name is required")
    .max(100, "Name must be at most 100 characters long"),
});

export const createCategorySchema = z.object({
  name: z
    .string()
    .nonempty("Name is required")
    .max(100, "Name must be at most 100 characters long"),
});

export const getAllCategoriesSchema = z.array(categorySchema);

export type CategoryDto = z.infer<typeof categorySchema>;
export type GetAllCategoriesDto = z.infer<typeof getAllCategoriesSchema>;
export type CreateCategoryDto = z.infer<typeof createCategorySchema>;
