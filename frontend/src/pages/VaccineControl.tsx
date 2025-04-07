import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaUser, FaPaw } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';
import axiosInstance from '../api/axiosInstance';

interface Vaccine {
  vaccineId: number;
  name: string;
}

interface AppliedVaccine {
  vaccineId: number;
  applicationDate: string;
}

interface Pet {
  petId: number;
  name: string;
  species: string;
  breed: string;
  weight: string;
  age: string;
  owner: string;
  contact: string;
  image: string;
}

const VaccineControl: React.FC = () => {
  const [pets, setPets] = useState<Pet[]>([]);
  const [vaccines, setVaccines] = useState<Vaccine[]>([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingPet, setEditingPet] = useState<Pet | null>(null);
  const [newPet, setNewPet] = useState({
    name: '', species: '', breed: '', weight: '', age: '', owner: '', contact: '', image: ''
  });
  const [appliedVaccines, setAppliedVaccines] = useState<AppliedVaccine[]>([]);

  useEffect(() => {
    fetchPets();
    fetchVaccines();
  }, []);

  const fetchPets = async () => {
    try {
      const res = await axiosInstance.get('/Pet');
      setPets(res.data.data);
    } catch (error) {
      console.error('Error al obtener mascotas:', error);
    }
  };

  const fetchVaccines = async () => {
    try {
      const res = await axiosInstance.get('/Vaccine');
      setVaccines(res.data.data);
    } catch (error) {
      console.error('Error al obtener vacunas:', error);
    }
  };

  const handlePetChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewPet({ ...newPet, [e.target.name]: e.target.value });
  };

  const handleVaccineChange = (index: number, field: keyof AppliedVaccine, value: string) => {
    const updated = [...appliedVaccines];
    updated[index][field] = field === 'vaccineId' ? parseInt(value) : value;
    setAppliedVaccines(updated);
  };

  const addVaccineField = () => {
    setAppliedVaccines([...appliedVaccines, { vaccineId: vaccines[0]?.vaccineId || 0, applicationDate: '' }]);
  };

  const handleAddOrUpdatePet = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      let savedPetId: number;
      if (editingPet) {
        await axiosInstance.put(`/Pet/${editingPet.petId}`, { petId: editingPet.petId, ...newPet });
        savedPetId = editingPet.petId;
      } else {
        const response = await axiosInstance.post('/Pet', newPet);
        savedPetId = response.data.data.petId;
      }

      for (const vaccine of appliedVaccines) {
        await axiosInstance.post('/api/v1/AppliedVaccine/Create', {
          applicationDate: vaccine.applicationDate,
          petId: savedPetId,
          vaccineId: vaccine.vaccineId,
          auditCreateUser: 1
        });
      }

      fetchPets();
      resetForm();
      setModalOpen(false);
    } catch (error) {
      console.error('Error al guardar mascota:', error);
    }
  };

  const resetForm = () => {
    setNewPet({ name: '', species: '', breed: '', weight: '', age: '', owner: '', contact: '', image: '' });
    setAppliedVaccines([]);
    setEditingPet(null);
  };

  const handleEdit = (pet: Pet) => {
    setEditingPet(pet);
    setNewPet(pet);
    setAppliedVaccines([]);
    setModalOpen(true);
  };

  const handleDeletePet = async (id: number) => {
    if (!window.confirm('¿Estás seguro de eliminar esta mascota?')) return;
    try {
      await axiosInstance.delete(`/Pet/${id}`);
      fetchPets();
    } catch (error) {
      console.error('Error al eliminar mascota:', error);
    }
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Mascotas y Vacunas</h2>
          <div className="flex justify-end mb-4">
            <button className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 flex items-center" onClick={() => { resetForm(); setModalOpen(true); }}>
              <FaPaw className="mr-2" /> Nueva Mascota
            </button>
          </div>
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
            {pets.map((pet) => (
              <div key={pet.petId} className="bg-white p-4 rounded-lg shadow-lg">
                <div className="bg-blue-600 text-white p-2 rounded-t-lg">
                  <h3 className="text-xl font-bold">{pet.name}</h3>
                  <p className="text-sm">{pet.species} - {pet.breed}</p>
                </div>
                <div className="p-3">
                  <p><strong>Peso:</strong> {pet.weight} kg</p>
                  <p><strong>Edad:</strong> {pet.age} años</p>
                  <p><FaUser className="inline text-gray-600" /> <strong>Dueño:</strong> {pet.owner}</p>
                  <p><strong>Contacto:</strong> {pet.contact}</p>
                </div>
                <div className="flex justify-around p-2">
                  <button onClick={() => handleEdit(pet)} className="bg-blue-500 text-white px-3 py-1 rounded flex items-center">
                    <FaEdit className="mr-2" /> Editar
                  </button>
                  <button onClick={() => handleDeletePet(pet.petId)} className="bg-red-500 text-white px-3 py-1 rounded flex items-center">
                    <FaTrash className="mr-2" /> Eliminar
                  </button>
                </div>
              </div>
            ))}
          </div>
        </div>
      </main>
      <Footer />

      {modalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full">
            <h3 className="text-2xl font-bold text-center text-blue-600">
              {editingPet ? 'Editar Mascota' : 'Agregar Nueva Mascota'}
            </h3>
            <form onSubmit={handleAddOrUpdatePet} className="mt-4 space-y-4">
              {Object.entries(newPet).map(([key, value]) => (
                <input key={key} type="text" name={key} placeholder={key} value={value} onChange={handlePetChange} required={key !== 'image'} className="w-full p-2 border rounded" />
              ))}
              <h4 className="font-bold mt-4">Vacunas Aplicadas</h4>
              {appliedVaccines.map((vaccine, index) => (
                <div key={index} className="flex gap-2">
                  <select
                    value={vaccine.vaccineId}
                    onChange={(e) => handleVaccineChange(index, 'vaccineId', e.target.value)}
                    className="w-1/2 p-2 border rounded"
                  >
                    {vaccines.map((v) => (
                      <option key={v.vaccineId} value={v.vaccineId}>{v.name}</option>
                    ))}
                  </select>
                  <input
                    type="date"
                    value={vaccine.applicationDate}
                    onChange={(e) => handleVaccineChange(index, 'applicationDate', e.target.value)}
                    className="w-1/2 p-2 border rounded"
                    required
                  />
                </div>
              ))}
              <button type="button" onClick={addVaccineField} className="text-blue-600 text-sm underline">
                + Agregar vacuna aplicada
              </button>
              <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 w-full">
                Guardar
              </button>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default VaccineControl;












