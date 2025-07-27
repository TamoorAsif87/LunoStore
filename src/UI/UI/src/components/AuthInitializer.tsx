import { useEffect } from "react";
import { useRefreshToken } from "../features/auth/hooks/useRefreshToken";

const AuthInitializer = () => {
  const { mutate: refreshSession } = useRefreshToken();

  useEffect(() => {
    refreshSession();
  }, [refreshSession]);

  return null;
};

export default AuthInitializer;
