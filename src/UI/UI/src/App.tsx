import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Toaster } from "react-hot-toast";
import {
  HomeLayout,
  About,
  Contact,
  Products,
  Register,
  Login,
  Home,
  ProductIndex,
  CreateUpdateProduct,
  Users,
  CreateUpdateUser,
  Orders,
  Categories,
  CreateUpdateCategory,
  AuthLayout,
  ChangePassword,
  ProtectedLayout,
  ForgotPassword,
  ResetPassword,
  ProtectedAdmin,
  AdminHome,
  UserProfile,
  Basket,
  ProductDetailsPage,
  Checkout,
  CheckoutComplete,
  ViewOrder,
  MyOrders,
  NotFound,
} from "./pages";
import { Provider } from "react-redux";
import { store } from "./store";
import AuthInitializer from "./components/AuthInitializer";
import { useEffect, useState } from "react";
import Loading from "./components/Loading";

const queryClient = new QueryClient();

const router = createBrowserRouter([
  {
    path: "/",
    element: <HomeLayout />,
    children: [
      { index: true, element: <Home /> },
      { path: "products", element: <Products /> },
      { path: "products/:id", element: <ProductDetailsPage /> },
      { path: "about", element: <About /> },
      { path: "contact", element: <Contact /> },
      { path: "basket", element: <Basket /> },
      { path: "checkout", element: <Checkout /> },
      { path: "checkout/complete/:id", element: <CheckoutComplete /> },
      { path: "user/orders/:id", element: <MyOrders /> },
      {
        path: "user",
        element: <ProtectedLayout />,
        children: [
          {
            path: "change-password",
            element: <ChangePassword />,
          },
          {
            path: "profile",
            element: <UserProfile />,
          },
        ],
      },
    ],
  },
  {
    path: "/admin",
    element: <ProtectedAdmin />,
    children: [
      { index: true, element: <Navigate to="dashboard" replace /> },
      { path: "dashboard", element: <AdminHome /> },
      { path: "products", element: <ProductIndex /> },
      { path: "products/create", element: <CreateUpdateProduct /> },
      { path: "products/:id", element: <CreateUpdateProduct /> },
      { path: "users", element: <Users /> },
      { path: "users/create", element: <CreateUpdateUser /> },
      { path: "users/:id", element: <CreateUpdateUser /> },
      { path: "orders", element: <Orders /> },
      { path: "orders/:id", element: <ViewOrder /> },
      { path: "categories", element: <Categories /> },
      { path: "categories/create", element: <CreateUpdateCategory /> },
      { path: "categories/:id", element: <CreateUpdateCategory /> },
    ],
  },

  {
    path: "/auth",
    element: <AuthLayout />,
    children: [
      {
        path: "login",
        element: <Login />,
      },
      {
        path: "register",
        element: <Register />,
      },
      {
        path: "forgot-password",
        element: <ForgotPassword />,
      },
      {
        path: "reset-password",
        element: <ResetPassword />,
      },
    ],
  },
  {
    path: "*",
    element: <NotFound />,
  },
]);

function App() {
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setLoading(false);
    }, 2000); // 10 seconds

    return () => clearTimeout(timeout);
  }, []);

  if (loading) {
    return <Loading />; // or any splash screen
  }

  return (
    <Provider store={store}>
      <QueryClientProvider client={queryClient}>
        <AuthInitializer />
        <RouterProvider router={router} />
        <Toaster position="top-right" reverseOrder={false} />
      </QueryClientProvider>
    </Provider>
  );
}

export default App;
