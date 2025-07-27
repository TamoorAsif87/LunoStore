import {
  orderSchema,
  type OrderDto,
} from "../features/orders/schemas/orderSchema";
import { authFetch, axiosFetch } from "./AxiosFetch";

// ✅ Get all orders (Admin only)
export async function getAllOrders(): Promise<OrderDto[]> {
  const response = await authFetch.get("/order");
  return response.data;
}

//Get orders by customerId
export async function getOrdersByCustomerId(
  customerId: string
): Promise<OrderDto[]> {
  const response = await axiosFetch.get(`/order/customer/${customerId}`);
  return response.data;
}

//  Get single order by ID
export async function getOrderById(id: string): Promise<OrderDto> {
  const response = await axiosFetch.get(`/order/${id}`);
  const parsed = orderSchema.parse(response.data);
  return parsed;
}

//  Create a new order
export async function createOrder(order: OrderDto): Promise<{ id: string }> {
  const response = await authFetch.post("/order", order);
  return response.data;
}

//  Update order (Admin only)
export async function updateOrder(order: OrderDto): Promise<void> {
  await authFetch.put("/order", order);
}

//  Delete order (Admin only)
export async function deleteOrder(id: string): Promise<void> {
  await authFetch.delete(`/order/${id}`);
}
