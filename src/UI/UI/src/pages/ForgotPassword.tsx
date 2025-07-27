import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  forgotPasswordSchema,
  type ForgotPasswordFormValues,
} from "../features/auth/schemas/forgotPasswordSchema";
import FormInput from "../components/FormInput";
import { useForgotPassword } from "../features/auth/hooks/useForgotPassword";
import { useState } from "react";
import { type AxiosError } from "axios";
import { Link } from "react-router-dom";

const ForgotPassword = () => {
  const [serverError, setServerError] = useState<string | null>(null);

  const methods = useForm<ForgotPasswordFormValues>({
    resolver: zodResolver(forgotPasswordSchema),
  });

  const { mutate: forgotPassword, isPending: isLoading } = useForgotPassword();

  const onSubmit = (data: ForgotPasswordFormValues) => {
    forgotPassword(data, {
      onError: (error: AxiosError<{ detail: string }>) => {
        const message = error?.response?.data?.detail || "Request failed.";
        setServerError(message);
      },
    });
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 border rounded bg-base-100 shadow">
      <h2 className="text-2xl font-bold mb-6 text-center">Forgot Password</h2>

      {serverError && (
        <div className="text-red-600 font-medium text-center mb-4">
          {serverError}
        </div>
      )}

      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <FormInput name="email" label="Email" type="email" />

          <button
            disabled={isLoading}
            className="btn btn-primary w-full mt-4"
            type="submit"
          >
            {isLoading ? "Sending..." : "Send Reset Link"}
          </button>

          <div className="text-center text-sm mt-4">
            <Link
              to="/auth/login"
              className="link text-blue-500 hover:underline"
            >
              Back to Login
            </Link>
          </div>
        </form>
      </FormProvider>
    </div>
  );
};

export default ForgotPassword;
