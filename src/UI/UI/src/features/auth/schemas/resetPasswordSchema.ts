import { z } from "zod";

export const resetPasswordSchema = z.object({
  email: z.email("Invalid email address"),
  token: z.string(),
  newPassword: z.string().min(6, "Password must be at least 6 characters"),
});

export type ResetPasswordFormValues = z.infer<typeof resetPasswordSchema>;
