import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';
import Header from '../components/Header';
import Footer from '../components/Footer';

const AdminProducts: React.FC = () => {
  const navigate = useNavigate();
  const [products, setProducts] = useState<{ id: number; name: string; price: number; category: string }[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<{ id: number; name: string; price: string; category: string } | null>(null);
  const [newProduct, setNewProduct] = useState({ name: '', price: '', category: '' });

  useEffect(() => {
    const storedProducts = JSON.parse(localStorage.getItem('productos') || '[]');
    setProducts(storedProducts);
  }, []);

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const filteredProducts = products.filter(
    (product) =>
      product.name.toLowerCase().includes(search.toLowerCase()) ||
      product.category.toLowerCase().includes(search.toLowerCase())
  );

  const handleNewProductChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewProduct({ ...newProduct, [e.target.name]: e.target.value });
  };

  const handleAddOrUpdateProduct = (e: React.FormEvent) => {
    e.preventDefault();

    let updatedProducts;
    if (editingProduct) {
      // Editar producto existente
      updatedProducts = products.map((p) =>
        p.id === editingProduct.id ? { ...editingProduct, price: parseFloat(editingProduct.price) } : p
      );
    } else {
      // Agregar nuevo producto
      const storedProducts = JSON.parse(localStorage.getItem('productos') || '[]');
      const newEntry = { ...newProduct, id: storedProducts.length + 1, price: parseFloat(newProduct.price) };
      updatedProducts = [...storedProducts, newEntry];
    }

    localStorage.setItem('productos', JSON.stringify(updatedProducts));
    setProducts(updatedProducts);
    setModalOpen(false);
    setEditingProduct(null);
  };

  const handleEdit = (product: { id: number; name: string; price: number; category: string }) => {
    setEditingProduct({ ...product, price: product.price.toString() });
    setModalOpen(true);
  };

  const handleDelete = (id: number) => {
    const updatedProducts = products.filter((product) => product.id !== id);
    localStorage.setItem('productos', JSON.stringify(updatedProducts));
    setProducts(updatedProducts);
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Productos</h2>

          {/* Buscador */}
          <div className="flex items-center justify-between mb-4">
            <input
              type="text"
              placeholder="Buscar productos..."
              value={search}
              onChange={handleSearchChange}
              className="w-full md:w-1/2 p-2 border rounded"
            />
            <button
              className="ml-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
              onClick={() => {
                setEditingProduct(null);
                setModalOpen(true);
              }}
            >
              + Nuevo Producto
            </button>
          </div>

          {/* Tabla de productos */}
          <table className="w-full border-collapse border border-gray-300">
            <thead>
              <tr className="bg-gray-200">
                <th className="border p-2">ID</th>
                <th className="border p-2">Nombre</th>
                <th className="border p-2">Precio</th>
                <th className="border p-2">Categoría</th>
                <th className="border p-2">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {filteredProducts.map((product) => (
                <tr key={product.id} className="text-center border">
                  <td className="border p-2">{product.id}</td>
                  <td className="border p-2">{product.name}</td>
                  <td className="border p-2">${product.price.toFixed(2)}</td>
                  <td className="border p-2">{product.category}</td>
                  <td className="border p-2 flex justify-center space-x-4">
                    <button onClick={() => handleEdit(product)} className="text-blue-600 hover:text-blue-800">
                      <FaEdit />
                    </button>
                    <button onClick={() => handleDelete(product.id)} className="text-red-600 hover:text-red-800">
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
              {filteredProducts.length === 0 && (
                <tr>
                  <td colSpan={5} className="text-center p-4">No hay productos encontrados.</td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </main>
      <Footer />

      {/* Modal para agregar o editar producto */}
      {modalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full">
            <h3 className="text-2xl font-bold text-center text-blue-600">
              {editingProduct ? 'Editar Producto' : 'Agregar Nuevo Producto'}
            </h3>
            <form onSubmit={handleAddOrUpdateProduct} className="mt-4 space-y-4">
              <input
                type="text"
                name="name"
                placeholder="Nombre del producto"
                value={editingProduct ? editingProduct.name : newProduct.name}
                onChange={(e) =>
                  editingProduct
                    ? setEditingProduct({ ...editingProduct, name: e.target.value })
                    : setNewProduct({ ...newProduct, name: e.target.value })
                }
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="number"
                name="price"
                placeholder="Precio"
                value={editingProduct ? editingProduct.price : newProduct.price}
                onChange={(e) =>
                  editingProduct
                    ? setEditingProduct({ ...editingProduct, price: e.target.value })
                    : setNewProduct({ ...newProduct, price: e.target.value })
                }
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="category"
                placeholder="Categoría"
                value={editingProduct ? editingProduct.category : newProduct.category}
                onChange={(e) =>
                  editingProduct
                    ? setEditingProduct({ ...editingProduct, category: e.target.value })
                    : setNewProduct({ ...newProduct, category: e.target.value })
                }
                required
                className="w-full p-2 border rounded"
              />
              <div className="flex justify-between">
                <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                  {editingProduct ? 'Guardar Cambios' : 'Agregar'}
                </button>
                <button type="button" onClick={() => setModalOpen(false)} className="bg-gray-400 text-white px-4 py-2 rounded hover:bg-gray-500">
                  Cancelar
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default AdminProducts;



