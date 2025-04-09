import React, { useState } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';
import axios from 'axios';
import Swal from 'sweetalert2';

const Appointments: React.FC = () => {
  const [appointments, setAppointments] = useState<any[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [newAppointment, setNewAppointment] = useState({
    appointmentDate: '',
    reason: '',
    petId: 0,
    medicId: 0,
    state: 0,
    auditCreateUser: 0,
    appointmentDetail: [{ diagnosis: '', treatment: '', observations: '' }],
  });

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const handleNewAppointmentChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewAppointment({ ...newAppointment, [e.target.name]: e.target.value });
  };

  const handleAppointmentDetailChange = (
    e: React.ChangeEvent<HTMLInputElement>,
    index: number
  ) => {
    const updatedDetails = [...newAppointment.appointmentDetail];
    updatedDetails[index] = {
      ...updatedDetails[index],
      [e.target.name]: e.target.value,
    };
    setNewAppointment({ ...newAppointment, appointmentDetail: updatedDetails });
  };

  const handleAddOrUpdateAppointment = (e: React.FormEvent) => {
    e.preventDefault();

    axios
      .post('https://api.vetfriends.customcodecr.com/api/v1/Appoiment', newAppointment, {
        headers: { 'Content-Type': 'application/json' },
      })
      .then((response) => {
        setAppointments([...appointments, response.data.data]);
        setModalOpen(false);
        Swal.fire('¡Éxito!', 'La cita se ha creado correctamente.', 'success');
        setNewAppointment({
          appointmentDate: '',
          reason: '',
          petId: 0,
          medicId: 0,
          state: 0,
          auditCreateUser: 0,
          appointmentDetail: [{ diagnosis: '', treatment: '', observations: '' }],
        });
      })
      .catch((error) => {
        Swal.fire('Error', 'No se pudo crear la cita.', 'error');
        console.error('Error al crear cita:', error);
      });
  };

  const handleEdit = (appointment: any) => {
    setNewAppointment(appointment);
    setModalOpen(true);
  };

  const handleDelete = (id: number) => {
    Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción no se puede deshacer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        const updatedAppointments = appointments.filter((app) => app.id !== id);
        setAppointments(updatedAppointments);
        Swal.fire('Eliminado', 'La cita ha sido eliminada.', 'success');
      }
    });
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Citas</h2>

          {/* Buscador y botón */}
          <div className="flex items-center justify-between mb-4">
            <input
              type="text"
              placeholder="Buscar citas..."
              value={search}
              onChange={handleSearchChange}
              className="w-full md:w-1/2 p-2 border rounded"
            />
            <button
              className="ml-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
              onClick={() => {
                setNewAppointment({
                  appointmentDate: '',
                  reason: '',
                  petId: 0,
                  medicId: 0,
                  state: 0,
                  auditCreateUser: 90,
                  appointmentDetail: [{ diagnosis: '', treatment: '', observations: '' }],
                });
                setModalOpen(true);
              }}
            >
              + Nueva Cita
            </button>
          </div>

          {/* Tabla de citas */}
          <table className="w-full border-collapse border border-gray-300">
            <thead>
              <tr className="bg-gray-200">
                <th className="border p-2">ID</th>
                <th className="border p-2">Fecha</th>
                <th className="border p-2">Razón</th>
                <th className="border p-2">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {appointments.map((appointment) => (
                <tr key={appointment.id} className="text-center border">
                  <td className="border p-2">{appointment.id}</td>
                  <td className="border p-2">{new Date(appointment.appointmentDate).toLocaleString()}</td>
                  <td className="border p-2">{appointment.reason}</td>
                  <td className="border p-2 flex justify-center space-x-4">
                    <button
                      onClick={() => handleEdit(appointment)}
                      className="text-blue-600 hover:text-blue-800"
                    >
                      <FaEdit />
                    </button>
                    <button
                      onClick={() => handleDelete(appointment.id)}
                      className="text-red-600 hover:text-red-800"
                    >
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
              {appointments.length === 0 && (
                <tr>
                  <td colSpan={4} className="text-center p-4">
                    No hay citas encontradas.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </main>
      <Footer />

      {/* Modal */}
      {modalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full">
            <h3 className="text-2xl font-bold text-center text-blue-600">
              {newAppointment.id ? 'Editar Cita' : 'Agregar Nueva Cita'}
            </h3>
            <form onSubmit={handleAddOrUpdateAppointment} className="mt-4 space-y-4">
              <input
                type="datetime-local"
                name="appointmentDate"
                value={newAppointment.appointmentDate}
                onChange={handleNewAppointmentChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="reason"
                placeholder="Razón de la cita"
                value={newAppointment.reason}
                onChange={handleNewAppointmentChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="number"
                name="petId"
                placeholder="ID de la mascota"
                value={newAppointment.petId}
                onChange={handleNewAppointmentChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="number"
                name="medicId"
                placeholder="ID del médico"
                value={newAppointment.medicId}
                onChange={handleNewAppointmentChange}
                required
                className="w-full p-2 border rounded"
              />
              {/* Detalles */}
              {newAppointment.appointmentDetail.map((detail, index) => (
                <div key={index} className="space-y-2">
                  <input
                    type="text"
                    name="diagnosis"
                    placeholder="Diagnóstico"
                    value={detail.diagnosis}
                    onChange={(e) => handleAppointmentDetailChange(e, index)}
                    required
                    className="w-full p-2 border rounded"
                  />
                  <input
                    type="text"
                    name="treatment"
                    placeholder="Tratamiento"
                    value={detail.treatment}
                    onChange={(e) => handleAppointmentDetailChange(e, index)}
                    required
                    className="w-full p-2 border rounded"
                  />
                  <input
                    type="text"
                    name="observations"
                    placeholder="Observaciones"
                    value={detail.observations}
                    onChange={(e) => handleAppointmentDetailChange(e, index)}
                    required
                    className="w-full p-2 border rounded"
                  />
                </div>
              ))}
              <div className="flex justify-between">
                <button
                  type="submit"
                  className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                >
                  {newAppointment.id ? 'Guardar Cambios' : 'Agregar'}
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false);
                    Swal.fire('Cancelado', 'No se realizó ninguna acción.', 'info');
                    setNewAppointment({
                      appointmentDate: '',
                      reason: '',
                      petId: 0,
                      medicId: 0,
                      state: 0,
                      auditCreateUser: 0,
                      appointmentDetail: [{ diagnosis: '', treatment: '', observations: '' }],
                    });
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

export default Appointments;
