import { Navigate } from 'react-router-dom';
import { useAuth } from './AuthContext';

export const ProtectedRoute = ({ children }) => {
    const { user, isLoading } = useAuth();

    if (isLoading) {
        return <div>Loading...</div>; // Можно добавить спиннер
    }

    if (!user) {
        return <Navigate to="/login" replace />;
    }

    return children;
};