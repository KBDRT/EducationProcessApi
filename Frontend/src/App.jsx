import './App.css'
import Teachers from './Pages/Teachers/Teachers';
import AddTeacher from './Pages/Teachers/components/TeacherRow';
import { Toaster, toaster } from "@/components/ui/toaster"
import  DirectionPage  from './Pages/Direction/Direction';
import  RegisterPage  from './Pages/Auth/Register';
import  LoginPage  from './Pages/Auth/Login';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
      
import { AuthProvider } from './AuthContext';
import { ProtectedRoute } from './ProtectedRoute';
import { PublicNonAuthRoute } from './PublicNonAuthRoute';
import { TeachersProvider } from './Providers/TeachersProvider';

function App() {
  return (
    <div>
      <BrowserRouter>
        <AuthProvider>
                <Routes>
                    <Route path="/" element={<h1>Главная</h1>} />

                    <Route path="/login" element={
                      <PublicNonAuthRoute>
                          <LoginPage />
                      </PublicNonAuthRoute>
                    }/>

                    <Route path="/register" element={
                      <ProtectedRoute>
                          <RegisterPage />
                      </ProtectedRoute>
                    }/>

                    <Route path="/teachers" element={
                      <ProtectedRoute>
                        <TeachersProvider>
                            <Teachers />
                        </TeachersProvider>
                      </ProtectedRoute>
                    }/>
                </Routes>
            </AuthProvider>
      </BrowserRouter>
      <Toaster />
    </div>
  )
}

export default App;
