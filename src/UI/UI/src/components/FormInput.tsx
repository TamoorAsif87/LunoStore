import { type InputHTMLAttributes } from "react";
import { useFormContext } from "react-hook-form";
// import { type FieldErrors, type FieldValues } from "react-hook-form";

interface FormInputProps extends InputHTMLAttributes<HTMLInputElement> {
  name: string;
  label: string;
  // errors: FieldErrors<FieldValues>;
}

const FormInput = ({ name, label, type, ...rest }: FormInputProps) => {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  const inputProps = register(
    name,
    type === "number" ? { valueAsNumber: true } : {}
  );

  return (
    <div className="form-control w-full mb-4">
      {type !== "hidden" && (
        <label className="label">
          <span className="label-text">{label}</span>
        </label>
      )}
      <input
        type={type}
        {...inputProps}
        {...rest}
        className="input input-bordered w-full"
      />
      {errors[name] && (
        <p className="text-error text-sm mt-1">
          {String(errors[name]?.message)}
        </p>
      )}
    </div>
  );
};

export default FormInput;
