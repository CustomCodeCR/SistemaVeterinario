import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaUserPlus, FaSearch } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';
import axios from 'axios';
import Swal from 'sweetalert2';

interface Client {
  userId?: number;
  address: string;
  phone: string;
  state: number;
  auditCreateUser: number;
  name?: string;
  email?: string;
}

const AddClient: React.FC = () => {
  const [clients, setClients] = useState<Client[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [editingClient, setEditingClient] = useState<Client | null>(null);
  const [newClient, setNewClient] = useState<Client>({
    userId: 0,
    address: '',
    phone: '',
    state: 1,
    auditCreateUser: 0
  });

  useEffect(() => {
    fetchClients();
  }, []);

  const fetchClients = async () => {
    try {
      const res = await axios.get('https://api.vetfriends.customcodecr.com/api/v1/Client', {
        headers: {
          'Accept': '*/*'
        }
      });

      // Adaptar la respuesta según la estructura que devuelve tu API
      setClients(res.data.data || res.data || []);
    } catch (error) {
      console.error('Error cargando clientes:', error);
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Error al cargar los clientes'
      });
    }
  };

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const filteredClients = clients.filter(client =>
    (client.name?.toLowerCase().includes(search.toLowerCase()) ||
    client.email?.toLowerCase().includes(search.toLowerCase()) ||
    client.phone.toLowerCase().includes(search.toLowerCase()) ||
    client.address.toLowerCase().includes(search.toLowerCase()) ||
    client.userId?.toString().includes(search))
  );

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setNewClient(prev => ({
      ...prev,
      [name]: name === 'userId' || name === 'state' || name === 'auditCreateUser'
        ? Number(value)
        : value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      if (editingClient && editingClient.userId) {
        // Actualizar cliente existente
        await axios.put(
          'https://api.vetfriends.customcodecr.com/api/v1/Client/Update',
          {
            userId: newClient.userId,
            address: newClient.address,
            phone: newClient.phone,
            state: newClient.state,
            auditUpdateUser: newClient.auditCreateUser
          },
          {
            headers: {
              'Content-Type': 'application/json',
              'Accept': '*/*'
            }
          }
        );
        Swal.fire({
          icon: 'success',
          title: 'Cliente actualizado',
          showConfirmButton: false,
          timer: 1500
        });
      } else {
        // Crear nuevo cliente
        await axios.post(
          'https://api.vetfriends.customcodecr.com/api/v1/Client/Create',
          {
            userId: newClient.userId,
            address: newClient.address,
            phone: newClient.phone,
            state: newClient.state,
            auditCreateUser: newClient.auditCreateUser
          },
          {
            headers: {
              'Content-Type': 'application/json',
              'Accept': '*/*'
            }
          }
        );
        Swal.fire({
          icon: 'success',
          title: 'Cliente creado',
          text: `ID: ${newClient.userId} - Teléfono: ${newClient.phone}`
        });
      }

      await fetchClients();
      setModalOpen(false);
      setEditingClient(null);
      setNewClient({
        userId: 0,
        address: '',
        phone: '',
        state: 1,
        auditCreateUser: 0
      });
    } catch (error) {
      console.error('Error al guardar cliente:', error);
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: axios.isAxiosError(error) && error.response?.data?.message
          ? error.response.data.message
          : 'Error al guardar el cliente'
      });
    }
  };

  const handleEdit = (client: Client) => {
    setEditingClient(client);
    setNewClient({
      userId: client.userId || 0,
      address: client.address,
      phone: client.phone,
      state: client.state,
      auditCreateUser: client.auditCreateUser || 0
    });
    setModalOpen(true);
  };

  const handleDelete = async (userId: number) => {
    const result = await Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción eliminará el cliente permanentemente.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    });

    if (!result.isConfirmed) return;

    try {
      await axios.delete(
        `https://api.vetfriends.customcodecr.com/api/v1/Client/Delete/${userId}`,
        {
          data: { auditDeleteUser: newClient.auditCreateUser || 0 },
          headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*'
          }
        }
      );
      await fetchClients();
      Swal.fire({
        icon: 'success',
        title: 'Cliente eliminado',
        showConfirmButton: false,
        timer: 1500
      });
    } catch (error) {
      console.error('Error al eliminar cliente:', error);
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Error al eliminar el cliente'
      });
    }
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">
            Gestión de Clientes
          </h2>

          <div className="flex flex-col md:flex-row items-center justify-between mb-6 gap-4">
            <div className="relative w-full md:w-1/2">
              <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                <FaSearch className="text-gray-400" />
              </div>
              <input
                type="text"
                placeholder="Buscar por ID, teléfono, dirección..."
                value={search}
                onChange={handleSearchChange}
                className="w-full pl-10 p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
              />
            </div>
            <button
              className="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
              onClick={() => {
                setEditingClient(null);
                setNewClient({
                  userId: 0,
                  address: '',
                  phone: '',
                  state: 1,
                  auditCreateUser: 0
                });
                setModalOpen(true);
              }}
            >
              <FaUserPlus /> Nuevo Cliente
            </button>
          </div>

          <div className="overflow-x-auto">
            <table className="w-full border-collapse">
              <thead>
                <tr className="bg-gray-200 text-left">
                  <th className="p-3 border border-gray-300">ID Usuario</th>
                  <th className="p-3 border border-gray-300">Teléfono</th>
                  <th className="p-3 border border-gray-300">Dirección</th>
                  <th className="p-3 border border-gray-300">Estado</th>
                  <th className="p-3 border border-gray-300">Acciones</th>
                </tr>
              </thead>
              <tbody>
                {filteredClients.length > 0 ? (
                  filteredClients.map((client) => (
                    <tr key={client.userId} className="border-b border-gray-300 hover:bg-gray-50">
                      <td className="p-3 border border-gray-300">{client.userId}</td>
                      <td className="p-3 border border-gray-300">{client.phone}</td>
                      <td className="p-3 border border-gray-300">{client.address}</td>
                      <td className="p-3 border border-gray-300">
                        <span className={`px-2 py-1 rounded-full text-xs ${
                          client.state === 1 ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'
                        }`}>
                          {client.state === 1 ? 'Activo' : 'Inactivo'}
                        </span>
                      </td>
                      <td className="p-3 border border-gray-300">
                        <div className="flex gap-2">
                          <button
                            onClick={() => handleEdit(client)}
                            className="text-blue-600 hover:text-blue-800 p-1 rounded hover:bg-blue-50"
                            title="Editar"
                          >
                            <FaEdit />
                          </button>
                          <button
                            onClick={() => client.userId && handleDelete(client.userId)}
                            className="text-red-600 hover:text-red-800 p-1 rounded hover:bg-red-50"
                            title="Eliminar"
                          >
                            <FaTrash />
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td colSpan={5} className="p-4 text-center text-gray-500">
                      No se encontraron clientes
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </main>
      <Footer />

      {modalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
          <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-md">
            <div className="flex justify-between items-center mb-4">
              <h3 className="text-2xl font-bold text-blue-600">
                {editingClient ? 'Editar Cliente' : 'Nuevo Cliente'}
              </h3>
              <button
                onClick={() => {
                  setModalOpen(false);
                  setEditingClient(null);
                }}
                className="text-gray-500 hover:text-gray-700"
              >
                ✕
              </button>
            </div>

            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <label className="block text-gray-700 mb-1">ID de Usuario</label>
                <input
                  type="number"
                  name="userId"
                  placeholder="ID del usuario"
                  value={newClient.userId || ''}
                  onChange={handleInputChange}
                  required
                  min="1"
                  className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
              </div>

              <div>
                <label className="block text-gray-700 mb-1">Teléfono</label>
                <input
                  type="tel"
                  name="phone"
                  placeholder="Ej: 8888-8888"
                  value={newClient.phone}
                  onChange={handleInputChange}
                  required
                  className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
              </div>

              <div>
                <label className="block text-gray-700 mb-1">Dirección</label>
                <input
                  type="text"
                  name="address"
                  placeholder="Dirección completa"
                  value={newClient.address}
                  onChange={handleInputChange}
                  required
                  className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
              </div>

              <div>
                <label className="block text-gray-700 mb-1">Estado</label>
                <select
                  name="state"
                  value={newClient.state}
                  onChange={handleInputChange}
                  className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  required
                >
                  <option value={1}>Activo</option>
                  <option value={0}>Inactivo</option>
                </select>
              </div>

              <div>
                <label className="block text-gray-700 mb-1">ID Usuario Auditoría</label>
                <input
                  type="number"
                  name="auditCreateUser"
                  placeholder="ID del usuario que registra"
                  value={newClient.auditCreateUser || ''}
                  onChange={handleInputChange}
                  required
                  min="1"
                  className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
              </div>

              <div className="flex justify-end gap-3 pt-4">
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false);
                    setEditingClient(null);
                  }}
                  className="px-4 py-2 border border-gray-300 rounded text-gray-700 hover:bg-gray-100"
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                >
                  {editingClient ? 'Guardar Cambios' : 'Registrar Cliente'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default AddClient;
