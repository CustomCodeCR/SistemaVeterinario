import React, { useEffect, useState } from 'react'
import logod from '../assets/logod.png'
import { Link, useNavigate } from 'react-router-dom'
import { FaUserCog } from 'react-icons/fa'

const Header: React.FC = () => {
  const [userName, setUserName] = useState<string | null>(null)
  const navigate = useNavigate()

  useEffect(() => {
    const userData = localStorage.getItem('userData')
    if (userData) {
      try {
        const parsed = JSON.parse(userData)
        const name = Array.isArray(parsed) ? parsed[0]?.name : parsed.name
        setUserName(name)
      } catch (err) {
        console.error('Error al leer userData del localStorage', err)
      }
    }
  }, [])

  const handleLogout = () => {
    localStorage.removeItem('authToken')
    localStorage.removeItem('userData')
    navigate('/profile') // O a tu ruta de login
  }

  // Puedes cambiar esto por tu lógica de autenticación real
  const isAdmin = true

  return (
    <header className="text-back flex items-center justify-between bg-white p-4 shadow-sm">
      {/* Logo a la izquierda */}
      <div className="mb-4 flex items-center md:mb-0">
        <img src={logod} alt="Logo" className="mr-2 size-8" />
        <h1 className="text-2xl font-extrabold text-blue-900 sm:text-2xl sm:tracking-tight lg:text-2xl">
          Vets<span className="text-orange-500">Friends</span>
        </h1>
      </div>

      {/* Navbar en el centro */}
      <nav className="hidden space-x-8 md:flex">
        <Link to="/" className="transition-colors hover:text-gray-600">
          Inicio
        </Link>
        <Link to="/products" className="transition-colors hover:text-gray-600">
          Productos
        </Link>
        <Link to="/services" className="transition-colors hover:text-gray-600">
          Servicios
        </Link>
        <Link to="/about" className="transition-colors hover:text-gray-600">
          Sobre Nosotros
        </Link>
        <Link to="/cart" className="transition-colors hover:text-gray-600">
          Carrito
        </Link>
      </nav>

      {/* Usuario y acciones */}
      <div className="flex items-center space-x-4">
        {isAdmin && (
          <Link
            to="/admin"
            className="flex items-center space-x-1 rounded bg-orange-600 px-3 py-2 text-white transition-colors hover:bg-blue-700"
          >
            <FaUserCog className="size-4" />
            <span className="hidden sm:inline">Administrar</span>
          </Link>
        )}

        <Link
          to="/contact"
          className="rounded bg-black px-4 py-2 text-white transition-colors hover:bg-gray-800"
        >
          Contacto
        </Link>

        {userName && (
          <div className="hidden sm:flex flex-col items-end text-sm text-gray-700">
            <span className="font-semibold">Hola, {userName.split(' ')[0]}</span>
            <button
              onClick={handleLogout}
              className="text-blue-600 hover:text-red-500 text-xs mt-1 transition-colors"
            >
              Cerrar sesión
            </button>
          </div>
        )}

        <Link
          to="/profile"
          className="text-gray-700 transition-colors hover:text-blue-600"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="size-6"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"
            />
          </svg>
        </Link>
      </div>
    </header>
  )
}

export default Header
