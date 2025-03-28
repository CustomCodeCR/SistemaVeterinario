import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import {
  FaUser,
  FaLock,
  FaEnvelope,
  FaSignInAlt,
  FaUserPlus
} from 'react-icons/fa'
import Header from '../components/Header'

const Profile = () => {
  const [isLogin, setIsLogin] = useState(true)
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    name: ''
  })
  const [error, setError] = useState('')
  const navigate = useNavigate()

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: value
    }))
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    setError('')

    // Validación básica
    if (!formData.email || !formData.password) {
      setError('Por favor completa todos los campos')
      return
    }

    if (!isLogin && !formData.name) {
      setError('Por favor ingresa tu nombre')
      return
    }

    // Aquí iría tu lógica de autenticación real
    console.log('Datos enviados:', formData)

    // Simulación de autenticación exitosa
    setTimeout(() => {
      navigate('/profile/dashboard')
    }, 1000)
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <Header />

      <main className="flex items-center justify-center p-4 md:p-8">
        <div className="w-full max-w-md overflow-hidden rounded-xl bg-white shadow-lg">
          {/* Pestañas Login/Signup */}
          <div className="flex border-b">
            <button
              className={`flex-1 px-6 py-4 text-center font-medium ${
                isLogin ? 'bg-blue-600 text-white' : 'bg-gray-100 text-gray-700'
              }`}
              onClick={() => setIsLogin(true)}
            >
              <FaSignInAlt className="mr-2 inline" />
              Iniciar Sesión
            </button>
            <button
              className={`flex-1 px-6 py-4 text-center font-medium ${
                !isLogin
                  ? 'bg-blue-600 text-white'
                  : 'bg-gray-100 text-gray-700'
              }`}
              onClick={() => setIsLogin(false)}
            >
              <FaUserPlus className="mr-2 inline" />
              Registrarse
            </button>
          </div>

          {/* Formulario */}
          <form onSubmit={handleSubmit} className="space-y-6 p-6">
            {error && (
              <div className="rounded border border-red-400 bg-red-100 px-4 py-3 text-red-700">
                {error}
              </div>
            )}

            {!isLogin && (
              <div className="space-y-2">
                <label htmlFor="name" className="block text-gray-700">
                  Nombre Completo
                </label>
                <div className="relative">
                  <span className="absolute inset-y-0 left-0 flex items-center pl-3">
                    <FaUser className="text-gray-400" />
                  </span>
                  <input
                    type="text"
                    id="name"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                    placeholder="Tu nombre"
                  />
                </div>
              </div>
            )}

            <div className="space-y-2">
              <label htmlFor="email" className="block text-gray-700">
                Correo Electrónico
              </label>
              <div className="relative">
                <span className="absolute inset-y-0 left-0 flex items-center pl-3">
                  <FaEnvelope className="text-gray-400" />
                </span>
                <input
                  type="email"
                  id="email"
                  name="email"
                  value={formData.email}
                  onChange={handleChange}
                  className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                  placeholder="tu@email.com"
                />
              </div>
            </div>

            <div className="space-y-2">
              <label htmlFor="password" className="block text-gray-700">
                Contraseña
              </label>
              <div className="relative">
                <span className="absolute inset-y-0 left-0 flex items-center pl-3">
                  <FaLock className="text-gray-400" />
                </span>
                <input
                  type="password"
                  id="password"
                  name="password"
                  value={formData.password}
                  onChange={handleChange}
                  className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                  placeholder={
                    isLogin ? 'Tu contraseña' : 'Crea una contraseña'
                  }
                  minLength={6}
                />
              </div>
            </div>

            {isLogin && (
              <div className="flex items-center justify-between">
                <div className="flex items-center">
                  <input
                    id="remember-me"
                    name="remember-me"
                    type="checkbox"
                    className="size-4 rounded border-gray-300 text-blue-600 focus:ring-blue-500"
                  />
                  <label
                    htmlFor="remember-me"
                    className="ml-2 block text-sm text-gray-700"
                  >
                    Recordarme
                  </label>
                </div>

                <Link
                  to="/forgot-password"
                  className="text-sm text-blue-600 hover:text-blue-500"
                >
                  ¿Olvidaste tu contraseña?
                </Link>
              </div>
            )}

            <button
              type="submit"
              className="flex w-full items-center justify-center rounded-lg bg-blue-600 px-4 py-3 font-medium text-white transition duration-200 hover:bg-blue-700"
            >
              {isLogin ? (
                <>
                  <FaSignInAlt className="mr-2" />
                  Iniciar Sesión
                </>
              ) : (
                <>
                  <FaUserPlus className="mr-2" />
                  Registrarse
                </>
              )}
            </button>
          </form>
        </div>
      </main>
    </div>
  )
}

export default Profile
