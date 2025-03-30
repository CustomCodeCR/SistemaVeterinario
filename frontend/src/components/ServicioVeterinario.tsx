import React from 'react'
import logo from '../assets/logo.png' // Asegúrate de que la ruta del logo sea correcta
import veterinarioImage from '../assets/veterinario.png' // Asegúrate de que la ruta de la imagen sea correcta
import AgendarCitaButton from './AgendarCitaButton' // Reutilizamos el botón de agendar cita

const ServicioVeterinario: React.FC = () => {
  // Función para manejar el clic en "Agendar cita"
  const handleAgendarCita = () => {
    alert('Cita agendada para Veterinario')
  }

  return (
    <section className="bg-gray-100 p-8">
      <div className="container mx-auto flex flex-col items-center justify-between md:flex-row">
        <div className="mb-8 md:mb-0 md:w-1/2">
          {/* Título y logo */}
          <div className="mb-4 flex items-center">
            <h2 className="text-center text-4xl font-extrabold text-orange-500 sm:text-4xl sm:tracking-tight lg:text-4xl">
              Veterinario
            </h2>
            <img src={logo} alt="Logo" className="size-8" />{' '}
            {/* Ajusta el tamaño del logo */}
          </div>
          {/* Descripción */}
          <p className="mb-4 text-gray-600">
            Nuestro servicio veterinario ofrece atención médica de calidad para
            tu mascota, incluyendo consultas, vacunación, cirugías y más.
            ¡Confía en nuestros expertos para el cuidado de tu mejor amigo!
          </p>
          {/* Botón para agendar cita */}
          <AgendarCitaButton onClick={handleAgendarCita} />
        </div>
        <div className="flex justify-center md:w-1/2">
          <img
            src={veterinarioImage}
            alt="Servicio de Veterinario"
            className="h-64 w-full rounded-lg object-cover shadow-md md:h-96"
          />
        </div>
      </div>
    </section>
  )
}

export default ServicioVeterinario
