import React from 'react';

interface AddToCartButtonProps {
  product: {
    id: number;
    name: string;
    price: number;
    image: string;
    category?: string;
  };
  className?: string;
  onAddToCart: (product: any) => void;
}

const AddToCartButton: React.FC<AddToCartButtonProps> = ({ product, className, onAddToCart }) => {
  return (
    <button
      onClick={() => onAddToCart(product)}
      className={`flex items-center justify-center rounded-md bg-orange-500 px-3 py-2 text-sm font-medium text-white hover:bg-orange-600 focus:outline-none focus:ring-2 focus:ring-orange-500 focus:ring-offset-2 ${className}`}
    >
      <svg
        xmlns="http://www.w3.org/2000/svg"
        className="mr-1 h-4 w-4"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth={2}
          d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"
        />
      </svg>
      Agregar
    </button>
  );
};

export default AddToCartButton;
