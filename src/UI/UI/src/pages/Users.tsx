import Loading from "../components/Loading";
import { useGetAllUsers } from "../features/auth/hooks/useGetAllUsers";
import { useTitle } from "../hooks/useTitle";

function Users() {
  useTitle("Users| LunoStore");
  const { data: users, isPending: isLoading, isError } = useGetAllUsers();

  if (isLoading) return <Loading />;
  if (isError)
    return <div className="p-6 text-red-500">Failed to load users.</div>;

  return (
    <div className="max-w-[1100px] mx-auto px-4 py-8">
      <h1 className="text-2xl font-bold mb-6">All Users</h1>
      <div className="overflow-x-auto">
        <table className="table w-full border-collapse border rounded-md overflow-hidden shadow-sm">
          <thead className="bg-gray-100 text-left text-sm text-gray-700">
            <tr>
              <th className="p-3">#</th>
              <th className="p-3">Name</th>
              <th className="p-3">Email</th>
              <th className="p-3">Phone</th>
            </tr>
          </thead>
          <tbody className="text-sm text-gray-800 divide-y">
            {users?.map((user, index) => (
              <tr key={user.id}>
                <td className="p-3">{index + 1}</td>
                <td className="p-3">{user.name || "—"}</td>
                <td className="p-3">{user.email}</td>
                <td className="p-3">{user.phone || "—"}</td>
              </tr>
            ))}
          </tbody>
        </table>

        {users?.length === 0 && (
          <p className="text-center mt-4 text-gray-500">No users found.</p>
        )}
      </div>
    </div>
  );
}

export default Users;
