import { useSelector } from "react-redux";
import type { ProductDto } from "../features/products/schemas/productSchema";
import type { RootState } from "../store";
import { useAppDispatch } from "../store/hook";
import { useNavigate } from "react-router-dom";
import {
  addItem,
  createBasket,
  type ShoppingCartItem,
} from "../features/basket/basket";
import { getBasketUserName } from "../Utils/localStorage";

type ProductCardProps = {
  product: ProductDto;
};

export default function ProductCard({ product }: ProductCardProps) {
  const cart = useSelector((state: RootState) => state.basket.cart);
  const id = cart.id;
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleNavigateToProduct = () => {
    navigate(`/products/${product.id}`);
  };

  const handleAddToCart = (product: ProductDto) => {
    const shoppingCartItem: ShoppingCartItem = {
      productImages: product.productImages ?? [],
      productColors: product.productColors,
      productName: product.productName,
      productId: product.id,
      quantity: 1,
      price: product.price,
    };

    if (id === undefined) {
      dispatch(
        createBasket({
          userName: getBasketUserName(),
          items: [{ ...shoppingCartItem }],
          id: undefined,
          totalItems: 1,
          totalPrice: shoppingCartItem.price,
        })
      );
    } else {
      dispatch(
        addItem({ userName: cart.userName, item: { ...shoppingCartItem } })
      );
    }
  };

  return (
    <div className="card bg-base-100 shadow-md h-full flex flex-col">
      <div
        className="flex flex-col flex-grow cursor-pointer"
        onClick={handleNavigateToProduct}
      >
        <figure className="h-64 bg-gray-100 flex justify-center items-center">
          <img
            src={
              product.productImages?.[0] ||
              "https://via.placeholder.com/300x200"
            }
            alt={product.productName}
            className="w-full h-full object-contain p-4"
          />
        </figure>
        <div className="card-body flex flex-col justify-between flex-grow">
          <div>
            <h2 className="card-title text-lg">{product.productName}</h2>
            <div className="mt-2 flex justify-between items-center">
              <span className="font-semibold text-primary">
                ${product.price}
              </span>
              <span className="text-xs text-gray-400">
                {product.inStock} in stock
              </span>
            </div>
          </div>
        </div>
      </div>

      {/* Non-clickable Add to Cart button */}
      <div className="px-4 pb-4">
        <button
          onClick={() => handleAddToCart(product)}
          className="btn btn-primary w-full"
          disabled={product.inStock <= 0}
        >
          Add to Cart
        </button>
      </div>
    </div>
  );
}
