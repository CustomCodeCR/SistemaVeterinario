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
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
