import { z } from "zod";

export const updateProfileSchema = z.object({
  name: z.string().min(1, "Name is required"),
  address: z.string().optional(),
  phone: z.string().optional(),
  city: z.string().optional(),
  country: z.string().optional(),
  profileImageFile: z.any().optional(),
});

export type UpdateProfileSchema = z.infer<typeof updateProfileSchema>;
