import './App.css'
import Teachers from './Pages/Teachers/Teachers';
import AddTeacher from './Pages/Teachers/components/TeacherRow';
import { Toaster, toaster } from "@/components/ui/toaster"
import  DirectionPage  from './Pages/Direction/Direction';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
      

function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
            <Route path="/Direction" element={<DirectionPage />} />
            <Route path="/" element={ <Teachers /> } />
        </Routes>
      </BrowserRouter>
      <Toaster />
    </div>
  )
}

export default App;
