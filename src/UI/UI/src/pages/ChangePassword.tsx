import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import FormInput from "../components/FormInput";
import { useState } from "react";
import { type AxiosError } from "axios";
import {
  changePasswordSchema,
  type ChangePasswordSchema,
} from "../features/auth/schemas/changesPasswordSchema";
import { useChangePassword } from "../features/auth/hooks/useChangePassword";
import { useTitle } from "../hooks/useTitle";
import { useAuth } from "../features/auth/hooks/useAuth";

const ChangePassword = () => {
  useTitle("change-password | LunoStore");
  const { user } = useAuth();
  const email = user?.email || "";

  const [serverError, setServerError] = useState<string | null>(null);

  const methods = useForm<ChangePasswordSchema>({
    resolver: zodResolver(changePasswordSchema),
  });

  // const navigate = useNavigate();
  const { mutate: changePassword, isPending: isLoading } = useChangePassword();

  const onSubmit = async (data: ChangePasswordSchema) => {
    changePassword(data, {
      onError: (error: AxiosError<{ detail: string }>) => {
        const message = error?.response?.data?.detail || "Login failed.";
        console.log(error);

        setServerError(message);
      },
    });
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 border rounded bg-base-100 shadow">
      <h2 className="text-2xl font-bold mb-6 text-center">Change Password</h2>
      {serverError && (
        <div className="text-red-600 font-medium text-center mb-4">
          {serverError}
        </div>
      )}

      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <FormInput name="email" label="Email" type="email" value={email} />
          <FormInput
            name="currentPassword"
            label="Current Password"
            type="password"
          />
          <FormInput name="newPassword" label="New Password" type="password" />

          <button
            disabled={isLoading}
            className="btn btn-primary w-full mt-4"
            type="submit"
          >
            {isLoading ? "loading..." : "Change Password"}
          </button>
        </form>
      </FormProvider>
    </div>
  );
};

export default ChangePassword;
