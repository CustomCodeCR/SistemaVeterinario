import React from 'react'
import logod from '../assets/logod.png' // Asegúrate de que la ruta del logo sea correcta
import groomingImage from '../assets/grooming.png' // Asegúrate de que la ruta de la imagen sea correcta
import AgendarCitaButton from './AgendarCitaButton' // Importa el nuevo componente

const ServicioGrooming: React.FC = () => {
  // Función para manejar el clic en "Agendar cita"
  const handleAgendarCita = () => {
    alert('Cita agendada para Grooming')
  }

  return (
    <section className="bg-gray-50 p-8">
      <div className="container mx-auto flex flex-col items-center justify-between md:flex-row">
        {/* Contenido a la izquierda */}
        <div className="mb-8 md:mb-0 md:w-1/2">
          {/* Título y logo */}
          <div className="mb-4 flex items-center">
            <h2 className="text-center text-4xl font-extrabold text-blue-900 sm:text-4xl sm:tracking-tight lg:text-4xl">
              Grooming
            </h2>
            <img src={logod} alt="Logo" className="size-8" />{' '}
            {/* Ajusta el tamaño del logo */}
          </div>
          {/* Descripción */}
          <p className="mb-4 text-gray-600">
            Nuestro servicio de grooming ofrece un cuidado completo para tu
            mascota, incluyendo baño, corte de pelo, cepillado y más. ¡Deja a tu
            mascota en las mejores manos!
          </p>
          {/* Botón para agendar cita */}
          <AgendarCitaButton onClick={handleAgendarCita} />
        </div>

        {/* Foto a la derecha */}
        <div className="flex justify-center md:w-1/2">
          <img
            src={groomingImage}
            alt="Servicio de Grooming"
            className="h-64 w-full rounded-lg object-cover shadow-md md:h-96"
          />
        </div>
      </div>
    </section>
  )
}

export default ServicioGrooming
