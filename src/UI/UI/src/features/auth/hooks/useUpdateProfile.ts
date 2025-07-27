import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateProfile } from "../../../Api/Auth";
import type { AxiosError } from "axios";
import toast from "react-hot-toast";
import type { UserProfile } from "../schemas/userProfileSchema";
import { updateUserProfile } from "../../../Utils/localStorage";

export function useUpdateProfile() {
  const queryClient = useQueryClient();

  return useMutation<UserProfile, AxiosError<{ detail: string }>, FormData>({
    mutationFn: updateProfile,
    onSuccess: (data) => {
      toast.success("Profile updated successfully!");
      // Optionally refetch the profile query to update the UI

      updateUserProfile(data);

      queryClient.setQueryData(["profile"], data);

      //dispatch event to update user profile in local storage

      window.dispatchEvent(new Event("userProfileUpdated"));
    },
    onError: (error) => {
      const message =
        error.response?.data?.detail || "Failed to update profile.";
      toast.error(message);
    },
  });
}
