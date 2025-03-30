import React from 'react'

interface AgendarCitaButtonProps {
  onClick?: () => void // Prop opcional para manejar clics
  disabled?: boolean // Prop para deshabilitar el botón
}

const AgendarCitaButton: React.FC<AgendarCitaButtonProps> = ({
  onClick,
  disabled = false
}) => {
  return (
    <button
      className={`rounded-lg px-6 py-2 transition-colors ${
        disabled
          ? 'cursor-not-allowed bg-gray-400' // Estilo cuando está deshabilitado
          : 'bg-gray-500 text-white hover:bg-gray-600' // Estilo normal
      }`}
      onClick={onClick}
      disabled={disabled}
    >
      Agendar cita
    </button>
  )
}

export default AgendarCitaButton
