import { useQuery } from "@tanstack/react-query";
import { getProfile } from "../../../Api/Auth";
import type { UserProfile } from "../schemas/userProfileSchema";

export const useGetProfile = () => {
  return useQuery<UserProfile>({
    queryKey: ["user-profile"],
    queryFn: getProfile,
  });
};
