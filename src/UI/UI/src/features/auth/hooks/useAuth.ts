import { useQueryClient } from "@tanstack/react-query";
import { useEffect, useState } from "react";
import { getUser } from "../../../Utils/localStorage";

export function useAuth() {
  const [user, setUser] = useState(getUser());
  const queryClient = useQueryClient();

  useEffect(() => {
    const interval = setInterval(() => {
      const currentUser = getUser();
      setUser(currentUser);
    }, 500);

    return () => clearInterval(interval);
  }, [queryClient]);

  return { user, isAuthenticated: !!user };
}
