import { useMutation } from "@tanstack/react-query";
import { forgotPassword } from "../../../Api/Auth";
import { useNavigate } from "react-router-dom";
import type { ForgotPasswordFormValues } from "../schemas/forgotPasswordSchema";
import type { AxiosError } from "axios";
import toast from "react-hot-toast";
import { addPasswordResetToken } from "../../../Utils/localStorage";

export function useForgotPassword() {
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (data: ForgotPasswordFormValues) => forgotPassword(data),
    onSuccess: (data: { message: string; token: string }) => {
      toast.success(data.message);
      addPasswordResetToken(data.token);
      navigate("/auth/reset-password");
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      const message =
        error?.response?.data?.detail || "Failed to send reset instructions.";
      toast.error(message);
    },
  });
}
