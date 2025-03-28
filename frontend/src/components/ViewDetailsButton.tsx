import React from 'react'

interface ViewDetailsButtonProps {
  onClick?: () => void
  className?: string
}

const ViewDetailsButton: React.FC<ViewDetailsButtonProps> = ({
  onClick,
  className = ''
}) => {
  return (
    <button
      onClick={onClick}
      className={`whitespace-nowrap rounded bg-blue-600 px-3 py-2 text-sm text-white hover:bg-blue-700 ${className}`}
    >
      Ver detalles
    </button>
  )
}

export default ViewDetailsButton
