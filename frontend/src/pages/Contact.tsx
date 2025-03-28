import React from 'react'
import Header from '../components/Header'
import Footer from '../components/Footer'
import { FaPhone, FaEnvelope, FaMapMarkerAlt, FaClock } from 'react-icons/fa'

const Contact = () => {
  return (
    <div className="relative overflow-hidden bg-white">
      <Header />
      <main className="min-h-screen bg-gradient-to-b from-blue-50 to-white px-4 py-12 sm:px-6 lg:px-8">
        <div className="mx-auto max-w-7xl">
          <div className="mb-12 text-center">
            <h1 className="text-4xl font-extrabold text-blue-900 sm:text-5xl sm:tracking-tight lg:text-6xl">
              Contacta a <span className="text-orange-500">VetsFriends</span>
            </h1>
            <p className="mx-auto mt-3 max-w-md text-base text-gray-500 sm:text-lg md:mt-5 md:max-w-3xl md:text-xl">
              Estamos aquí para cuidar de tu compañero peludo
            </p>
          </div>

          <div className="flex flex-col gap-12 lg:flex-row">
            {/* Formulario de Contacto - Lado izquierdo */}
            <div className="lg:w-1/2">
              <div className="rounded-xl border border-gray-100 bg-white p-8 shadow-lg">
                <h2 className="mb-6 text-2xl font-bold text-gray-800">
                  Envíanos un mensaje
                </h2>
                <form className="space-y-6">
                  <div>
                    <label
                      htmlFor="name"
                      className="mb-1 block text-sm font-medium text-gray-700"
                    >
                      Nombre completo
                    </label>
                    <input
                      type="text"
                      id="name"
                      name="name"
                      className="w-full rounded-lg border border-gray-300 px-4 py-2 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                      placeholder="Tu nombre"
                      required
                    />
                  </div>

                  <div>
                    <label
                      htmlFor="email"
                      className="mb-1 block text-sm font-medium text-gray-700"
                    >
                      Correo electrónico
                    </label>
                    <input
                      type="email"
                      id="email"
                      name="email"
                      className="w-full rounded-lg border border-gray-300 px-4 py-2 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                      placeholder="tu@email.com"
                      required
                    />
                  </div>

                  <div>
                    <label
                      htmlFor="phone"
                      className="mb-1 block text-sm font-medium text-gray-700"
                    >
                      Teléfono
                    </label>
                    <input
                      type="tel"
                      id="phone"
                      name="phone"
                      className="w-full rounded-lg border border-gray-300 px-4 py-2 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                      placeholder="8888-8888"
                    />
                  </div>

                  <div>
                    <label
                      htmlFor="subject"
                      className="mb-1 block text-sm font-medium text-gray-700"
                    >
                      Asunto
                    </label>
                    <select
                      id="subject"
                      name="subject"
                      className="w-full rounded-lg border border-gray-300 px-4 py-2 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                    >
                      <option value="">Seleccione un asunto</option>
                      <option value="cita">Solicitar cita</option>
                      <option value="emergencia">Emergencia</option>
                      <option value="consulta">Consulta general</option>
                      <option value="servicios">
                        Información de servicios
                      </option>
                    </select>
                  </div>

                  <div>
                    <label
                      htmlFor="message"
                      className="mb-1 block text-sm font-medium text-gray-700"
                    >
                      Mensaje
                    </label>
                    <textarea
                      id="message"
                      name="message"
                      rows={4}
                      className="w-full rounded-lg border border-gray-300 px-4 py-2 focus:border-blue-500 focus:ring-2 focus:ring-blue-500"
                      placeholder="Escribe tu mensaje aquí..."
                      required
                    ></textarea>
                  </div>

                  <button
                    type="submit"
                    className="w-full rounded-lg bg-blue-600 px-4 py-3 font-medium text-white transition duration-200 hover:bg-blue-700"
                  >
                    Enviar mensaje
                  </button>
                </form>
              </div>
            </div>

            {/* Información de Contacto y Mapa - Lado derecho */}
            <div className="space-y-8 lg:w-1/2">
              <div className="rounded-xl border border-gray-100 bg-white p-8 shadow-lg">
                <h2 className="mb-6 text-2xl font-bold text-gray-800">
                  Información de contacto
                </h2>

                <div className="space-y-5">
                  <div className="flex items-start">
                    <div className="shrink-0 rounded-full bg-blue-100 p-3">
                      <FaMapMarkerAlt className="text-xl text-blue-600" />
                    </div>
                    <div className="ml-4">
                      <h3 className="text-lg font-semibold text-gray-800">
                        Dirección
                      </h3>
                      <p className="text-gray-600">
                        Plaza Momentum Pinares, San José, Costa Rica
                      </p>
                      <p className="mt-1 text-sm text-gray-500">
                        Segundo piso, local #25
                      </p>
                    </div>
                  </div>

                  <div className="flex items-start">
                    <div className="shrink-0 rounded-full bg-orange-100 p-3">
                      <FaPhone className="text-xl text-orange-600" />
                    </div>
                    <div className="ml-4">
                      <h3 className="text-lg font-semibold text-gray-800">
                        Teléfonos
                      </h3>
                      <p className="text-gray-600">
                        Consultas: (506) 2222-2222
                      </p>
                      <p className="text-gray-600">
                        Emergencias: (506) 8888-8888
                      </p>
                    </div>
                  </div>

                  <div className="flex items-start">
                    <div className="shrink-0 rounded-full bg-green-100 p-3">
                      <FaEnvelope className="text-xl text-green-600" />
                    </div>
                    <div className="ml-4">
                      <h3 className="text-lg font-semibold text-gray-800">
                        Correo electrónico
                      </h3>
                      <p className="text-gray-600">info@vetsfriends.com</p>
                      <p className="text-gray-600">
                        emergencias@vetsfriends.com
                      </p>
                    </div>
                  </div>

                  <div className="flex items-start">
                    <div className="shrink-0 rounded-full bg-purple-100 p-3">
                      <FaClock className="text-xl text-purple-600" />
                    </div>
                    <div className="ml-4">
                      <h3 className="text-lg font-semibold text-gray-800">
                        Horario de atención
                      </h3>
                      <p className="text-gray-600">
                        Lunes a Viernes: 8:00 am - 7:00 pm
                      </p>
                      <p className="text-gray-600">
                        Sábados: 9:00 am - 4:00 pm
                      </p>
                      <p className="text-gray-600">Emergencias: 24/7</p>
                    </div>
                  </div>
                </div>
              </div>

              {/* Mapa de Google Maps */}
              <div className="rounded-xl border border-gray-100 bg-white p-4 shadow-lg">
                <h3 className="mb-4 text-xl font-semibold text-gray-800">
                  Nuestra ubicación
                </h3>
                <div className="aspect-w-16 aspect-h-9 overflow-hidden rounded-lg">
                  <iframe
                    src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3929.982734928028!2d-84.0368569247157!3d9.935290573197952!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8fa0e3aa66f5a5a7%3A0x5a8e4a9a9a9a9a9a!2sPlaza%20Momentum%20Pinares!5e0!3m2!1sen!2scr!4v1620000000000!5m2!1sen!2scr"
                    width="100%"
                    height="400"
                    style={{ border: 0 }}
                    allowFullScreen
                    loading="lazy"
                    title="Ubicación de VetsFriends en Plaza Momentum Pinares"
                  ></iframe>
                </div>
              </div>
            </div>
          </div>
        </div>
      </main>
      <Footer />
    </div>
  )
}

export default Contact
