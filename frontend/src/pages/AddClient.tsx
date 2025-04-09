import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaUserPlus } from 'react-icons/fa';
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
  name?: string; // Para mostrar en la tabla
  email?: string; // Para mostrar en la tabla
}

const AddClient: React.FC = () => {
  const [clients, setClients] = useState<Client[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [editingClient, setEditingClient] = useState<Client | null>(null);
  const [newClient, setNewClient] = useState<Client>({
    address: '',
    phone: '',
    state: 1,
    auditCreateUser: 90 // Valor quemado
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
    (client.email?.toLowerCase().includes(search.toLowerCase())) ||
    client.phone.toLowerCase().includes(search.toLowerCase()) ||
    client.address.toLowerCase().includes(search.toLowerCase()))
  );

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setNewClient(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const payload = {
        address: newClient.address,
        phone: newClient.phone,
        state: Number(newClient.state),
        auditCreateUser: 90 // Valor quemado
      };

      if (editingClient && editingClient.userId) {
        // Actualizar cliente existente
        await axios.put(
          'https://api.vetfriends.customcodecr.com/api/v1/Client/Update',
          {
            userId: editingClient.userId,
            ...payload,
            auditUpdateUser: 90 // Valor quemado para actualización
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
          payload,
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
          text: `Teléfono: ${newClient.phone}`
        });
      }

      await fetchClients();
      setModalOpen(false);
      setEditingClient(null);
      setNewClient({
        address: '',
        phone: '',
        state: 1,
        auditCreateUser: 90 // Resetear con valor quemado
      });
    } catch (error) {
      console.error('Error al guardar cliente:', error);
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Error al guardar el cliente'
      });
    }
  };

  const handleEdit = (client: Client) => {
    setEditingClient(client);
    setNewClient({
      address: client.address,
      phone: client.phone,
      state: client.state,
      auditCreateUser: 90 // Valor quemado al editar
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
          data: { auditDeleteUser: 90 }, // Valor quemado para eliminación
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

          <div className="flex items-center justify-between mb-4">
            <input
              type="text"
              placeholder="Buscar clientes..."
              value={search}
              onChange={handleSearchChange}
              className="w-full md:w-1/2 p-2 border rounded"
            />
            <button
              className="ml-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 flex items-center"
              onClick={() => {
                setEditingClient(null);
                setNewClient({
                  address: '',
                  phone: '',
                  state: 1,
                  auditCreateUser: 90
                });
                setModalOpen(true);
              }}
            >
              <FaUserPlus className="mr-2" /> Nuevo Cliente
            </button>
          </div>

          <div className="overflow-x-auto">
            <table className="w-full border-collapse border border-gray-300">
              <thead>
                <tr className="bg-gray-200">
                  <th className="border p-2">ID</th>
                  <th className="border p-2">Teléfono</th>
                  <th className="border p-2">Dirección</th>
                  <th className="border p-2">Estado</th>
                  <th className="border p-2">Acciones</th>
                </tr>
              </thead>
              <tbody>
                {filteredClients.length > 0 ? (
                  filteredClients.map((client) => (
                    <tr key={client.userId} className="text-center border hover:bg-gray-50">
                      <td className="border p-2">{client.userId}</td>
                      <td className="border p-2">{client.phone}</td>
                      <td className="border p-2">{client.address}</td>
                      <td className="border p-2">
                        <span className={`px-2 py-1 rounded-full text-white text-sm ${client.state === 1 ? 'bg-green-500' : 'bg-red-500'}`}>
                          {client.state === 1 ? 'Activo' : 'Inactivo'}
                        </span>
                      </td>
                      <td className="border p-2 flex justify-center space-x-4">
                        <button
                          onClick={() => handleEdit(client)}
                          className="text-blue-600 hover:text-blue-800 p-1"
                          title="Editar"
                        >
                          <FaEdit />
                        </button>
                        <button
                          onClick={() => client.userId && handleDelete(client.userId)}
                          className="text-red-600 hover:text-red-800 p-1"
                          title="Eliminar"
                        >
                          <FaTrash />
                        </button>
                      </td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td colSpan={5} className="text-center p-4">No hay clientes registrados.</td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </main>
      <Footer />

      {modalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full mx-4">
            <h3 className="text-2xl font-bold text-center text-blue-600 mb-4">
              {editingClient ? 'Editar Cliente' : 'Nuevo Cliente'}
            </h3>
            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <label className="block text-gray-700 mb-1">Teléfono</label>
                <input
                  type="tel"
                  name="phone"
                  placeholder="8888-8888"
                  value={newClient.phone}
                  onChange={handleInputChange}
                  required
                  className="w-full p-2 border rounded"
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
                  className="w-full p-2 border rounded"
                />
              </div>

              <div>
                <label className="block text-gray-700 mb-1">Estado</label>
                <select
                  name="state"
                  value={newClient.state}
                  onChange={handleInputChange}
                  className="w-full p-2 border rounded"
                  required
                >
                  <option value={1}>Activo</option>
                  <option value={0}>Inactivo</option>
                </select>
              </div>

              <input type="hidden" name="auditCreateUser" value={90} />

              <div className="flex justify-between pt-4">
                <button
                  type="submit"
                  className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 flex-1 mr-2"
                >
                  {editingClient ? 'Guardar Cambios' : 'Registrar Cliente'}
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false);
                    setEditingClient(null);
                    setNewClient({
                      address: '',
                      phone: '',
                      state: 1,
                      auditCreateUser: 90
                    });
                  }}
                  className="bg-gray-400 text-white px-4 py-2 rounded hover:bg-gray-500 flex-1 ml-2"
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

export default AddClient;
