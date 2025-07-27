import { useQuery } from "@tanstack/react-query";
import type { UserDto } from "../../../Utils/localStorage";
import { getUsers } from "../../../Api/Auth";

export function useGetAllUsers() {
  return useQuery<UserDto[]>({
    queryKey: ["admin:users"],
    queryFn: getUsers,
  });
}
