import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "./AuthContext";

export default function ProtectedRoute() {
  const { token, isLoading } = useAuth();

  if (isLoading) return <div className="p-6 text-slate-600">Loading...</div>;

  return token ? <Outlet /> : <Navigate to="/login" replace />;
}