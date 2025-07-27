import { z } from "zod";

export const changePasswordSchema = z.object({
  email: z.email("Invalid email address"),
  currentPassword: z.string().min(6, "Password must be at least 6 characters"),
  newPassword: z.string().min(6, "Password must be at least 6 characters"),
});

export type ChangePasswordSchema = z.infer<typeof changePasswordSchema>;
