import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { FaEdit, FaTrash } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';

interface Category {
  categoryId?: number;
  categoryName: string;
  description: string;
  state: number;
  auditCreateUser: number;
}

const AdminCategories: React.FC = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingCategory, setEditingCategory] = useState<Category | null>(null);
  const [newCategory, setNewCategory] = useState<Category>({
    categoryName: '',
    description: '',
    state: 1,
    auditCreateUser: 1,
  });
  const [users, setUsers] = useState<{ userId: number; name: string }[]>([]);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const res = await axios.get('https://api.vetfriends.customcodecr.com/api/v1/User');
        if (res.data && Array.isArray(res.data.data)) {
          setUsers(res.data.data);
        }
      } catch (err) {
        console.error('Error al cargar usuarios:', err);
      }
    };

    fetchUsers();
  }, []);

  const handleNewCategoryChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setNewCategory((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const payload = {
        ...newCategory,
        state: Number(newCategory.state),
        auditCreateUser: Number(newCategory.auditCreateUser),
      };

      const res = await axios.post(
        'https://api.vetfriends.customcodecr.com/api/v1/ProductCategory/Create',
        payload,
        {
          headers: {
            'Content-Type': 'application/json',
            Accept: '*/*',
          },
        }
      );

      if (res.data.isSuccess) {
        const createdCategory = { ...newCategory };
        setCategories([...categories, createdCategory]);
        setModalOpen(false);
        setNewCategory({
          categoryName: '',
          description: '',
          state: 1,
          auditCreateUser: 1,
        });
      } else {
        alert('Error al crear categoría: ' + res.data.message);
      }
    } catch (err) {
      console.error(err);
      alert('Error al enviar la solicitud.');
    }
  };

  const handleEdit = (category: Category) => {
    setEditingCategory(category);
    setNewCategory(category);
    setModalOpen(true);
  };

  const handleDelete = (id: number) => {
    const updated = categories.filter((cat) => cat.categoryId !== id);
    setCategories(updated);
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Categorías</h2>

          <div className="flex justify-end mb-4">
            <button
              className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
              onClick={() => {
                setEditingCategory(null);
                setNewCategory({ categoryName: '', description: '', state: 1, auditCreateUser: 1 });
                setModalOpen(true);
              }}
            >
              + Nueva Categoría
            </button>
          </div>

          <table className="w-full border-collapse border border-gray-300">
            <thead>
              <tr className="bg-gray-200">
                <th className="border p-2">Nombre</th>
                <th className="border p-2">Descripción</th>
                <th className="border p-2">Estado</th>
                <th className="border p-2">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {categories.map((category, index) => (
                <tr key={index} className="text-center border">
                  <td className="border p-2">{category.categoryName}</td>
                  <td className="border p-2">{category.description}</td>
                  <td className="border p-2">{category.state === 1 ? 'Activo' : 'Inactivo'}</td>
                  <td className="border p-2 flex justify-center space-x-4">
                    <button
                      onClick={() => handleEdit(category)}
                      className="text-blue-600 hover:text-blue-800"
                    >
                      <FaEdit />
                    </button>
                    <button
                      onClick={() => handleDelete(index)}
                      className="text-red-600 hover:text-red-800"
                    >
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
              {categories.length === 0 && (
                <tr>
                  <td colSpan={4} className="text-center p-4">
                    No hay categorías registradas.
                  </td>
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
              {editingCategory ? 'Editar Categoría' : 'Nueva Categoría'}
            </h3>
            <form onSubmit={handleSubmit} className="mt-4 space-y-4">
              <input
                type="text"
                name="categoryName"
                placeholder="Nombre"
                value={newCategory.categoryName}
                onChange={handleNewCategoryChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="description"
                placeholder="Descripción"
                value={newCategory.description}
                onChange={handleNewCategoryChange}
                className="w-full p-2 border rounded"
              />
              <select
                name="state"
                value={newCategory.state}
                onChange={handleNewCategoryChange}
                className="w-full p-2 border rounded"
              >
                <option value={1}>Activo</option>
                <option value={0}>Inactivo</option>
              </select>
              <select
                name="auditCreateUser"
                value={newCategory.auditCreateUser}
                onChange={handleNewCategoryChange}
                className="w-full p-2 border rounded"
              >
                <option value="">Seleccione un usuario</option>
                {users.map((user) => (
                  <option key={user.userId} value={user.userId}>
                    {user.name}
                  </option>
                ))}
              </select>
              <div className="flex justify-between">
                <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                  {editingCategory ? 'Guardar Cambios' : 'Crear'}
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false);
                    setEditingCategory(null);
                    setNewCategory({ categoryName: '', description: '', state: 1, auditCreateUser: 1 });
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

export default AdminCategories;
