import { z } from "zod";

export const userProfileSchema = z.object({
  name: z.string(),
  userName: z.string(),
  email: z.email(),
  address: z.string().nullable(),
  phone: z.string().nullable(),
  city: z.string().nullable(),
  country: z.string().nullable(),
  profileImage: z.string().nullable(),
});

export type UserProfile = z.infer<typeof userProfileSchema>;
