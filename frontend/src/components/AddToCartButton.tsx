import React from 'react';
import { Product } from './ProductSection';

interface AddToCartButtonProps {
  product: Product;
  className?: string;
}

const AddToCartButton: React.FC<AddToCartButtonProps> = ({ product, className }) => {
  const handleAddToCart = () => {
    // Se obtiene el carrito actual o se inicializa como array vacío
    const storedCart = JSON.parse(localStorage.getItem('cartItems') || '[]');

    // Buscar si el producto ya existe en el carrito
    const productIndex = storedCart.findIndex((item: any) => item.id === product.id);

    if (productIndex >= 0) {
      // Si ya existe, incrementar la cantidad
      storedCart[productIndex].quantity += 1;
    } else {
      // Si no existe, agregar el producto con cantidad 1
      storedCart.push({ ...product, quantity: 1 });
    }

    // Actualizar el localStorage
    localStorage.setItem('cartItems', JSON.stringify(storedCart));
    alert(`${product.name} ha sido agregado al carrito.`);
  };

  return (
    <button onClick={handleAddToCart} className={`${className} bg-green-500 text-white px-4 py-2 rounded`}>
      Agregar al carrito
    </button>
  );
};

export default AddToCartButton;

