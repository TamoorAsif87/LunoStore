import { useEffect, useRef } from "react";
import { useForm, FormProvider, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useParams, useNavigate } from "react-router-dom";

import { useCategories } from "../features/category/hooks/useCategories";
import { useProductById } from "../features/products/hooks/useGetProductById";
import { useCreateProduct } from "../features/products/hooks/useCreateProduct";
import { useUpdateProduct } from "../features/products/hooks/useUpdateProduct";

import {
  productCreateDtoSchema,
  productDtoSchema,
  type ProductCreateDto,
  type ProductDto,
} from "../features/products/schemas/productSchema";

import FormInput from "../components/FormInput";
import RichTextEditor from "../components/RichTextEditor";
import Loading from "../components/Loading";
import { useTitle } from "../hooks/useTitle";

const CreateUpdateProduct = () => {
  useTitle("Products | LunoStore");
  const { id } = useParams();
  const navigate = useNavigate();
  const isEditMode = !!id;

  const schema = isEditMode ? productDtoSchema : productCreateDtoSchema;

  const methods = useForm<ProductCreateDto | ProductDto>({
    resolver: zodResolver(schema),
    defaultValues: {
      productImages: [],
      productColors: [],
    },
  });

  const { register, setValue, watch } = methods;

  const imageInputRef = useRef<HTMLInputElement | null>(null);
  const selectedImages = useRef<File[]>([]);

  const { data: product, isLoading: isFetching } = useProductById(id || "");
  const { data: categories = [] } = useCategories();

  const { mutate: createProduct, isPending: isCreating } = useCreateProduct();
  const { mutate: updateProduct, isPending: isUpdating } = useUpdateProduct();

  useEffect(() => {
    if (product) {
      methods.reset({
        id: product.id,
        productName: product.productName,
        description: product.description,
        price: product.price,
        inStock: product.inStock,
        productColors: product.productColors,
        productImages: product.productImages,
        categoryId: product.categoryId,
      });
    }
  }, [product, methods]);

  const onSubmit = (formData: ProductCreateDto | ProductDto) => {
    const form = new FormData();

    form.append("productName", formData.productName);
    form.append("description", formData.description);
    form.append("price", formData.price.toString());
    form.append("inStock", formData.inStock.toString());
    form.append("categoryId", formData.categoryId);

    (formData.productColors || []).forEach((color, index) => {
      form.append(`productColors[${index}]`, color);
    });

    if (selectedImages.current.length > 0) {
      selectedImages.current.forEach((file) => {
        form.append("Files", file);
      });
    }

    if (
      isEditMode &&
      product?.productImages &&
      product?.productImages?.length > 0
    ) {
      product?.productImages?.forEach((img, index) => {
        form.append(`productImages[${index}]`, img);
      });
    }

    if (isEditMode) {
      const data = formData as ProductDto;

      form.append("id", data.id);

      updateProduct(
        { id: data.id!, formData: form },
        { onSuccess: () => navigate("/admin/products") }
      );
    } else {
      createProduct(form, { onSuccess: () => navigate("/admin/products") });
    }
  };

  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      selectedImages.current = Array.from(e.target.files);
    }
  };

  if (isEditMode && isFetching) return <Loading />;

  return (
    <div className="min-h-screen flex items-center justify-center bg-base-200">
      <div className="w-full max-w-2xl p-6 bg-base-100 shadow rounded-box">
        <h2 className="text-2xl font-bold mb-4 text-center">
          {isEditMode ? "Edit Product" : "Create Product"}
        </h2>

        <FormProvider {...methods}>
          <form
            onSubmit={methods.handleSubmit(onSubmit, (errors) => {
              console.log(errors);
            })}
            className="grid grid-cols-1 md:grid-cols-2 gap-4"
          >
            {isEditMode && <input type="hidden" {...register("id")} />}

            <FormInput name="productName" label="Product Name" type="text" />
            <FormInput
              name="price"
              label="Price ($)"
              type="number"
              step="0.01"
            />

            <input type="hidden" {...register("productImages")} />

            <FormInput name="inStock" label="Stock Quantity" type="number" />
            {/* Category Dropdown */}
            <div className="form-control">
              <label className="label" htmlFor="categoryId">
                <span className="label-text">Category</span>
              </label>
              <select
                id="categoryId"
                className="select select-bordered w-full"
                {...register("categoryId")}
              >
                <option value="">Select a category</option>
                {categories.map((cat) => (
                  <option key={cat.id} value={cat.id}>
                    {cat.name}
                  </option>
                ))}
              </select>
            </div>

            {/* Colors input */}
            <div className="form-control">
              <label className="label">
                <span className="label-text">Colors (comma-separated)</span>
              </label>
              <input
                type="text"
                placeholder="e.g. Red, Blue, Green"
                className="input input-bordered w-full"
                value={watch("productColors")?.join(", ")}
                onChange={(e) => {
                  const valueArray = e.target.value
                    .split(",")
                    .map((c) => c.trim())
                    .filter((c) => c !== "");

                  // Important: Pass { shouldValidate: true } to re-validate
                  setValue("productColors", valueArray, {
                    shouldValidate: true,
                    shouldDirty: true,
                  });
                }}
              />
            </div>

            {/* Description */}
            <div className="form-control col-span-1 md:col-span-2">
              <Controller
                name="description"
                control={methods.control}
                render={({ field }) => (
                  <RichTextEditor
                    value={field.value}
                    onChange={field.onChange}
                  />
                )}
              />
            </div>

            {/* File Upload */}
            <div className="form-control col-span-1 md:col-span-2 mt-12">
              <label className="label">
                <span className="label-text">Product Images</span>
              </label>
              <input
                type="file"
                accept="image/*"
                multiple
                ref={imageInputRef}
                className="file-input file-input-bordered w-full"
                onChange={handleImageChange}
              />
            </div>

            {/* Submit Button */}
            <div className="md:col-span-2">
              <button
                type="submit"
                className="btn btn-primary w-full"
                disabled={isCreating || isUpdating}
              >
                {isEditMode ? "Update Product" : "Create Product"}
              </button>
            </div>
          </form>
        </FormProvider>
      </div>
    </div>
  );
};

export default CreateUpdateProduct;
