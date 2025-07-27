import { useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useParams, useNavigate } from "react-router-dom";
import {
  categorySchema,
  createCategorySchema,
  type CategoryDto,
  type CreateCategoryDto,
} from "../features/category/schemas/categorySchema";
import { useCreateCategory } from "../features/category/hooks/useCreateCategory";
import { useUpdateCategory } from "../features/category/hooks/useUpdateCategory";
import { useCategoryById } from "../features/category/hooks/useCategoryById";
import Loading from "../components/Loading";
import FormInput from "../components/FormInput";
import { useTitle } from "../hooks/useTitle";

const CreateUpdateCategory = () => {
  useTitle("Categories | LunoStore");
  const { id } = useParams(); // id for edit mode
  const navigate = useNavigate();
  const isEditMode = !!id;

  const methods = useForm<CategoryDto | CreateCategoryDto>({
    resolver: zodResolver(isEditMode ? categorySchema : createCategorySchema),
  });

  const { setValue } = methods;

  const { mutate: createCategory, isPending: isCreating } = useCreateCategory();
  const { mutate: updateCategory, isPending: isUpdating } = useUpdateCategory();
  const { data, isLoading: isFetching } = useCategoryById(id || "");

  useEffect(() => {
    if (data) {
      setValue("id", data.id);
      setValue("name", data.name);
    }
  }, [data, setValue]);

  const onSubmit = (formData: CategoryDto | CreateCategoryDto) => {
    console.log(formData);

    if (isEditMode) {
      updateCategory(
        { id: id!, categoryDto: formData as CategoryDto },
        { onSuccess: () => navigate("/admin/categories") }
      );
    } else {
      createCategory(formData as CreateCategoryDto, {
        onSuccess: () => navigate("/admin/categories"),
      });
    }
  };

  if (isEditMode && isFetching) return <Loading />;

  return (
    <div className="min-h-screen flex items-center justify-center bg-base-200">
      <div className="w-full max-w-md p-6 bg-base-100 shadow rounded-box">
        <h2 className="text-2xl font-bold mb-4 text-center">
          {isEditMode ? "Edit Category" : "Create Category"}
        </h2>
        <FormProvider {...methods}>
          <form onSubmit={methods.handleSubmit(onSubmit)} className="space-y-4">
            {isEditMode && <FormInput name="id" type="hidden" label="Id" />}
            <FormInput name="name" label="Name" type="text" />

            <button
              type="submit"
              className="btn btn-primary w-full"
              disabled={isCreating || isUpdating}
            >
              {isEditMode ? "Update Category" : "Create Category"}
            </button>
          </form>
        </FormProvider>
      </div>
    </div>
  );
};

export default CreateUpdateCategory;
