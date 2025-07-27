import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import ProductCard from "../components/ProductCard";
import { productSpecsSchema } from "../features/products/schemas/productSchema";
import type z from "zod";
import { useCategories } from "../features/category/hooks/useCategories";
import { useSearchParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { useFilterProducts } from "../features/products/hooks/useFilterProducts";
import Loading from "../components/Loading";
import Pagination from "../components/Pagination";
import { useTitle } from "../hooks/useTitle";

const sortOptions = [
  { label: "Newest", value: "-created" },
  { label: "Oldest", value: "created" },
  { label: "Price: Low to High", value: "price" },
  { label: "Price: High to Low", value: "-price" },
  { label: "Name: A-Z", value: "name" },
  { label: "Name: Z-A", value: "-name" },
];

type ProductSpecs = z.infer<typeof productSpecsSchema>;

function FilterSidebar({
  onSubmit,
}: {
  onSubmit: (data: ProductSpecs) => void;
}) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(productSpecsSchema),
    defaultValues: {
      priceStart: 0,
      page: 1,
      priceEnd: 100,
    },
  });

  const { data: categories } = useCategories();

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="space-y-6 p-4 bg-base-200 rounded-xl"
    >
      <h2 className="text-xl font-semibold">Filter Products</h2>

      {/* Search */}
      <div className="form-control">
        <label className="label">
          <span className="label-text">Search Product</span>
        </label>
        <input
          className="input"
          type="text"
          placeholder="searchProduct"
          {...register("searchProduct")}
        />
      </div>

      {/* Price Range */}
      <div className="form-control">
        <label className="label">
          <span className="label-text">Price Range</span>
        </label>
        <div className="flex gap-2">
          <input
            type="number"
            placeholder="Min"
            {...register("priceStart", { valueAsNumber: true })}
            className="input input-bordered w-full"
          />
          <input
            type="number"
            placeholder="Max"
            {...register("priceEnd", { valueAsNumber: true })}
            className="input input-bordered w-full"
          />
        </div>
        {errors.priceEnd && (
          <p className="text-error text-sm mt-1">{errors.priceEnd.message}</p>
        )}
      </div>

      {/* Category */}
      <div className="form-control">
        <label className="label">
          <span className="label-text">Category</span>
        </label>
        <select
          {...register("categoryId")}
          className="select select-bordered w-full"
        >
          <option value="">All</option>
          {categories?.map((cat) => (
            <option key={cat.id} value={cat.id}>
              {cat.name}
            </option>
          ))}
        </select>
      </div>

      {/* Sort By */}
      <div className="form-control">
        <label className="label">
          <span className="label-text">Sort By</span>
        </label>
        <select
          {...register("sortBy")}
          className="select select-bordered w-full"
        >
          {sortOptions.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
      </div>

      <button type="submit" className="btn btn-primary w-full">
        Apply Filters
      </button>
    </form>
  );
}

function readUrlQueryParams(searchParams: URLSearchParams): ProductSpecs {
  const queryObject: Record<string, string | undefined> = {};
  searchParams.forEach((value, key) => {
    queryObject[key] = value;
  });

  const parsed = productSpecsSchema.safeParse({
    categoryId: queryObject.categoryId,
    searchProduct: queryObject.searchProduct,
    priceStart: queryObject.priceStart
      ? Number(queryObject.priceStart)
      : undefined,
    priceEnd: queryObject.priceEnd ? Number(queryObject.priceEnd) : undefined,
    sortBy: queryObject.sortBy,
    page: queryObject.page ? Number(queryObject.page) : undefined,
  });

  if (!parsed.success) {
    console.warn("Invalid query params", parsed.error.format());
    return productSpecsSchema.parse({});
  }

  return parsed.data;
}

export default function Products() {
  useTitle("Products | LunoStore");
  const [searchParams, setSearchParams] = useSearchParams();
  const [filters, setFilters] = useState<ProductSpecs>(() =>
    readUrlQueryParams(searchParams)
  );

  const makeFilterParams = (data: ProductSpecs): void => {
    const params = new URLSearchParams();

    if (data.searchProduct) params.set("searchProduct", data.searchProduct);
    if (data.categoryId) params.set("categoryId", data.categoryId);
    if (typeof data.priceStart === "number")
      params.set("priceStart", data.priceStart.toString());
    if (typeof data.priceEnd === "number")
      params.set("priceEnd", data.priceEnd.toString());
    if (data.sortBy) params.set("sortBy", data.sortBy);
    if (typeof data.page === "number") params.set("page", data.page.toString());

    setSearchParams(params);
  };

  const handleFilterSubmit = (data: ProductSpecs) => {
    makeFilterParams(data);
    const productSpecs: ProductSpecs = readUrlQueryParams(searchParams);
    setFilters(productSpecs);
  };

  useEffect(() => {
    const parsed = readUrlQueryParams(searchParams);
    setFilters(parsed);
  }, [searchParams]);

  const { data, isPending: isLoading } = useFilterProducts(filters);
  console.log(data);

  return (
    <div className="p-6 max-w-[1400px] mx-auto">
      <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
        {/* Sidebar */}
        <aside className="md:col-span-1">
          <FilterSidebar onSubmit={handleFilterSubmit} />
        </aside>

        {/* Product Grid */}
        <main className="md:col-span-3">
          <h1 className="text-2xl font-bold mb-6">All Products</h1>

          {isLoading ? (
            <Loading />
          ) : !data?.items.length ? (
            <p>No products found.</p>
          ) : (
            <>
              <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                {data.items.map((product) => (
                  <ProductCard key={product.id} product={product} />
                ))}
              </div>

              {/* ✅ Pagination shown only if totalPages > 1 */}
              <Pagination
                page={filters.page ?? 1}
                totalPages={data.totalPages}
                onPageChange={(newPage) => {
                  handleFilterSubmit({ ...filters, page: newPage });
                }}
              />
            </>
          )}
        </main>
      </div>
    </div>
  );
}
