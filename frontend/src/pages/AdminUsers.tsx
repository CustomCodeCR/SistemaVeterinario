import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';

const AdminUsers: React.FC = () => {
  const [users, setUsers] = useState<{ id: number; name: string; email: string; role: string }[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [editingUser, setEditingUser] = useState<{ id: number; name: string; email: string; role: string } | null>(null);
  const [newUser, setNewUser] = useState({ name: '', email: '', role: '' });

  useEffect(() => {
    const storedUsers = JSON.parse(localStorage.getItem('usuarios') || '[]');
    setUsers(storedUsers);
  }, []);

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const filteredUsers = users.filter(
    (user) =>
      user.name.toLowerCase().includes(search.toLowerCase()) ||
      user.role.toLowerCase().includes(search.toLowerCase())
  );

  const handleNewUserChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewUser({ ...newUser, [e.target.name]: e.target.value });
  };

  const handleAddOrUpdateUser = (e: React.FormEvent) => {
    e.preventDefault();

    let updatedUsers;
    if (editingUser) {
      // Editar usuario existente
      updatedUsers = users.map((u) =>
        u.id === editingUser.id ? { ...editingUser } : u
      );
    } else {
      // Agregar nuevo usuario
      const storedUsers = JSON.parse(localStorage.getItem('usuarios') || '[]');
      const newEntry = { ...newUser, id: storedUsers.length + 1 };
      updatedUsers = [...storedUsers, newEntry];
    }

    localStorage.setItem('usuarios', JSON.stringify(updatedUsers));
    setUsers(updatedUsers);
    setModalOpen(false);
    setEditingUser(null);
    setNewUser({ name: '', email: '', role: '' }); // Limpia el formulario
  };

  const handleEdit = (user: { id: number; name: string; email: string; role: string }) => {
    setEditingUser(user);
    setNewUser(user); // Rellena el formulario con los datos del usuario editado
    setModalOpen(true);
  };

  const handleDelete = (id: number) => {
    const updatedUsers = users.filter((user) => user.id !== id);
    localStorage.setItem('usuarios', JSON.stringify(updatedUsers));
    setUsers(updatedUsers);
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Usuarios</h2>

          {/* Buscador */}
          <div className="flex items-center justify-between mb-4">
            <input
              type="text"
              placeholder="Buscar usuarios..."
              value={search}
              onChange={handleSearchChange}
              className="w-full md:w-1/2 p-2 border rounded"
            />
            <button
              className="ml-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
              onClick={() => {
                setEditingUser(null);
                setNewUser({ name: '', email: '', role: '' }); // Limpia el formulario
                setModalOpen(true);
              }}
            >
              + Nuevo Usuario
            </button>
          </div>

          {/* Tabla de usuarios */}
          <table className="w-full border-collapse border border-gray-300">
            <thead>
              <tr className="bg-gray-200">
                <th className="border p-2">ID</th>
                <th className="border p-2">Nombre</th>
                <th className="border p-2">Email</th>
                <th className="border p-2">Rol</th>
                <th className="border p-2">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {filteredUsers.map((user) => (
                <tr key={user.id} className="text-center border">
                  <td className="border p-2">{user.id}</td>
                  <td className="border p-2">{user.name}</td>
                  <td className="border p-2">{user.email}</td>
                  <td className="border p-2">{user.role}</td>
                  <td className="border p-2 flex justify-center space-x-4">
                    <button onClick={() => handleEdit(user)} className="text-blue-600 hover:text-blue-800">
                      <FaEdit />
                    </button>
                    <button onClick={() => handleDelete(user.id)} className="text-red-600 hover:text-red-800">
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
              {filteredUsers.length === 0 && (
                <tr>
                  <td colSpan={5} className="text-center p-4">No hay usuarios encontrados.</td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </main>
      <Footer />

      {/* Modal para agregar o editar usuario */}
      {modalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full">
            <h3 className="text-2xl font-bold text-center text-blue-600">
              {editingUser ? 'Editar Usuario' : 'Agregar Nuevo Usuario'}
            </h3>
            <form onSubmit={handleAddOrUpdateUser} className="mt-4 space-y-4">
              <input
                type="text"
                name="name"
                placeholder="Nombre del usuario"
                value={newUser.name}
                onChange={handleNewUserChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="email"
                name="email"
                placeholder="Email"
                value={newUser.email}
                onChange={handleNewUserChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="role"
                placeholder="Rol (Admin, Usuario, Recepcionista, Veterinario)"
                value={newUser.role}
                onChange={handleNewUserChange}
                required
                className="w-full p-2 border rounded"
              />
              <div className="flex justify-between">
                <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                  {editingUser ? 'Guardar Cambios' : 'Agregar'}
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false);
                    setEditingUser(null);
                    setNewUser({ name: '', email: '', role: '' }); // Limpia el formulario al cerrar
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

export default AdminUsers;
