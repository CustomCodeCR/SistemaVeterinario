import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';
import axios from 'axios';
import Swal from 'sweetalert2';

interface AppointmentDetail {
  diagnosis: string;
  treatment: string;
  observations: string;
}

interface Appointment {
  id?: number;
  appointmentDate: string;
  reason: string;
  petId: number;
  medicId: number;
  state: number;
  auditCreateUser: number;
  appointmentDetail: AppointmentDetail[];
}

const Appointments: React.FC = () => {
  const [appointments, setAppointments] = useState<Appointment[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentAppointment, setCurrentAppointment] = useState<Appointment>({
    appointmentDate: new Date().toISOString().slice(0, 16), // Formato inicial correcto
    reason: '',
    petId: 0,
    medicId: 0,
    state: 0,
    auditCreateUser: 0,
    appointmentDetail: [{ diagnosis: '', treatment: '', observations: '' }],
  });

  // Obtener citas al cargar el componente
  useEffect(() => {
    fetchAppointments();
  }, []);

  const fetchAppointments = async () => {
    try {
      const response = await axios.get('https://api.vetfriends.customcodecr.com/api/v1/Appoiment');
      setAppointments(response.data.data || []);
    } catch (error) {
      console.error('Error al obtener citas:', error);
      Swal.fire('Error', 'No se pudieron cargar las citas', 'error');
    }
  };

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const filteredAppointments = appointments.filter(appointment =>
    appointment.reason.toLowerCase().includes(search.toLowerCase()) ||
    appointment.appointmentDate.toLowerCase().includes(search.toLowerCase())
  );

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setCurrentAppointment({
      ...currentAppointment,
      [name]: name === 'petId' || name === 'medicId' || name === 'state' || name === 'auditCreateUser'
        ? parseInt(value)
        : value
    });
  };

  const handleDetailChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, index: number) => {
    const updatedDetails = [...currentAppointment.appointmentDetail];
    updatedDetails[index] = {
      ...updatedDetails[index],
      [e.target.name]: e.target.value,
    };
    setCurrentAppointment({
      ...currentAppointment,
      appointmentDetail: updatedDetails
    });
  };

  const handleAddDetail = () => {
    setCurrentAppointment({
      ...currentAppointment,
      appointmentDetail: [
        ...currentAppointment.appointmentDetail,
        { diagnosis: '', treatment: '', observations: '' }
      ]
    });
  };

  const handleRemoveDetail = (index: number) => {
    if (currentAppointment.appointmentDetail.length > 1) {
      const updatedDetails = [...currentAppointment.appointmentDetail];
      updatedDetails.splice(index, 1);
      setCurrentAppointment({
        ...currentAppointment,
        appointmentDetail: updatedDetails
      });
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    // Validación básica
    if (currentAppointment.petId <= 0 || currentAppointment.medicId <= 0 || currentAppointment.auditCreateUser <= 0) {
      Swal.fire('Error', 'Todos los IDs deben ser números positivos', 'error');
      return;
    }

    try {
      // Formatear la fecha correctamente para la API
      const formattedAppointment = {
        ...currentAppointment,
        appointmentDate: new Date(currentAppointment.appointmentDate).toISOString()
      };

      if (isEditing && currentAppointment.id) {
        // Actualizar cita existente
        await axios.put(
          `https://api.vetfriends.customcodecr.com/api/v1/Appoiment/Update/${currentAppointment.id}`,
          formattedAppointment,
          {
            headers: {
              'Content-Type': 'application/json'
            }
          }
        );
        Swal.fire('¡Éxito!', 'La cita se ha actualizado correctamente.', 'success');
      } else {
        // Crear nueva cita
        const response = await axios.post(
          'https://api.vetfriends.customcodecr.com/api/v1/Appoiment/Create',
          formattedAppointment,
          {
            headers: {
              'Content-Type': 'application/json'
            }
          }
        );

        if (response.data && response.data.success) {
          Swal.fire('¡Éxito!', 'La cita se ha creado correctamente.', 'success');
        } else {
          throw new Error(response.data?.message || 'Error al crear la cita');
        }
      }

      await fetchAppointments();
      setModalOpen(false);
      resetForm();
    } catch (error) {
      console.error('Error al guardar cita:', error);
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: error.response?.data?.message || 'No se pudo guardar la cita. Verifica los datos e intenta nuevamente.'
      });
    }
  };

  const handleEdit = (appointment: Appointment) => {
    // Convertir la fecha al formato correcto para el datetime-local input
    const formattedDate = appointment.appointmentDate.includes('T')
      ? appointment.appointmentDate.slice(0, 16)
      : new Date(appointment.appointmentDate).toISOString().slice(0, 16);

    setCurrentAppointment({
      ...appointment,
      appointmentDate: formattedDate
    });
    setIsEditing(true);
    setModalOpen(true);
  };

  const handleDelete = async (id: number) => {
    const result = await Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción no se puede deshacer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    });

    if (result.isConfirmed) {
      try {
        await axios.delete(
          `https://api.vetfriends.customcodecr.com/api/v1/Appoiment/Delete/${id}`,
          {
            headers: {
              'Content-Type': 'application/json'
            }
          }
        );
        await fetchAppointments();
        Swal.fire('Eliminado', 'La cita ha sido eliminada.', 'success');
      } catch (error) {
        console.error('Error al eliminar cita:', error);
        Swal.fire('Error', 'No se pudo eliminar la cita.', 'error');
      }
    }
  };

  const resetForm = () => {
    setCurrentAppointment({
      appointmentDate: new Date().toISOString().slice(0, 16),
      reason: '',
      petId: 0,
      medicId: 0,
      state: 0,
      auditCreateUser: 0,
      appointmentDetail: [{ diagnosis: '', treatment: '', observations: '' }],
    });
    setIsEditing(false);
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Citas</h2>

          {/* Buscador y botón */}
          <div className="flex flex-col md:flex-row items-center justify-between mb-6 gap-4">
            <input
              type="text"
              placeholder="Buscar citas por razón o fecha..."
              value={search}
              onChange={handleSearchChange}
              className="w-full md:w-1/2 p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
            />
            <button
              className="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
              onClick={() => {
                resetForm();
                setModalOpen(true);
              }}
            >
              <FaPlus /> Nueva Cita
            </button>
          </div>

          {/* Tabla de citas */}
          <div className="overflow-x-auto">
            <table className="w-full border-collapse">
              <thead>
                <tr className="bg-gray-200 text-left">
                  <th className="p-3 border border-gray-300">ID</th>
                  <th className="p-3 border border-gray-300">Fecha y Hora</th>
                  <th className="p-3 border border-gray-300">Razón</th>
                  <th className="p-3 border border-gray-300">Mascota ID</th>
                  <th className="p-3 border border-gray-300">Médico ID</th>
                  <th className="p-3 border border-gray-300">Estado</th>
                  <th className="p-3 border border-gray-300">Acciones</th>
                </tr>
              </thead>
              <tbody>
                {filteredAppointments.length > 0 ? (
                  filteredAppointments.map((appointment) => (
                    <tr key={appointment.id} className="border-b border-gray-300 hover:bg-gray-50">
                      <td className="p-3 border border-gray-300">{appointment.id}</td>
                      <td className="p-3 border border-gray-300">
                        {new Date(appointment.appointmentDate).toLocaleString()}
                      </td>
                      <td className="p-3 border border-gray-300">{appointment.reason}</td>
                      <td className="p-3 border border-gray-300">{appointment.petId}</td>
                      <td className="p-3 border border-gray-300">{appointment.medicId}</td>
                      <td className="p-3 border border-gray-300">
                        <span className={`px-2 py-1 rounded-full text-xs ${
                          appointment.state === 1 ? 'bg-green-100 text-green-800' :
                          appointment.state === 2 ? 'bg-yellow-100 text-yellow-800' :
                          'bg-red-100 text-red-800'
                        }`}>
                          {appointment.state === 1 ? 'Completada' :
                           appointment.state === 2 ? 'En proceso' : 'Pendiente'}
                        </span>
                      </td>
                      <td className="p-3 border border-gray-300">
                        <div className="flex gap-2">
                          <button
                            onClick={() => handleEdit(appointment)}
                            className="text-blue-600 hover:text-blue-800 p-1 rounded hover:bg-blue-50"
                            title="Editar"
                          >
                            <FaEdit />
                          </button>
                          <button
                            onClick={() => appointment.id && handleDelete(appointment.id)}
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
                    <td colSpan={7} className="p-4 text-center text-gray-500">
                      No se encontraron citas
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </main>
      <Footer />

      {/* Modal para agregar/editar citas */}
      {modalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
          <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-2xl max-h-[90vh] overflow-y-auto">
            <div className="flex justify-between items-center mb-4">
              <h3 className="text-2xl font-bold text-blue-600">
                {isEditing ? 'Editar Cita' : 'Nueva Cita'}
              </h3>
              <button
                onClick={() => {
                  setModalOpen(false);
                  resetForm();
                }}
                className="text-gray-500 hover:text-gray-700"
              >
                ✕
              </button>
            </div>

            <form onSubmit={handleSubmit} className="space-y-4">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="block text-gray-700 mb-1">Fecha y Hora</label>
                  <input
                    type="datetime-local"
                    name="appointmentDate"
                    value={currentAppointment.appointmentDate}
                    onChange={handleInputChange}
                    required
                    className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-gray-700 mb-1">Razón</label>
                  <input
                    type="text"
                    name="reason"
                    placeholder="Razón de la cita"
                    value={currentAppointment.reason}
                    onChange={handleInputChange}
                    required
                    className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-gray-700 mb-1">ID de Mascota</label>
                  <input
                    type="number"
                    name="petId"
                    placeholder="ID de la mascota"
                    value={currentAppointment.petId || ''}
                    onChange={handleInputChange}
                    required
                    min="1"
                    className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-gray-700 mb-1">ID de Médico</label>
                  <input
                    type="number"
                    name="medicId"
                    placeholder="ID del médico"
                    value={currentAppointment.medicId || ''}
                    onChange={handleInputChange}
                    required
                    min="1"
                    className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-gray-700 mb-1">Estado</label>
                  <select
                    name="state"
                    value={currentAppointment.state}
                    onChange={(e) => setCurrentAppointment({
                      ...currentAppointment,
                      state: parseInt(e.target.value)
                    })}
                    className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  >
                    <option value="0">Pendiente</option>
                    <option value="1">Completada</option>
                    <option value="2">En proceso</option>
                  </select>
                </div>

                <div>
                  <label className="block text-gray-700 mb-1">ID Usuario Auditoría</label>
                  <input
                    type="number"
                    name="auditCreateUser"
                    placeholder="ID del usuario que registra"
                    value={currentAppointment.auditCreateUser || ''}
                    onChange={handleInputChange}
                    required
                    min="1"
                    className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  />
                </div>
              </div>

              {/* Detalles de la cita */}
              <div className="mt-6">
                <div className="flex justify-between items-center mb-2">
                  <h4 className="text-lg font-semibold text-gray-700">Detalles de la Cita</h4>
                  <button
                    type="button"
                    onClick={handleAddDetail}
                    className="flex items-center gap-1 text-sm bg-blue-100 text-blue-600 px-2 py-1 rounded hover:bg-blue-200"
                  >
                    <FaPlus /> Agregar Detalle
                  </button>
                </div>

                {currentAppointment.appointmentDetail.map((detail, index) => (
                  <div key={index} className="mb-4 p-4 border border-gray-200 rounded-lg">
                    <div className="flex justify-between items-center mb-2">
                      <h5 className="font-medium text-gray-700">Detalle #{index + 1}</h5>
                      {currentAppointment.appointmentDetail.length > 1 && (
                        <button
                          type="button"
                          onClick={() => handleRemoveDetail(index)}
                          className="text-red-500 hover:text-red-700 text-sm"
                        >
                          Eliminar
                        </button>
                      )}
                    </div>

                    <div className="grid grid-cols-1 gap-3">
                      <div>
                        <label className="block text-gray-700 mb-1">Diagnóstico</label>
                        <input
                          type="text"
                          name="diagnosis"
                          placeholder="Diagnóstico"
                          value={detail.diagnosis}
                          onChange={(e) => handleDetailChange(e, index)}
                          required
                          className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                        />
                      </div>

                      <div>
                        <label className="block text-gray-700 mb-1">Tratamiento</label>
                        <input
                          type="text"
                          name="treatment"
                          placeholder="Tratamiento"
                          value={detail.treatment}
                          onChange={(e) => handleDetailChange(e, index)}
                          required
                          className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                        />
                      </div>

                      <div>
                        <label className="block text-gray-700 mb-1">Observaciones</label>
                        <textarea
                          name="observations"
                          placeholder="Observaciones"
                          value={detail.observations}
                          onChange={(e) => handleDetailChange(e, index)}
                          rows={3}
                          className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                        />
                      </div>
                    </div>
                  </div>
                ))}
              </div>

              <div className="flex justify-end gap-3 pt-4">
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false);
                    resetForm();
                  }}
                  className="px-4 py-2 border border-gray-300 rounded text-gray-700 hover:bg-gray-100"
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                >
                  {isEditing ? 'Actualizar Cita' : 'Crear Cita'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default Appointments;
