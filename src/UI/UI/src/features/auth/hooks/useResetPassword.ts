import { useMutation } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import type { AxiosError } from "axios";
import type { ResetPasswordFormValues } from "../schemas/resetPasswordSchema";
import { resetPassword } from "../../../Api/Auth";
import { removePasswordResetToken } from "../../../Utils/localStorage";

export function useResetPassword() {
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (data: ResetPasswordFormValues) => resetPassword(data),
    onSuccess: (res: { success: boolean; message: string }) => {
      removePasswordResetToken();
      toast.success(res.message);
      navigate("/auth/login");
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      const message = error.response?.data?.detail || "Reset password failed";
      toast.error(message);
    },
  });
}
