import React, { useState, useEffect } from 'react';
import Header from '../components/Header';
import Footer from '../components/Footer';

interface User {
  name: string;
  email: string;
  role: string;
  id: number;
}

interface CartItem {
  id: number;
  name: string;
  price: number;
  quantity: number;
  image: string;
  productId: number;
}

interface SaleData {
  clientId: number;
  saleDate: string;
  state: number;
  auditCreateUser: number;
  saleDetail: Array<{
    productId: number;
    quantity: number;
    price: number;
  }>;
}

const CartPage: React.FC = () => {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [paymentMethod, setPaymentMethod] = useState<string>('transferencia');
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [currentUser, setCurrentUser] = useState<User | null>(null);

  useEffect(() => {
    // Obtener el carrito desde el localStorage
    const storedCart = JSON.parse(localStorage.getItem('cartItems') || '[]');
    setCartItems(storedCart);

    // Obtener el usuario autenticado desde el localStorage
    const userData = JSON.parse(localStorage.getItem('userData') || 'null');
    setCurrentUser(userData);
  }, []);

  // Calcula el total a pagar
  const totalPrice = cartItems.reduce((acc, item) => acc + item.price * item.quantity, 0);

  const handlePaymentChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPaymentMethod(e.target.value);
  };

  const updateCartInStorage = (updatedCart: CartItem[]) => {
    localStorage.setItem('cartItems', JSON.stringify(updatedCart));
    setCartItems(updatedCart);
  };

  // Función para crear una venta
  const createSale = async (saleData: SaleData) => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await fetch('https://api.vetfriends.customcodecr.com/api/v1/Sale/Create', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(saleData),
      });

      if (!response.ok) {
        throw new Error('Error al crear la venta');
      }

      const data = await response.json();
      return data;
    } catch (err) {
      setError(err.message || 'Error al procesar la venta');
      throw err;
    } finally {
      setIsLoading(false);
    }
  };

  // Función para actualizar una venta
  const updateSale = async (saleId: number, saleData: SaleData) => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await fetch(`https://api.vetfriends.customcodecr.com/api/v1/Sale/Update/${saleId}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(saleData),
      });

      if (!response.ok) {
        throw new Error('Error al actualizar la venta');
      }

      const data = await response.json();
      return data;
    } catch (err) {
      setError(err.message || 'Error al actualizar la venta');
      throw err;
    } finally {
      setIsLoading(false);
    }
  };

  // Función para eliminar una venta
  const deleteSale = async (saleId: number) => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await fetch(`https://api.vetfriends.customcodecr.com/api/v1/Sale/Delete/${saleId}`, {
        method: 'DELETE',
      });

      if (!response.ok) {
        throw new Error('Error al eliminar la venta');
      }

      return true;
    } catch (err) {
      setError(err.message || 'Error al eliminar la venta');
      throw err;
    } finally {
      setIsLoading(false);
    }
  };

  const handleCheckout = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError(null);
    setSuccess(null);

    // Verificar que hay un usuario autenticado
    if (!currentUser) {
      setError('Debe iniciar sesión para realizar una compra');
      setIsLoading(false);
      return;
    }

    try {
      // Preparar los datos para la venta
      const saleData: SaleData = {
        clientId: currentUser.id, // Usamos el ID del usuario autenticado
        saleDate: new Date().toISOString(),
        state: 1, // Estado inicial de la venta
        auditCreateUser: currentUser.id, // Usamos el mismo ID para auditCreateUser
        saleDetail: cartItems.map(item => ({
          productId: item.productId,
          quantity: item.quantity,
          price: item.price,
        })),
      };

      // Crear la venta en el backend
      const createdSale = await createSale(saleData);

      // Mostrar mensaje de éxito
      setSuccess(`Pago realizado con ${paymentMethod}. Total pagado: $${totalPrice.toFixed(2)}`);

      // Vaciar el carrito
      updateCartInStorage([]);
    } catch (err) {
      setError(err.message || 'Error al procesar el pago');
    } finally {
      setIsLoading(false);
    }
  };

  const handleUpdateQuantity = (id: number, newQuantity: number) => {
    if (newQuantity < 1) return;

    const updatedCart = cartItems.map(item =>
      item.id === id ? { ...item, quantity: newQuantity } : item
    );

    updateCartInStorage(updatedCart);
  };

  const handleRemoveItem = (id: number) => {
    const updatedCart = cartItems.filter(item => item.id !== id);
    updateCartInStorage(updatedCart);
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-4xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Carrito de Compras</h2>

          {error && (
            <div className="mb-4 p-4 bg-red-100 border border-red-400 text-red-700 rounded">
              {error}
            </div>
          )}

          {success && (
            <div className="mb-4 p-4 bg-green-100 border border-green-400 text-green-700 rounded">
              {success}
            </div>
          )}

          {cartItems.length === 0 ? (
            <p>El carrito está vacío.</p>
          ) : (
            <div>
              {!currentUser && (
                <div className="mb-4 p-4 bg-yellow-100 border border-yellow-400 text-yellow-700 rounded">
                  Debes iniciar sesión para realizar una compra
                </div>
              )}

              <table className="w-full mb-4">
                <thead>
                  <tr className="border-b">
                    <th className="text-left p-2">Producto</th>
                    <th className="text-right p-2">Precio</th>
                    <th className="text-center p-2">Cantidad</th>
                    <th className="text-right p-2">Subtotal</th>
                    <th className="text-right p-2">Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {cartItems.map(item => (
                    <tr key={item.id} className="border-b">
                      <td className="p-2 flex items-center">
                        {item.image && (
                          <img src={item.image} alt={item.name} className="w-12 h-12 object-cover mr-2" />
                        )}
                        {item.name}
                      </td>
                      <td className="p-2 text-right">${item.price.toFixed(2)}</td>
                      <td className="p-2 text-center">
                        <input
                          type="number"
                          min="1"
                          value={item.quantity}
                          onChange={(e) => handleUpdateQuantity(item.id, parseInt(e.target.value))}
                          className="w-16 text-center border rounded"
                        />
                      </td>
                      <td className="p-2 text-right">${(item.price * item.quantity).toFixed(2)}</td>
                      <td className="p-2 text-right">
                        <button
                          onClick={() => handleRemoveItem(item.id)}
                          className="text-red-500 hover:text-red-700"
                        >
                          Eliminar
                        </button>
                      </td>
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
                <button
                  type="submit"
                  className="w-full bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 disabled:bg-blue-300"
                  disabled={isLoading || !currentUser}
                >
                  {isLoading ? 'Procesando...' : 'Proceder con el Pago'}
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
