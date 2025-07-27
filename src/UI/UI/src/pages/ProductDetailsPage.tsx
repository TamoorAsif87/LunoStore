import { useNavigate, useParams } from "react-router-dom";
import { useSelector } from "react-redux";

import { useState } from "react";
import { useProductById } from "../features/products/hooks/useGetProductById";
import type { RootState } from "../store";
import { useAppDispatch } from "../store/hook";
import {
  addItem,
  createBasket,
  type ShoppingCartItem,
} from "../features/basket/basket";
import Loading from "../components/Loading";

export default function ProductDetailsPage() {
  const { id } = useParams<{ id: string }>();
  const { data: product, isLoading, isError } = useProductById(id!);
  const cart = useSelector((state: RootState) => state.basket.cart);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const [quantity, setQuantity] = useState(1);
  const [selectedColor, setSelectedColor] = useState<string | null>(null);

  const handleAddToCart = async () => {
    if (!product) return;

    const shoppingCartItem: ShoppingCartItem = {
      productId: product.id,
      productName: product.productName,
      price: product.price,
      quantity,
      productImages: product.productImages ?? [],
      productColors: selectedColor ? [selectedColor] : product.productColors,
    };

    try {
      if (!cart.id) {
        await dispatch(
          createBasket({
            userName: cart.userName,
            items: [shoppingCartItem],
            id: undefined,
            totalItems: quantity,
            totalPrice: product.price * quantity,
          })
        ).unwrap(); // Wait for basket creation + fetch
      } else {
        await dispatch(
          addItem({ userName: cart.userName, item: shoppingCartItem })
        ).unwrap(); // Wait for item add + fetch
      }

      navigate("/basket"); // After state is updated
    } catch (err) {
      console.error("Add to cart failed", err);
      // Optional: show toast or alert
    }
  };

  if (isLoading) return <Loading />;
  if (isError || !product) return <div className="p-4">Product not found.</div>;

  return (
    <div className="max-w-6xl mx-auto px-4 py-8 grid grid-cols-1 md:grid-cols-2 gap-10">
      <div>
        <img
          src={product.productImages?.[0] || "https://via.placeholder.com/400"}
          alt={product.productName}
          className="w-full h-[400px] object-contain bg-gray-100 rounded-lg"
        />
      </div>

      {/* Product Details */}
      <div className="flex flex-col space-y-6">
        <div>
          <h1 className="text-2xl font-semibold">{product.productName}</h1>
          <p className="text-lg text-primary font-bold mt-2">
            ${product.price.toFixed(2)}
          </p>
        </div>

        <div>
          <h4 className="font-semibold mb-2">Description</h4>
          <div
            className="prose max-w-none text-gray-700"
            dangerouslySetInnerHTML={{ __html: product.description }}
          />
        </div>

        {/* Colors */}
        {product.productColors?.length > 0 && (
          <div>
            <h4 className="font-medium mb-2">Available Colors</h4>
            <div className="flex gap-2">
              {product.productColors.map((color) => (
                <button
                  key={color}
                  onClick={() => setSelectedColor(color)}
                  className={`w-8 h-8 rounded-full border-2 ${
                    selectedColor === color
                      ? "ring-2 ring-primary"
                      : "border-gray-300"
                  }`}
                  style={{ backgroundColor: color }}
                  title={color}
                />
              ))}
            </div>
          </div>
        )}

        {/* Quantity Selector */}
        <div className="flex items-center gap-4">
          <span className="font-medium">Quantity:</span>
          <div className="flex items-center border rounded w-fit">
            <button
              onClick={() => setQuantity((q) => Math.max(1, q - 1))}
              className="px-3 py-1 text-xl"
            >
              –
            </button>
            <span className="px-4 py-1">{quantity}</span>
            <button
              onClick={() => setQuantity((q) => q + 1)}
              className="px-3 py-1 text-xl"
            >
              +
            </button>
          </div>
        </div>

        {/* Add to Cart */}
        <button
          onClick={handleAddToCart}
          className="btn btn-primary mt-4 w-full md:w-1/2"
          disabled={product.inStock <= 0}
        >
          {product.inStock > 0 ? "Add to Cart" : "Out of Stock"}
        </button>

        <p className="text-sm text-gray-500">
          {product.inStock} items in stock
        </p>
      </div>
    </div>
  );
}
