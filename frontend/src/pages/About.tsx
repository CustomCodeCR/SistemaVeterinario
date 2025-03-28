import React from 'react'
import Header from '../components/Header'
import Footer from '../components/Footer'
import { FaPaw, FaHeart, FaClinicMedical, FaUserFriends } from 'react-icons/fa'

const About = () => {
  return (
    <div className="relative overflow-hidden bg-white">
      <Header />
      <main className="from-grey-100 min-h-screen bg-gradient-to-b to-white px-4 py-12 sm:px-6 lg:px-8">
        <div className="mx-auto max-w-7xl">
          <div className="mb-12 text-center">
            <h1 className="text-4xl font-extrabold text-blue-900 sm:text-5xl sm:tracking-tight lg:text-6xl">
              Sobre <span className="text-orange-500">VetsFriends</span>
            </h1>
            <p className="mx-auto mt-3 max-w-md text-base text-gray-500 sm:text-lg md:mt-5 md:max-w-3xl md:text-xl">
              Donde la pasión por los animales se encuentra con la excelencia
              médica
            </p>
          </div>

          <div className="flex flex-col items-center gap-12 lg:flex-row">
            {/* Texto descriptivo */}
            <div className="space-y-6 lg:w-1/2">
              <p className="text-lg leading-relaxed text-gray-700">
                En <strong className="text-blue-600">VetsFriends</strong>, no
                somos solo una clínica veterinaria, somos una familia de amantes
                de los animales comprometidos con el bienestar integral de tus
                compañeros peludos.
              </p>

              <p className="text-lg leading-relaxed text-gray-700">
                Fundada en 2020, nuestra misión ha sido proporcionar atención
                compasiva y de alta calidad, creando un ambiente donde las
                mascotas se sientan seguras y amadas, y sus dueños encuentren
                orientación experta y comprensión.
              </p>

              <div className="grid grid-cols-1 gap-6 pt-4 md:grid-cols-2">
                <div className="flex items-start">
                  <FaHeart className="mr-3 mt-1 text-2xl text-red-500" />
                  <div>
                    <h3 className="text-xl font-semibold text-gray-800">
                      Nuestra Filosofía
                    </h3>
                    <p className="text-gray-600">
                      Cuidamos cada mascota como si fuera nuestra propia
                      familia.
                    </p>
                  </div>
                </div>

                <div className="flex items-start">
                  <FaClinicMedical className="mr-3 mt-1 text-2xl text-blue-500" />
                  <div>
                    <h3 className="text-xl font-semibold text-gray-800">
                      Equipo Experto
                    </h3>
                    <p className="text-gray-600">
                      Profesionales certificados con años de experiencia.
                    </p>
                  </div>
                </div>

                <div className="flex items-start">
                  <FaPaw className="mr-3 mt-1 text-2xl text-orange-500" />
                  <div>
                    <h3 className="text-xl font-semibold text-gray-800">
                      Enfoque Integral
                    </h3>
                    <p className="text-gray-600">
                      Desde prevención hasta tratamientos especializados.
                    </p>
                  </div>
                </div>

                <div className="flex items-start">
                  <FaUserFriends className="mr-3 mt-1 text-2xl text-green-500" />
                  <div>
                    <h3 className="text-xl font-semibold text-gray-800">
                      Comunidad
                    </h3>
                    <p className="text-gray-600">
                      Talleres y eventos para dueños de mascotas.
                    </p>
                  </div>
                </div>
              </div>

              <blockquote className="mt-8 rounded-lg border-l-4 border-blue-500 bg-blue-50 p-6">
                <p className="italic text-gray-700">
                  "En VetsFriends, cada colita que se mueve y cada ronroneo que
                  escuchamos nos recuerdan por qué amamos lo que hacemos. Tu
                  mascota no es solo un paciente, es un amigo que merece lo
                  mejor."
                </p>
                <footer className="mt-4 font-medium text-blue-600">
                  — Dr. Carlos Rodríguez, Fundador
                </footer>
              </blockquote>
            </div>

            {/* Imagen */}
            <div className="lg:w-1/2">
              <div className="relative overflow-hidden rounded-xl shadow-2xl">
                <img
                  src="https://okdiario.com/img/2023/06/16/un-estudio-revela-que-es-lo-que-mas-felices-hace-a-los-perros.jpg"
                  alt="Perro feliz en la clínica VetsFriends"
                  className="h-auto w-full object-cover"
                />
                <div className="absolute inset-x-0 bottom-0 bg-gradient-to-t from-black/70 to-transparent p-6">
                  <p className="text-lg font-medium text-white">
                    La felicidad de tus mascotas es nuestra mayor recompensa
                  </p>
                </div>
              </div>

              <div className="mt-8 rounded-lg border border-orange-100 bg-orange-50 p-6">
                <h3 className="mb-3 text-xl font-bold text-orange-800">
                  ¿Por qué elegirnos?
                </h3>
                <ul className="space-y-2 text-gray-700">
                  <li className="flex items-center">
                    <span className="mr-2 text-orange-500">✓</span>{' '}
                    Instalaciones modernas y equipamiento de última generación
                  </li>
                  <li className="flex items-center">
                    <span className="mr-2 text-orange-500">✓</span> Atención de
                    emergencia 24/7
                  </li>
                  <li className="flex items-center">
                    <span className="mr-2 text-orange-500">✓</span> Programas de
                    bienestar preventivo
                  </li>
                  <li className="flex items-center">
                    <span className="mr-2 text-orange-500">✓</span> Seguimiento
                    personalizado de cada caso
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </main>
      <Footer />
    </div>
  )
}

export default About
