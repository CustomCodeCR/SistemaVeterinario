import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';
import axios from 'axios';
import Swal from 'sweetalert2'; // SweetAlert2 agregado

const AdminUsers: React.FC = () => {
  const [users, setUsers] = useState<any[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [editingUser, setEditingUser] = useState<any | null>(null);
  const [newUser, setNewUser] = useState({
    firstName: '',
    lastName: '',
    username: '',
    password: '',
    email: '',
    userType: 'User',
    state: 1
  });

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await axios.get("https://api.vetfriends.customcodecr.com/api/v1/User");
      setUsers(response.data.data);
    } catch (err) {
      console.error("Error fetching users:", err);
    }
  };

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const handleNewUserChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setNewUser({ ...newUser, [name]: name === 'state' ? parseInt(value) : value });
  };

  const handleAddOrUpdateUser = async (e: React.FormEvent) => {
    e.preventDefault();

    const { firstName, lastName, username, password, email } = newUser;
    if (!firstName || !lastName || !username || (!editingUser && !password) || !email) {
      Swal.fire({
        icon: 'warning',
        title: 'Campos incompletos',
        text: 'Por favor completa todos los campos requeridos.',
      });
      return;
    }

    try {
      const payload = {
        firstName: newUser.firstName,
        lastName: newUser.lastName,
        username: newUser.username,
        password: newUser.password,
        email: newUser.email,
        userType: newUser.userType,
        state: newUser.state,
        auditCreateUser: 1
      };

      if (editingUser) {
        await axios.put("https://api.vetfriends.customcodecr.com/api/v1/User/Update", {
          userId: editingUser.userId,
          ...payload,
          auditUpdateUser: 1
        });

        Swal.fire({
          icon: 'success',
          title: 'Usuario actualizado',
          showConfirmButton: false,
          timer: 1500
        });
      } else {
        await axios.post("https://api.vetfriends.customcodecr.com/api/v1/User/Create", payload);

        Swal.fire({
          icon: 'success',
          title: 'Usuario creado correctamente',
          text: `Nombre: ${newUser.firstName} ${newUser.lastName}\nRol: ${newUser.userType}`,
        });
      }

      fetchUsers();
      setModalOpen(false);
      setEditingUser(null);
      resetForm();
    } catch (error: any) {
      console.error("Error saving user:", error);
      if (error.response) {
        console.log("Detalles:", error.response.data);
        Swal.fire({
          icon: 'error',
          title: 'Error del servidor',
          text: JSON.stringify(error.response.data)
        });
      }
    }
  };

  const resetForm = () => {
    setNewUser({
      firstName: '',
      lastName: '',
      username: '',
      password: '',
      email: '',
      userType: 'User',
      state: 1
    });
  };

  const handleEdit = (user: any) => {
    setEditingUser(user);
    setNewUser({
      firstName: user.firstName,
      lastName: user.lastName,
      username: user.userName,
      password: '',
      email: user.email,
      userType: user.userType,
      state: user.state
    });
    setModalOpen(true);
  };

  const handleDelete = async (id: number) => {
    const result = await Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción eliminará al usuario permanentemente.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    });

    if (!result.isConfirmed) return;

    try {
      await axios.delete(`https://api.vetfriends.customcodecr.com/api/v1/User/Delete/${id}`);
      fetchUsers();
      Swal.fire({
        icon: 'success',
        title: 'Usuario eliminado correctamente',
        showConfirmButton: false,
        timer: 1500
      });
    } catch (error: any) {
      console.error("Error deleting user:", error);
      if (error.response) {
        console.log("Detalles:", error.response.data);
        Swal.fire({
          icon: 'error',
          title: 'Error al eliminar',
          text: JSON.stringify(error.response.data)
        });
      }
    }
  };

  const filteredUsers = users.filter((user) =>
    user.firstName.toLowerCase().includes(search.toLowerCase()) ||
    user.userType.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Usuarios</h2>

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
                resetForm();
                setModalOpen(true);
              }}
            >
              + Nuevo Usuario
            </button>
          </div>

          <table className="w-full border-collapse border border-gray-300">
            <thead>
              <tr className="bg-gray-200">
                <th className="border p-2">ID</th>
                <th className="border p-2">Nombre</th>
                <th className="border p-2">Apellido</th>
                <th className="border p-2">Usuario</th>
                <th className="border p-2">Email</th>
                <th className="border p-2">Rol</th>
                <th className="border p-2">Fecha de Creación</th>
                <th className="border p-2">Estado</th>
                <th className="border p-2">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {filteredUsers.map((user) => (
                <tr key={user.userId} className="text-center border">
                  <td className="border p-2">{user.userId}</td>
                  <td className="border p-2">{user.firstName}</td>
                  <td className="border p-2">{user.lastName}</td>
                  <td className="border p-2">{user.userName}</td>
                  <td className="border p-2">{user.email}</td>
                  <td className="border p-2">{user.userType}</td>
                  <td className="border p-2">{new Date(user.auditCreateDate).toLocaleString()}</td>
                  <td className="border p-2">
                    <span className={`px-2 py-1 rounded-full text-white text-sm ${user.state === 1 ? 'bg-green-500' : 'bg-red-500'}`}>
                      {user.stateDesc || (user.state === 1 ? 'Activo' : 'Desactivado')}
                    </span>
                  </td>
                  <td className="border p-2 flex justify-center space-x-4">
                    <button onClick={() => handleEdit(user)} className="text-blue-600 hover:text-blue-800">
                      <FaEdit />
                    </button>
                    <button onClick={() => handleDelete(user.userId)} className="text-red-600 hover:text-red-800">
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
              {filteredUsers.length === 0 && (
                <tr>
                  <td colSpan={9} className="text-center p-4">No hay usuarios encontrados.</td>
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
              {editingUser ? 'Editar Usuario' : 'Agregar Nuevo Usuario'}
            </h3>
            <form onSubmit={handleAddOrUpdateUser} className="mt-4 space-y-4">
              <input
                type="text"
                name="firstName"
                placeholder="Nombre"
                value={newUser.firstName}
                onChange={handleNewUserChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="lastName"
                placeholder="Apellido"
                value={newUser.lastName}
                onChange={handleNewUserChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="username"
                placeholder="Nombre de usuario"
                value={newUser.username}
                onChange={handleNewUserChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="password"
                name="password"
                placeholder="Contraseña"
                value={newUser.password}
                onChange={handleNewUserChange}
                required={!editingUser}
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
              <select
                name="userType"
                value={newUser.userType}
                onChange={handleNewUserChange}
                required
                className="w-full p-2 border rounded"
              >
                <option value="Admin">Admin</option>
                <option value="Medic">Medic</option>
                <option value="Client">Client</option>
                <option value="User">User</option>
              </select>
              <select
                name="state"
                value={newUser.state}
                onChange={handleNewUserChange}
                required
                className="w-full p-2 border rounded"
              >
                <option value={1}>Activo</option>
                <option value={0}>Desactivado</option>
              </select>
              <div className="flex justify-between">
                <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                  {editingUser ? 'Guardar Cambios' : 'Agregar'}
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false);
                    setEditingUser(null);
                    resetForm();
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



