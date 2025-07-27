import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  loginSchema,
  type LoginFormValues,
} from "../features/auth/schemas/loginSchema";
import FormInput from "../components/FormInput";
import { Link } from "react-router-dom";
import CheckboxInput from "../components/CheckboxInput";
import { useLogin } from "../features/auth/hooks/useLogin";
import { useState } from "react";
import { type AxiosError } from "axios";
import { useTitle } from "../hooks/useTitle";

// import { useNavigate } from "react-router-dom";

const Login = () => {
  useTitle("Login");
  const [serverError, setServerError] = useState<string | null>(null);

  const methods = useForm<LoginFormValues>({
    resolver: zodResolver(loginSchema),
  });

  // const navigate = useNavigate();
  const { mutate: login, isPending: isLoading } = useLogin();

  const onSubmit = async (data: LoginFormValues) => {
    login(data, {
      onError: (error: AxiosError<{ detail: string }>) => {
        const message = error?.response?.data?.detail || "Login failed.";
        setServerError(message);
      },
    });
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 border rounded bg-base-100 shadow">
      <h2 className="text-2xl font-bold mb-6 text-center">Login</h2>
      {serverError && (
        <div className="text-red-600 font-medium text-center mb-4">
          {serverError}
        </div>
      )}

      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <FormInput name="email" label="Email" type="email" />
          <FormInput name="password" label="Password" type="password" />
          <CheckboxInput name="rememberMe" label="RememberMe" />

          <button
            disabled={isLoading}
            className="btn btn-primary w-full mt-4"
            type="submit"
          >
            {isLoading ? "loading..." : "Login"}
          </button>

          <div className="flex justify-between items-center text-sm mt-3">
            <Link
              to="/auth/forgot-password"
              className="link text-blue-500 hover:underline"
            >
              Forgot Password?
            </Link>
            <Link
              to="/auth/register"
              className="link text-blue-500 hover:underline"
            >
              Register
            </Link>
          </div>
        </form>
      </FormProvider>
    </div>
  );
};

export default Login;
