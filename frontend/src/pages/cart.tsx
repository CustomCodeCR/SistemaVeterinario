import React, { useState, useEffect } from 'react';
import Header from '../components/Header';
import Footer from '../components/Footer';

interface CartItem {
  id: number;
  name: string;
  price: number;
  quantity: number;
  image: string;
}

const CartPage: React.FC = () => {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [paymentMethod, setPaymentMethod] = useState<string>('transferencia');

  useEffect(() => {
    // Se obtiene el carrito desde el localStorage
    const storedCart = JSON.parse(localStorage.getItem('cartItems') || '[]');
    setCartItems(storedCart);
  }, []);

  // Calcula el total a pagar
  const totalPrice = cartItems.reduce((acc, item) => acc + item.price * item.quantity, 0);

  const handlePaymentChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPaymentMethod(e.target.value);
  };

  const handleCheckout = (e: React.FormEvent) => {
    e.preventDefault();
    // Aquí se debería procesar el pago según el método seleccionado.
    alert(`Pago realizado con ${paymentMethod}. Total a pagar: $${totalPrice.toFixed(2)}`);
    // Opcionalmente, se puede vaciar el carrito:
    setCartItems([]);
    localStorage.removeItem('cartItems');
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-4xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Carrito de Compras</h2>
          {cartItems.length === 0 ? (
            <p>El carrito está vacío.</p>
          ) : (
            <div>
              <table className="w-full mb-4">
                <thead>
                  <tr>
                    <th className="text-left p-2">Producto</th>
                    <th className="text-right p-2">Precio</th>
                    <th className="text-center p-2">Cantidad</th>
                    <th className="text-right p-2">Subtotal</th>
                  </tr>
                </thead>
                <tbody>
                  {cartItems.map(item => (
                    <tr key={item.id}>
                      <td className="p-2 flex items-center">
                        {item.image && (
                          <img src={item.image} alt={item.name} className="w-12 h-12 object-cover mr-2" />
                        )}
                        {item.name}
                      </td>
                      <td className="p-2 text-right">${item.price.toFixed(2)}</td>
                      <td className="p-2 text-center">{item.quantity}</td>
                      <td className="p-2 text-right">${(item.price * item.quantity).toFixed(2)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
              <div className="text-right font-bold mb-4">
                Total: ${totalPrice.toFixed(2)}
              </div>
              <form onSubmit={handleCheckout} className="space-y-4">
                <div>
                  <h3 className="text-xl font-bold mb-2">Método de Pago</h3>
                  <div className="flex items-center mb-2">
                    <input 
                      type="radio" 
                      id="transferencia" 
                      name="paymentMethod" 
                      value="transferencia" 
                      checked={paymentMethod === 'transferencia'}
                      onChange={handlePaymentChange}
                      className="mr-2"
                    />
                    <label htmlFor="transferencia">Transferencia Bancaria</label>
                  </div>
                  <div className="flex items-center">
                    <input 
                      type="radio" 
                      id="tarjeta" 
                      name="paymentMethod" 
                      value="tarjeta" 
                      checked={paymentMethod === 'tarjeta'}
                      onChange={handlePaymentChange}
                      className="mr-2"
                    />
                    <label htmlFor="tarjeta">Tarjeta de Crédito</label>
                  </div>
                </div>
                <button type="submit" className="w-full bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                  Proceder con el Pago
                </button>
              </form>
            </div>
          )}
        </div>
      </main>
      <Footer />
    </div>
  );
};

export default CartPage;
