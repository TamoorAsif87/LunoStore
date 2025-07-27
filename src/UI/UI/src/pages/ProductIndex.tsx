import { Link } from "react-router-dom";
import { FaEdit, FaTrash } from "react-icons/fa";
import Loading from "../components/Loading";
import { useGetProducts } from "../features/products/hooks/useGetProducts";
import { useDeleteProduct } from "../features/products/hooks/useDeleteProduct";
import { useState } from "react";
import { useTitle } from "../hooks/useTitle";

const MAX_SIZE = 5;

const ProductIndex = () => {
  useTitle("Products | LunoStore");
  const [page, setPage] = useState<number>(1);

  const { data: products, isLoading } = useGetProducts();
  const { mutate: deleteProduct } = useDeleteProduct();

  const handleDelete = (id: string) => {
    if (confirm("Are you sure you want to delete this product?")) {
      deleteProduct(id);
    }
  };

  const totalPages = products ? Math.ceil(products.length / MAX_SIZE) : 1;
  const paginatedItems = products?.slice(
    (page - 1) * MAX_SIZE,
    page * MAX_SIZE
  );

  return (
    <div className="p-4 mt-5">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-2xl font-bold">Products</h2>
        <Link to="/admin/products/create" className="btn btn-primary">
          + Create Product
        </Link>
      </div>

      {isLoading ? (
        <Loading />
      ) : (
        <>
          <div className="overflow-x-auto">
            <table className="table table-zebra w-full">
              <thead>
                <tr>
                  <th>#</th>
                  <th>Name</th>
                  <th>Price</th>
                  <th>InStock</th>
                  <th>Colors</th>
                  <th>Category</th>
                  <th className="text-right">Actions</th>
                </tr>
              </thead>
              <tbody>
                {paginatedItems?.map((product, index) => (
                  <tr key={product.id}>
                    <td>{(page - 1) * MAX_SIZE + index + 1}</td>
                    <td>{product.productName}</td>
                    <td>$ {product.price}</td>
                    <td>{product.inStock}</td>
                    <td>{product.productColors.join(", ")}</td>
                    <td>{product.category?.name}</td>
                    <td className="text-right space-x-2">
                      <Link
                        to={`/admin/products/${product.id}`}
                        className="btn btn-sm btn-outline btn-info"
                      >
                        <FaEdit />
                      </Link>
                      <button
                        onClick={() => handleDelete(product.id)}
                        className="btn btn-sm btn-outline btn-error"
                      >
                        <FaTrash />
                      </button>
                    </td>
                  </tr>
                ))}
                {products?.length === 0 && (
                  <tr>
                    <td colSpan={7} className="text-center py-4">
                      No products found.
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>

          {/* Pagination */}
          <div className="flex justify-center mt-6">
            <div className="join">
              <button
                className="join-item btn"
                disabled={page === 1}
                onClick={() => setPage((prev) => prev - 1)}
              >
                «
              </button>

              {Array.from({ length: totalPages }).map((_, idx) => (
                <button
                  key={idx}
                  className={`join-item btn ${
                    page === idx + 1 ? "btn-active" : ""
                  }`}
                  onClick={() => setPage(idx + 1)}
                >
                  {idx + 1}
                </button>
              ))}

              <button
                className="join-item btn"
                disabled={page === totalPages}
                onClick={() => setPage((prev) => prev + 1)}
              >
                »
              </button>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default ProductIndex;
