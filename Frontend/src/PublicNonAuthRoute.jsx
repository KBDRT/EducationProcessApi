import { Navigate } from 'react-router-dom';
import { useAuth } from './AuthContext';

export const PublicNonAuthRoute = ({ children }) => {
    const { user, isLoading } = useAuth();

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (user) {
        return <Navigate to="/" replace />; 
    }

    return children;
};