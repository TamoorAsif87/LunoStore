import { z } from "zod";

export const registerSchema = z
  .object({
    name: z.string().max(100, "Name must be at most 100 characters long"),
    email: z.email("Please provide valid email"),
    password: z.string().min(6, "Password must be at least 6 characters long"),
    confirmPassword: z.string(),
  })
  .refine((data) => data.password === data.confirmPassword, {
    error: "Passwords do not match",
    path: ["confirmPassword"],
  });

export type RegisterFormValues = z.infer<typeof registerSchema>;
