import React from 'react'

interface AddToCartButtonProps {
  onClick?: () => void // Prop opcional para manejar clics
}

const AddToCartButton: React.FC<AddToCartButtonProps> = ({ onClick }) => {
  return (
    <button
      className="rounded-lg bg-gray-500 px-4 py-2 text-white transition-colors hover:bg-gray-600"
      onClick={onClick}
    >
      Agregar al carrito
    </button>
  )
}

export default AddToCartButton
