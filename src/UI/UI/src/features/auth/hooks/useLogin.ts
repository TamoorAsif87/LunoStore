import { useMutation } from "@tanstack/react-query";
import { loginUser } from "../../../Api/Auth";
import { type LoginFormValues } from "../schemas/loginSchema";
import {
  addRefreshToken,
  type ApiUserResponse,
} from "../../../Utils/localStorage";
import type { AxiosError } from "axios";
import { useLocation, useNavigate } from "react-router-dom";
import { addToken, addUser } from "../../../Utils/localStorage";
import toast from "react-hot-toast";

export function useLogin() {
  const navigate = useNavigate();
  const location = useLocation();

  const path = location.state?.from?.pathname || "/";

  return useMutation({
    mutationFn: (data: LoginFormValues) => loginUser(data),
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
      toast.success("Login successful!");

      // Navigate to home page or dashboard
      navigate(path, { replace: true });
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      console.log(error);
      const message = error?.response?.data?.detail || "Login failed.";
      // Example: show error toast
      toast.error(message);
      // toast.error("Login failed!");
    },
  });
}
