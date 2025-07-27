import { z, ZodType } from "zod";
import { categorySchema } from "../../category/schemas/categorySchema";

export const productDtoSchema = z.object({
  id: z.string(),
  productName: z.string().nonempty(),
  description: z.string(),
  price: z.number(),
  inStock: z.number().int(),
  productImages: z.array(z.string()).optional(),
  productColors: z.array(z.string()),
  categoryId: z.string(),
  category: categorySchema.optional().nullable(),
});

export const productCreateDtoSchema = z.object({
  productName: z.string().nonempty(),
  description: z.string(),
  price: z.number(),
  inStock: z.number().int(),
  productImages: z.array(z.string()).optional(),
  productColors: z.array(z.string()),
  categoryId: z.string(),
});

export const productSpecsSchema = z
  .object({
    categoryId: z.string().optional(),
    searchProduct: z.string().optional(),
    priceStart: z.number().min(0).optional().default(0),
    priceEnd: z.number().min(0).optional().default(0),
    sortBy: z.string().optional(),
    page: z.number().int().min(1).max(20).optional().default(1),
  })
  .refine(
    (data) => data.priceEnd === undefined || data.priceStart <= data.priceEnd,
    {
      message: "priceEnd must be greater than priceStart",
      path: ["priceEnd"] as (keyof z.infer<typeof productSpecsSchema>)[],
    }
  );

export const paginatedResultSchema = <T extends ZodType>(itemSchema: T) =>
  z.object({
    items: z.array(itemSchema),
    totalCount: z.number(),
    page: z.number(),
    totalPages: z.number(),
  });

export const paginatedProductsSchema = paginatedResultSchema(productDtoSchema);

export const productsDtoSchema = z.array(productDtoSchema);

export type ProductDto = z.infer<typeof productDtoSchema>;
export type ProductsDto = z.infer<typeof productsDtoSchema>;
export type ProductCreateDto = z.infer<typeof productCreateDtoSchema>;
export type ProductSpecs = z.infer<typeof productSpecsSchema>;

export type PaginatedProducts = z.infer<typeof paginatedProductsSchema>;
