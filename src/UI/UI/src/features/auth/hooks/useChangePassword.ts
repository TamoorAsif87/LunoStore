import { useMutation } from "@tanstack/react-query";
import { changePassword } from "../../../Api/Auth";

import type { AxiosError } from "axios";
import { useNavigate } from "react-router-dom";
import type { ChangePasswordSchema } from "../schemas/changesPasswordSchema";
import toast from "react-hot-toast";

export function useChangePassword() {
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (data: ChangePasswordSchema) => changePassword(data),
    onSuccess: (data: { success: boolean; message: string }) => {
      // toast.success("Login successful!");
      toast.success(data.message);
      // Navigate to home page or dashboard
      navigate("/");
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      const message =
        error?.response?.data?.detail || "Change password failed.";
      // toast.error("Login failed!");
      toast.error(message);
    },
  });
}
