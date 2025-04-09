import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { FaLock, FaEnvelope, FaSignInAlt } from 'react-icons/fa'
import Header from '../components/Header'

const Login = () => {
  const [formData, setFormData] = useState({
    username: '',
    password: ''
  })
  const [error, setError] = useState('')
  const [isLoading, setIsLoading] = useState(false)
  const navigate = useNavigate()

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: value
    }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setError('')

    // Validación básica
    if (!formData.username || !formData.password) {
      setError('Por favor completa todos los campos')
      return
    }

    try {
      setIsLoading(true)

      const response = await fetch('https://api.vetfriends.customcodecr.com/api/v1/Login/Login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(formData)
        }
      )

      // Verificar contenido de la respuesta antes de procesarlo
      const responseText = await response.text()
      let data = null

      // Intentar parsear la respuesta como JSON solo si tiene contenido
      if (responseText) {
        try {
          data = JSON.parse(responseText)
        } catch (jsonError) {
          console.error('Error parsing JSON response:', responseText)
          throw new Error('La respuesta del servidor no es un JSON válido')
        }
      }

      if (!response.ok) {
        // Si el servidor responde con un error
        const errorMessage =
          data?.message || `Error en la autenticación: ${response.status}`
        throw new Error(errorMessage)
      }

      // Verificar si tenemos una respuesta exitosa
      if (!data && response.ok) {
        // El servidor respondió con éxito pero sin datos
        console.log('Autenticación exitosa sin datos retornados')
      } else if (data) {
        // Guardar token o información de sesión si viene en la respuesta
        if (data.token) {
          localStorage.setItem('authToken', data.token)
        }

        // Si hay información de usuario en la respuesta, puedes guardarla también
        if (data.user) {
          localStorage.setItem('userData', JSON.stringify(data.user))
        }
      }

      navigate('/')
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Error durante el inicio de sesión')
      console.error('Login error:', err)
    } finally {
      setIsLoading(false)
    }
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <Header />

      <main className="flex items-center justify-center p-4 md:p-8">
        <div className="w-full max-w-md overflow-hidden rounded-xl bg-white shadow-lg">
          {/* Título */}
          <div className="bg-blue-600 px-6 py-4 text-center">
            <h2 className="text-xl font-semibold text-white">Iniciar Sesión</h2>
          </div>

          {/* Formulario */}
          <form onSubmit={handleSubmit} className="space-y-6 p-6">
            {error && (
              <div className="rounded border border-red-400 bg-red-100 px-4 py-3 text-red-700">
                {error}
              </div>
            )}

            <div className="space-y-2">
              <label htmlFor="username" className="block text-gray-700">
                Nombre de Usuario
              </label>
              <div className="relative">
                <span className="absolute inset-y-0 left-0 flex items-center pl-3">
                  <FaEnvelope className="text-gray-400" />
                </span>
                <input
                  type="text"
                  id="username"
                  name="username"
                  value={formData.username}
                  onChange={handleChange}
                  className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                  placeholder="Nombre de usuario"
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
                  placeholder="Tu contraseña"
                  minLength={6}
                />
              </div>
            </div>

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

              <a
                href="/forgot-password"
                className="text-sm text-blue-600 hover:text-blue-500"
              >
                ¿Olvidaste tu contraseña?
              </a>
            </div>

            <button
              type="submit"
              disabled={isLoading}
              className="flex w-full items-center justify-center rounded-lg bg-blue-600 px-4 py-3 font-medium text-white transition duration-200 hover:bg-blue-700 disabled:bg-blue-400"
            >
              {isLoading ? (
                'Iniciando sesión...'
              ) : (
                <>
                  <FaSignInAlt className="mr-2" />
                  Iniciar Sesión
                </>
              )}
            </button>
          </form>
        </div>
      </main>
    </div>
  )
}

export default Login
