import { createContext, useContext, useState, useEffect } from 'react';
import axios from 'axios';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    // Проверяем аутентификацию при загрузке (например, через куки)
    useEffect(() => {
        const checkAuth = async () => {
            try {
                const response = await axios.get('https://localhost:7032/checkauth', {
                    withCredentials: true,
                });
                setUser(response.data);
            } catch (error) {
                setUser(null);
            } finally {
                setIsLoading(false);
            }
        };

        checkAuth();
    }, []);

    const login = async (loginData) => {
        const response = await axios.post('https://localhost:7032/login', loginData, {
            withCredentials: true,
        });
        setUser(response.data.user);
    };

    const register = async (registerData) => {
        const response = await axios.post('https://localhost:7032/register', registerData, {
            withCredentials: true,
        });
        setUser(response.data.user);
    };

    const logout = async () => {
        await axios.post('https://localhost:7032/logout', {}, { withCredentials: true });
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, isLoading, login, register, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);