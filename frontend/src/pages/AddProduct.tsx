import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';
import axiosInstance from '../api/axiosInstance';

interface Product {
  productId: number;
  name: string;
  price: number;
  category: string;
}

const AddProduct: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);
  const [newProduct, setNewProduct] = useState({ name: '', price: '', category: '' });

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const res = await axiosInstance.get('/Product', {
        params: {
          NumFilter: 1,
          TextFilter: '',
          NumPage: 1,
          NumRecordsPage: 100
        }
      });
      setProducts(res.data.data);
    } catch (error) {
      console.error('Error cargando productos:', error);
    }
  };

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

  const handleAddOrUpdateProduct = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingProduct) {
        await axiosInstance.put('/Product/Update', {
          productId: editingProduct.productId,
          name: newProduct.name,
          price: parseFloat(newProduct.price),
          category: newProduct.category
        });
      } else {
        await axiosInstance.post('/Product/Create', {
          name: newProduct.name,
          price: parseFloat(newProduct.price),
          category: newProduct.category
        });
      }
      await fetchProducts();
      setModalOpen(false);
      setEditingProduct(null);
      setNewProduct({ name: '', price: '', category: '' });
    } catch (error) {
      console.error('Error al guardar producto:', error);
    }
  };

  const handleEdit = (product: Product) => {
    setEditingProduct(product);
    setNewProduct({
      name: product.name,
      price: product.price.toString(),
      category: product.category
    });
    setModalOpen(true);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('¿Estás seguro de eliminar este producto?')) {
      try {
        await axiosInstance.delete(`/Product/Delete/${id}`);
        await fetchProducts();
      } catch (error) {
        console.error('Error al eliminar producto:', error);
      }
    }
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Productos</h2>

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
                setNewProduct({ name: '', price: '', category: '' });
                setModalOpen(true);
              }}
            >
              + Nuevo Producto
            </button>
          </div>

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
              {filteredProducts.length > 0 ? (
                filteredProducts.map((product) => (
                  <tr key={product.productId} className="text-center border">
                    <td className="border p-2">{product.productId}</td>
                    <td className="border p-2">{product.name}</td>
                    <td className="border p-2">₡{product.price.toFixed(2)}</td>
                    <td className="border p-2">{product.category}</td>
                    <td className="border p-2 flex justify-center space-x-4">
                      <button onClick={() => handleEdit(product)} className="text-blue-600 hover:text-blue-800">
                        <FaEdit />
                      </button>
                      <button onClick={() => handleDelete(product.productId)} className="text-red-600 hover:text-red-800">
                        <FaTrash />
                      </button>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={5} className="text-center p-4">No hay productos encontrados.</td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </main>
      <Footer />

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
                value={newProduct.name}
                onChange={handleNewProductChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="number"
                name="price"
                placeholder="Precio"
                value={newProduct.price}
                onChange={handleNewProductChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="category"
                placeholder="Categoría"
                value={newProduct.category}
                onChange={handleNewProductChange}
                required
                className="w-full p-2 border rounded"
              />
              <div className="flex justify-between">
                <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                  {editingProduct ? 'Guardar Cambios' : 'Agregar'}
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false);
                    setEditingProduct(null);
                    setNewProduct({ name: '', price: '', category: '' });
                  }}
                  className="bg-gray-400 text-white px-4 py-2 rounded hover:bg-gray-500"
                >
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

export default AddProduct;





