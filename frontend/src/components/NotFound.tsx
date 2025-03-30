import { useNavigate } from 'react-router-dom'
import { motion } from 'framer-motion'
import { useEffect, useState } from 'react'

const NotFound = () => {
  const navigate = useNavigate()
  const [errorMessage, setErrorMessage] = useState('')
  const [animalIcon, setAnimalIcon] = useState('🐶')

  const errorMessages = [
    '¡Ups! Parece que este pájaro voló lejos del nido',
    'Este pez se escapó del acuario',
    'El gato juguetón escondió esta página',
    'El perro excavó demasiado y la página se perdió',
    'La tortuga se tomó su tiempo y no llegó a cargar',
    'El hamster se cansó de correr en la rueda',
    'La araña tejió su tela en el servidor',
    'El conejo saltó muy lejos con esta página',
    'El erizo se enrolló alrededor del enlace',
    'El zorro escondió esta página en su madriguera',
    'El búho no pudo encontrar esta página en la noche',
    'La jirafa estiró demasiado el cuello y no la vio'
  ]

  const animals = [
    '🐶',
    '🐱',
    '🐭',
    '🐹',
    '🐰',
    '🦊',
    '🐻',
    '🐼',
    '🐨',
    '🐯',
    '🦁',
    '🐮',
    '🐷',
    '🐸',
    '🐵'
  ]

  useEffect(() => {
    document.title = 'Página no encontrada | VetsFriends'
    // Seleccionar un mensaje aleatorio
    setErrorMessage(
      errorMessages[Math.floor(Math.random() * errorMessages.length)]
    )
    // Seleccionar un animal aleatorio
    setAnimalIcon(animals[Math.floor(Math.random() * animals.length)])
  }, [])

  return (
    <div className="flex min-h-screen flex-col items-center justify-center bg-gradient-to-br from-blue-50 to-purple-50 p-6">
      <motion.div
        initial={{ scale: 0.8, opacity: 0 }}
        animate={{ scale: 1, opacity: 1 }}
        transition={{ duration: 0.5 }}
        className="max-w-2xl text-center"
      >
        {/* Animación del número 404 */}
        <div className="relative mb-8 flex justify-center">
          <motion.div
            animate={{
              rotate: [0, 10, -10, 0],
              y: [0, -20, 0]
            }}
            transition={{
              repeat: Infinity,
              duration: 3,
              ease: 'easeInOut'
            }}
            className="text-9xl font-bold text-gray-800"
          >
            4
          </motion.div>
          <motion.div
            animate={{
              scale: [1, 1.1, 1],
              rotate: [0, -5, 5, 0]
            }}
            transition={{
              repeat: Infinity,
              duration: 2,
              ease: 'easeInOut',
              delay: 0.5
            }}
            className="mx-2 text-9xl font-bold text-gray-800"
          >
            <span className="text-6xl">{animalIcon}</span>
          </motion.div>
          <motion.div
            animate={{
              rotate: [0, -10, 10, 0],
              y: [0, 20, 0]
            }}
            transition={{
              repeat: Infinity,
              duration: 3.5,
              ease: 'easeInOut',
              delay: 0.2
            }}
            className="text-9xl font-bold text-gray-800"
          >
            4
          </motion.div>
        </div>

        <h1 className="mb-4 text-4xl font-bold text-gray-800">
          ¡Vaya! Algo salió mal
        </h1>
        <h2 className="mb-8 text-2xl text-gray-600">Página no encontrada</h2>
        <p className="mb-8 text-xl text-gray-600">{errorMessage}</p>

        <div className="flex flex-wrap justify-center gap-4">
          <motion.button
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.95 }}
            onClick={() => navigate(-1)}
            className="rounded-lg bg-blue-500 px-6 py-3 text-white shadow-md transition-colors hover:bg-blue-600"
          >
            ← Volver atrás
          </motion.button>

          <motion.button
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.95 }}
            onClick={() => navigate('/')}
            className="rounded-lg bg-purple-500 px-6 py-3 text-white shadow-md transition-colors hover:bg-purple-600"
          >
            Ir al inicio {animalIcon}
          </motion.button>
        </div>

        {/* Animación de mascota */}
        <motion.div
          animate={{
            x: [-50, 50, -50],
            rotate: [0, 5, -5, 0]
          }}
          transition={{
            repeat: Infinity,
            duration: 8,
            ease: 'easeInOut'
          }}
          className="mt-12 text-6xl"
        >
          {animalIcon}
        </motion.div>
      </motion.div>
    </div>
  )
}

export default NotFound
