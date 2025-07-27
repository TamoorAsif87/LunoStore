import ProductCard from "../components/ProductCard";
import { useGetProducts } from "../features/products/hooks/useGetProducts";
import { Link } from "react-router-dom";
import { useTitle } from "../hooks/useTitle";

function Home() {
  useTitle("Home | LunoStore");
  const { data: products, isLoading } = useGetProducts();

  const displayedProducts = products?.slice(0, 5) || [];

  return (
    <div className="max-w-[1200px] mx-auto p-4">
      <h1 className="text-3xl font-bold mb-6">Featured Products</h1>

      {isLoading ? (
        <div className="flex justify-center items-center h-40">
          <span className="loading loading-spinner loading-lg text-primary"></span>
        </div>
      ) : (
        <>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            {displayedProducts.map((product) => (
              <ProductCard product={product} key={product.id} />
            ))}
          </div>

          {/* Centered Block Button */}
          <div className="mt-8 flex justify-center">
            <Link to="/products" className="btn btn-secondary w-1/2">
              Load More Products
            </Link>
          </div>
        </>
      )}
    </div>
  );
}

export default Home;
