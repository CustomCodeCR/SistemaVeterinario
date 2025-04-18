import React, { useEffect, useState } from 'react'
import logod from '../assets/logod.png'
import { Link } from 'react-router-dom'
import { FaUserCog } from 'react-icons/fa'

const Header: React.FC = () => {
  const [userName, setUserName] = useState<string | null>(null)
  const [isAdmin, setIsAdmin] = useState<boolean>(false)

  useEffect(() => {
    const userData = localStorage.getItem('userData')
    if (userData) {
      try {
        const parsed = JSON.parse(userData)
        const user = Array.isArray(parsed) ? parsed[0] : parsed

        setUserName(user?.name || null)
        setIsAdmin(user?.role === 'admin') // Cambia aquí si usas otro rol
      } catch (err) {
        console.error('Error al leer userData del localStorage', err)
      }
    }
  }, [])

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

        {/* Perfil con nombre del usuario */}
        <div className="flex items-center space-x-2 text-gray-700">
          <Link
            to="/profile"
            className="flex items-center transition-colors hover:text-blue-600"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="size-6 mr-1"
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
            {userName && <span className="hidden sm:inline">{userName}</span>}
          </Link>
        </div>
      </div>
    </header>
  )
}

export default Header
