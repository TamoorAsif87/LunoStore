import axois, { AxiosError } from "axios";
import { getToken } from "../Utils/localStorage";

export const axiosFetch = axois.create({
  baseURL: "http://localhost:5000/api",
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
    Authorization: `Bearer ${getToken() || ""}`,
  },
});

export const authFetch = axiosFetch;

authFetch.interceptors.request.use(
  (config) => {
    const token = getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error: AxiosError) => {
    if (error.response?.status === 401) {
      // Handle unauthorized access, e.g., redirect to login
      window.location.href = "/auth/login";
    }
    return Promise.reject(error);
  }
);
