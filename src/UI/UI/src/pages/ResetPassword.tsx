import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  resetPasswordSchema,
  type ResetPasswordFormValues,
} from "../features/auth/schemas/resetPasswordSchema";
import FormInput from "../components/FormInput";
import { useResetPassword } from "../features/auth/hooks/useResetPassword";
import { useState } from "react";
import { type AxiosError } from "axios";
import { getPasswordResetToken } from "../Utils/localStorage";
import { useTitle } from "../hooks/useTitle";

const ResetPassword = () => {
  useTitle("reset-password | LunoStore");
  const [serverError, setServerError] = useState<string | null>(null);
  const resetPasswordToken = getPasswordResetToken();

  const methods = useForm<ResetPasswordFormValues>({
    resolver: zodResolver(resetPasswordSchema),
  });

  const { mutate: resetPassword, isPending } = useResetPassword();

  const onSubmit = (data: ResetPasswordFormValues) => {
    resetPassword(data, {
      onError: (error: AxiosError<{ detail: string }>) => {
        const message = error?.response?.data?.detail || "Reset failed.";
        setServerError(message);
      },
    });
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 border rounded bg-base-100 shadow">
      <h2 className="text-2xl font-bold mb-6 text-center">Reset Password</h2>

      {serverError && (
        <div className="text-red-600 font-medium text-center mb-4">
          {serverError}
        </div>
      )}

      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <FormInput name="email" label="Email" type="email" />
          <FormInput
            name="token"
            label="Token"
            type="hidden"
            value={`${resetPasswordToken}`}
          />
          <FormInput name="newPassword" label="New Password" type="password" />

          <button
            disabled={isPending}
            className="btn btn-primary w-full mt-4"
            type="submit"
          >
            {isPending ? "Resetting..." : "Reset Password"}
          </button>
        </form>
      </FormProvider>
    </div>
  );
};

export default ResetPassword;
