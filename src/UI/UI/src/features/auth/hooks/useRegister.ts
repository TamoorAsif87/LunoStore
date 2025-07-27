import { useMutation } from "@tanstack/react-query";
import { registerUser } from "../../../Api/Auth";
import {
  addRefreshToken,
  type ApiUserResponse,
} from "../../../Utils/localStorage";
import type { AxiosError } from "axios";
import { useNavigate } from "react-router-dom";
import { addToken, addUser } from "../../../Utils/localStorage";
import type { RegisterFormValues } from "../schemas/registerSchema";
import toast from "react-hot-toast";

export function useRegister() {
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (data: RegisterFormValues) => registerUser(data),
    onSuccess: (data: ApiUserResponse) => {
      // Add token to local storage
      addToken(data.accessToken);
      addRefreshToken(data.refreshToken);
      if (data.user) {
        addUser({ ...data.user, role: data.role });
      } else {
        addUser(data.user);
      }

      // toast.success("Login successful!");
      toast.success("Registration successful!");
      // Navigate to home page or dashboard
      navigate("/");
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      const message = error?.response?.data?.detail || "Registration failed.";
      // toast.error("Login failed!");
      toast.error(message);
    },
  });
}
