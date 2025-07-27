import { Link } from "react-router-dom";
import { FaEdit, FaTrash } from "react-icons/fa";
import { useCategories } from "../features/category/hooks/useCategories";
import Loading from "../components/Loading";
import { useDeleteCategory } from "../features/category/hooks/useDeleteCategory";
import { useTitle } from "../hooks/useTitle";

const Categories = () => {
  useTitle("Categories | LunoStore");
  const { data: categories, isLoading } = useCategories();
  const { mutate: deleteCategory } = useDeleteCategory();

  const handleDelete = (id: string) => {
    if (confirm("Are you sure you want to delete this category?")) {
      deleteCategory(id);
    }
  };

  return (
    <div className="p-4 mt-5">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-2xl font-bold">Categories</h2>
        <Link to="/admin/categories/create" className="btn btn-primary">
          + Create Category
        </Link>
      </div>

      {isLoading ? (
        <Loading />
      ) : (
        <div className="overflow-x-auto">
          <table className="table table-zebra w-full">
            <thead>
              <tr>
                <th>#</th>
                <th>Name</th>
                <th className="text-right">Actions</th>
              </tr>
            </thead>
            <tbody>
              {categories?.map((category, index) => (
                <tr key={category.id}>
                  <td>{index + 1}</td>
                  <td>{category.name}</td>
                  <td className="text-right space-x-2">
                    <Link
                      to={`/admin/categories/${category.id}`}
                      className="btn btn-sm btn-outline btn-info"
                    >
                      <FaEdit />
                    </Link>
                    <button
                      onClick={() => handleDelete(category.id)}
                      className="btn btn-sm btn-outline btn-error"
                    >
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
              {categories?.length === 0 && (
                <tr>
                  <td colSpan={3} className="text-center py-4">
                    No categories found.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default Categories;
