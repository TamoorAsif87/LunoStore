import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import FormInput from "../components/FormInput";
import { Link } from "react-router-dom";
import { useState } from "react";
import { type AxiosError } from "axios";
import {
  type RegisterFormValues,
  registerSchema,
} from "../features/auth/schemas/registerSchema";
import { useRegister } from "../features/auth/hooks/useRegister";

const Register = () => {
  const [serverError, setServerError] = useState<string | null>(null);

  const methods = useForm<RegisterFormValues>({
    resolver: zodResolver(registerSchema),
  });

  // const navigate = useNavigate();
  const { mutate: register, isPending: isLoading } = useRegister();

  const onSubmit = async (data: RegisterFormValues) => {
    register(data, {
      onError: (error: AxiosError<{ detail: string }>) => {
        const message = error?.response?.data?.detail || "Login failed.";
        setServerError(message);
      },
    });
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 border rounded bg-base-100 shadow">
      <h2 className="text-2xl font-bold mb-6 text-center">Register</h2>
      {serverError && (
        <div className="text-red-600 font-medium text-center mb-4">
          {serverError}
        </div>
      )}

      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <FormInput name="name" label="Name" type="text" />
          <FormInput name="email" label="Email" type="email" />
          <FormInput name="password" label="Password" type="password" />
          <FormInput
            name="confirmPassword"
            label="Confirm Password"
            type="password"
          />

          <button
            disabled={isLoading}
            className="btn btn-primary w-full mt-4"
            type="submit"
          >
            {isLoading ? "loading..." : "Register"}
          </button>

          <div className="flex justify-between items-center text-sm mt-3">
            <Link
              to="/auth/login"
              className="link text-blue-500 hover:underline"
            >
              Login
            </Link>
          </div>
        </form>
      </FormProvider>
    </div>
  );
};

export default Register;
