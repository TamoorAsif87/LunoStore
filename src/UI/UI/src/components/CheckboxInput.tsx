import { useFormContext } from "react-hook-form";

interface CheckboxInputProps {
  name: string;
  label: string;
}

const CheckboxInput = ({ name, label }: CheckboxInputProps) => {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <div className="form-control">
      <label className="label cursor-pointer justify-start gap-2">
        <input
          type="checkbox"
          {...register(name)}
          className="checkbox checkbox-primary"
        />
        <span className="label-text">{label}</span>
      </label>
      {errors[name] && (
        <p className="text-error text-sm mt-1">
          {(errors[name]?.message as string) ?? ""}
        </p>
      )}
    </div>
  );
};

export default CheckboxInput;
