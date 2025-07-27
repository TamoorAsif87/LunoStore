import { z } from "zod";

// Address schema
export const addressSchema = z.object({
  firstName: z.string(),
  lastName: z.string(),
  emailAddress: z.email(),
  addressLine: z.string(),
  country: z.string(),
  zipCode: z.string(),
  city: z.string(),
  state: z.string(),
});

// Payment schema
export const paymentSchema = z.object({
  cardName: z.string().nullable().optional(),
  cardNumber: z.string(),
  expiration: z.string(),
  cvv: z.string(),
});

// Order item schema
export const orderItemSchema = z.object({
  productId: z.string().uuid(),
  price: z.number(),
  quantity: z.number().int(),
  orderId: z.string().uuid(),
  productImages: z.array(z.string()),
  productName: z.string(),
  productColors: z.array(z.string()),
});

// Order schema
export const orderSchema = z.object({
  orderId: z.string(),
  items: z.array(orderItemSchema),
  customerId: z.string().nullable().optional(),
  shippingAddress: addressSchema,
  billingsAddress: addressSchema,
  payment: paymentSchema,
});

export type AddressDto = z.infer<typeof addressSchema>;
export type PaymentDto = z.infer<typeof paymentSchema>;
export type OrderItemDto = z.infer<typeof orderItemSchema>;
export type OrderDto = z.infer<typeof orderSchema>;
