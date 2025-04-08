import React, { useState } from 'react';
import Header from '../components/Header';
import Footer from '../components/Footer';
import axios from 'axios';
import Swal from 'sweetalert2';

const Vaccines: React.FC = () => {
  const [vaccines, setVaccines] = useState<any[]>([]);
  const [search, setSearch] = useState('');
  const [modalOpen, setModalOpen] = useState(false);
  const [newVaccine, setNewVaccine] = useState({
    vaccineName: '',
    description: '',
    type: '',
    state: 0,
    auditCreateUser: 0,
  });

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const handleNewVaccineChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewVaccine({ ...newVaccine, [e.target.name]: e.target.value });
  };

  const handleAddVaccine = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await axios.post('https://api.vetfriends.customcodecr.com/api/v1/Vaccine/Create', newVaccine, {
        headers: {
          'Content-Type': 'application/json',
          Accept: '*/*',
        },
      });

      Swal.fire({
        icon: 'success',
        title: 'Vacuna agregada',
        text: 'La vacuna ha sido registrada con éxito.',
      });

      setVaccines([...vaccines, response.data.data]);
      setModalOpen(false);
      setNewVaccine({
        vaccineName: '',
        description: '',
        type: '',
        state: 0,
        auditCreateUser: 0,
      });
    } catch (error) {
      console.error('Error al crear vacuna:', error);
      Swal.fire({
        icon: 'error',
        title: 'Error al crear vacuna',
        text: 'Verifica los datos e intenta de nuevo.',
      });
    }
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-4xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-orange-600 mb-4">Gestión de Vacunas</h2>

          <div className="flex items-center justify-between mb-4">
            <input
              type="text"
              placeholder="Buscar vacunas..."
              value={search}
              onChange={handleSearchChange}
              className="w-full md:w-1/2 p-2 border rounded"
            />
            <button
              className="ml-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-green-700"
              onClick={() => {
                setNewVaccine({
                  vaccineName: '',
                  description: '',
                  type: '',
                  state: 0,
                  auditCreateUser: 0,
                });
                setModalOpen(true);
              }}
            >
              + Nueva Vacuna
            </button>
          </div>

          <table className="w-full border-collapse border border-gray-300">
            <thead>
              <tr className="bg-gray-200">
                <th className="border p-2">Nombre</th>
                <th className="border p-2">Tipo</th>
                <th className="border p-2">Descripción</th>
              </tr>
            </thead>
            <tbody>
              {vaccines
                .filter((v) =>
                  v.vaccineName.toLowerCase().includes(search.toLowerCase())
                )
                .map((vaccine, index) => (
                  <tr key={index} className="text-center border">
                    <td className="border p-2">{vaccine.vaccineName}</td>
                    <td className="border p-2">{vaccine.type}</td>
                    <td className="border p-2">{vaccine.description}</td>
                  </tr>
                ))}
              {vaccines.length === 0 && (
                <tr>
                  <td colSpan={3} className="text-center p-4">
                    No hay vacunas registradas.
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
            <h3 className="text-2xl font-bold text-center text-blue-600">Agregar Vacuna</h3>
            <form onSubmit={handleAddVaccine} className="mt-4 space-y-4">
              <input
                type="text"
                name="vaccineName"
                placeholder="Nombre de la vacuna"
                value={newVaccine.vaccineName}
                onChange={handleNewVaccineChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="type"
                placeholder="Tipo"
                value={newVaccine.type}
                onChange={handleNewVaccineChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="description"
                placeholder="Descripción"
                value={newVaccine.description}
                onChange={handleNewVaccineChange}
                required
                className="w-full p-2 border rounded"
              />
              <div className="flex justify-between">
                <button
                  type="submit"
                  className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
                >
                  Agregar
                </button>
                <button
                  type="button"
                  onClick={() => setModalOpen(false)}
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

export default Vaccines;
