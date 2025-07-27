import { axiosFetch, authFetch } from "./AxiosFetch";
import { type LoginFormValues } from "../features/auth/schemas/loginSchema";
import {
  type ApiUserResponse,
  type RefreshToken,
  type UserDto,
} from "../Utils/localStorage";
import type { RegisterFormValues } from "../features/auth/schemas/registerSchema";
import type { ChangePasswordSchema } from "../features/auth/schemas/changesPasswordSchema";
import type { ForgotPasswordFormValues } from "../features/auth/schemas/forgotPasswordSchema";
import type { ResetPasswordFormValues } from "../features/auth/schemas/resetPasswordSchema";
import {
  userProfileSchema,
  type UserProfile,
} from "../features/auth/schemas/userProfileSchema";

// Register user
export const registerUser = async (
  data: RegisterFormValues
): Promise<ApiUserResponse> => {
  const response = await axiosFetch.post("/account/register", {
    Register: data,
  });
  return response.data;
};

// Login user
export const loginUser = async (
  data: LoginFormValues
): Promise<ApiUserResponse> => {
  const response = await axiosFetch.post("/account/login", { Login: data });
  return response.data;
};
// Forgot password (generates reset token)
export const forgotPassword = async (
  data: ForgotPasswordFormValues
): Promise<{ message: string; token: string }> => {
  const response = await axiosFetch.post("/account/forgot-password", {
    Email: data.email,
  });
  return response.data;
};

// Reset password
export const resetPassword = async (
  data: ResetPasswordFormValues
): Promise<{ success: boolean; message: string }> => {
  const response = await axiosFetch.post("/account/reset-password", {
    ResetPasswordDto: data,
  });
  return response.data;
};

// ChangePassword

export const changePassword = async (
  data: ChangePasswordSchema
): Promise<{ success: boolean; message: string }> => {
  const response = await authFetch.post("/account/change-password", {
    ChangePassword: data,
  });
  return response.data;
};

//profile
export const getProfile = async (): Promise<UserProfile> => {
  const response = await authFetch.get("/account/profile");
  const parsed = userProfileSchema.parse(response.data); // Validate with Zod
  return parsed;
};

// Update profile
export const updateProfile = async (data: FormData): Promise<UserProfile> => {
  const response = await authFetch.post("/account/update-profile", data, {
    headers: {
      // Let Axios set the correct multipart/form-data boundary
      "Content-Type": "multipart/form-data",
    },
  });

  const parsed = userProfileSchema.parse(response.data);
  return parsed;
};

export const refreshToken = async (
  data: RefreshToken
): Promise<ApiUserResponse> => {
  console.log(data);

  const response = await axiosFetch.post("/account/refresh-token", {
    RefreshToken: data,
  });
  return response.data;
};

export const getUsers = async (): Promise<UserDto[]> => {
  try {
    const response = await authFetch.get("/account/allUsers");
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};
