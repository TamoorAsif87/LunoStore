import { useMutation } from "@tanstack/react-query";
import { refreshToken as refreshTokenApi } from "../../../Api/Auth";
import {
  getToken,
  getRefreshToken,
  addToken,
  addRefreshToken,
  type ApiUserResponse,
  addUser,
  removeRefreshToken,
  removeToken,
  removeUser,
} from "../../../Utils/localStorage";
import type { AxiosError } from "axios";

export function useRefreshToken() {
  return useMutation({
    mutationFn: () => {
      const accessToken = getToken();
      const refreshToken = getRefreshToken();

      if (!accessToken || !refreshToken) {
        return Promise.reject(new Error("Missing tokens"));
      }

      return refreshTokenApi({
        accessToken,
        refreshToken,
      });
    },
    onSuccess: (data: ApiUserResponse) => {
      addToken(data.accessToken);
      addRefreshToken(data.refreshToken);

      if (data.user) {
        addUser({ ...data.user, role: data.role });
      }
    },
    onError: (error: AxiosError<{ detail: string }>) => {
      console.error("Refresh token error:", error);
      const message =
        error?.response?.data?.detail || "Session refresh failed.";
      removeToken();
      removeRefreshToken();
      removeUser();
      console.log(message);
    },
  });
}

export default useRefreshToken;
