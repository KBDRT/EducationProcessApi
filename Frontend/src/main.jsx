import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { Provider } from "@/components/ui/provider"
import { TeachersProvider } from './Providers/TeachersProvider';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
      

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Provider>
      <TeachersProvider>
        <App />
      </TeachersProvider>
    </Provider>
  </StrictMode>,
)
