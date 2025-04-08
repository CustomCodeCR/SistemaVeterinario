// App.tsx
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Products from './pages/Products'
import Home from './pages/home'
import About from './pages/About'
import Contact from './pages/Contact'
import Profile from './pages/Profile'
import Services from './pages/Services'
import NotFound from './components/NotFound'
import Admin from './pages/Admin'
import Appointments from './pages/Appointments'
import ServiciosClinicos from './pages/ServiciosClinicos'
import SalesReport from 'pages/SalesReport'
import Addproduct from './pages/Addproduct'
import AdminUsers from './pages/AdminUsers'
import VaccineControl from './pages/VaccineControl'
import CartPage from './pages/cart'
import AdminCategories from 'pages/AdminCategories';


function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/products" element={<Products />} />
        <Route path="/about" element={<About />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/profile" element={<Profile />} />
        <Route path="/services" element={<Services />} />
        <Route path="/admin" element={<Admin />} />
        <Route path="/appointments" element={<Appointments />} />
        <Route path="/ServiciosClinicos" element={<ServiciosClinicos />} />
        <Route path="/SalesReport" element={<SalesReport />} />
        <Route path="*" element={<NotFound />} />
        <Route path="/admin/add-product" element={<Addproduct />} />
        <Route path="/admin/users" element={<AdminUsers />} />
        <Route path="/control-vacunas" element={<VaccineControl />} />
        <Route path="/cart" element={<CartPage />} /> {/* Ruta del carrito */}
        <Route path="/AdminCategories" element={<AdminCategories />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
