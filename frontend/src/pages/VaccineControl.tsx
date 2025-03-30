import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaUser, FaPaw, FaSyringe } from 'react-icons/fa';
import Header from '../components/Header';
import Footer from '../components/Footer';

interface Pet {
  id: number;
  name: string;
  species: string;
  breed: string;
  weight: string;
  age: string;
  owner: string;
  contact: string;
  image: string;
  appliedVaccines: string[];
  pendingVaccines: string[];
  upcomingVaccines: { name: string; date: string }[];
}

const VaccineControl: React.FC = () => {
  const [pets, setPets] = useState<Pet[]>([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingPet, setEditingPet] = useState<Pet | null>(null);
  const [newPet, setNewPet] = useState({
    name: '',
    species: '',
    breed: '',
    weight: '',
    age: '',
    owner: '',
    contact: '',
    image: '',
    appliedVaccines: '',
    pendingVaccines: '',
    upcomingVaccines: ''
  });
  // Estado para controlar el modal de vacunas
  const [selectedVaccinesPet, setSelectedVaccinesPet] = useState<Pet | null>(null);

  useEffect(() => {
    const storedPets = JSON.parse(localStorage.getItem('mascotas') || '[]');
    setPets(storedPets);
  }, []);

  const handleNewPetChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setNewPet({ ...newPet, [e.target.name]: e.target.value });
  };

  const handleAddOrUpdatePet = (e: React.FormEvent) => {
    e.preventDefault();

    const storedPets = JSON.parse(localStorage.getItem('mascotas') || '[]');

    const petData: Pet = {
      id: editingPet ? editingPet.id : storedPets.length + 1,
      name: newPet.name,
      species: newPet.species,
      breed: newPet.breed,
      weight: newPet.weight,
      age: newPet.age,
      owner: newPet.owner,
      contact: newPet.contact,
      image: newPet.image,
      appliedVaccines: newPet.appliedVaccines.split(',').map(v => v.trim()),
      pendingVaccines: newPet.pendingVaccines.split(',').map(v => v.trim()),
      upcomingVaccines: newPet.upcomingVaccines.split(',').map(v => {
        const [name, date] = v.split('-').map(str => str.trim());
        return { name, date };
      })
    };

    let updatedPets;
    if (editingPet) {
      updatedPets = storedPets.map((pet: Pet) => (pet.id === editingPet.id ? petData : pet));
    } else {
      updatedPets = [...storedPets, petData];
    }

    setPets(updatedPets);
    localStorage.setItem('mascotas', JSON.stringify(updatedPets));

    setModalOpen(false);
    setEditingPet(null);
    setNewPet({
      name: '',
      species: '',
      breed: '',
      weight: '',
      age: '',
      owner: '',
      contact: '',
      image: '',
      appliedVaccines: '',
      pendingVaccines: '',
      upcomingVaccines: ''
    });
  };

  const handleDeletePet = (id: number) => {
    const updatedPets = pets.filter((pet) => pet.id !== id);
    setPets(updatedPets);
    localStorage.setItem('mascotas', JSON.stringify(updatedPets));
  };

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Mascotas y Vacunas</h2>

          {/* Botón para agregar nueva mascota */}
          <div className="flex justify-end mb-4">
            <button
              className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 flex items-center"
              onClick={() => {
                setEditingPet(null);
                setNewPet({
                  name: '',
                  species: '',
                  breed: '',
                  weight: '',
                  age: '',
                  owner: '',
                  contact: '',
                  image: '',
                  appliedVaccines: '',
                  pendingVaccines: '',
                  upcomingVaccines: ''
                });
                setModalOpen(true);
              }}
            >
              <FaPaw className="mr-2" /> Nueva Mascota
            </button>
          </div>

          {/* Tarjetas de mascotas */}
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
            {pets.map((pet) => (
              <div key={pet.id} className="bg-white p-4 rounded-lg shadow-lg">
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
                  <button 
                    onClick={() => {
                      setEditingPet(pet);
                      setNewPet({
                        name: pet.name,
                        species: pet.species,
                        breed: pet.breed,
                        weight: pet.weight,
                        age: pet.age,
                        owner: pet.owner,
                        contact: pet.contact,
                        image: pet.image,
                        appliedVaccines: pet.appliedVaccines.join(', '),
                        pendingVaccines: pet.pendingVaccines.join(', '),
                        upcomingVaccines: pet.upcomingVaccines.map(v => `${v.name} - ${v.date}`).join(', ')
                      });
                      setModalOpen(true);
                    }}
                    className="bg-blue-500 text-white px-3 py-1 rounded flex items-center"
                  >
                    <FaEdit className="inline mr-2" /> Editar
                  </button>
                  <button 
                    onClick={() => handleDeletePet(pet.id)} 
                    className="bg-red-500 text-white px-3 py-1 rounded flex items-center"
                  >
                    <FaTrash className="inline mr-2" /> Eliminar
                  </button>
                  <button 
                    onClick={() => setSelectedVaccinesPet(pet)} 
                    className="bg-green-500 text-white px-3 py-1 rounded flex items-center"
                  >
                    <FaSyringe className="inline mr-2" /> Vacunas
                  </button>
                </div>
              </div>
            ))}
          </div>
        </div>
      </main>
      <Footer />

      {/* Modal para agregar o editar mascota */}
      {modalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full">
            <h3 className="text-2xl font-bold text-center text-blue-600">
              {editingPet ? 'Editar Mascota' : 'Agregar Nueva Mascota'}
            </h3>
            <form onSubmit={handleAddOrUpdatePet} className="mt-4 space-y-4">
              <input type="text" name="name" placeholder="Nombre" value={newPet.name} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="species" placeholder="Especie" value={newPet.species} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="breed" placeholder="Raza" value={newPet.breed} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="weight" placeholder="Peso (kg)" value={newPet.weight} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="age" placeholder="Edad (años)" value={newPet.age} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="owner" placeholder="Nombre del dueño" value={newPet.owner} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="contact" placeholder="Contacto" value={newPet.contact} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="image" placeholder="URL de la imagen" value={newPet.image} onChange={handleNewPetChange} className="w-full p-2 border rounded" />
              <input type="text" name="appliedVaccines" placeholder="Vacunas aplicadas (separadas por coma)" value={newPet.appliedVaccines} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="pendingVaccines" placeholder="Vacunas pendientes (separadas por coma)" value={newPet.pendingVaccines} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <input type="text" name="upcomingVaccines" placeholder="Vacunas próximas (nombre - fecha)" value={newPet.upcomingVaccines} onChange={handleNewPetChange} required className="w-full p-2 border rounded" />
              <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 w-full">Guardar</button>
            </form>
          </div>
        </div>
      )}

      {/* Modal para ver las vacunas de una mascota */}
      {selectedVaccinesPet && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full">
            <h3 className="text-2xl font-bold text-center text-blue-600">
              Vacunas de {selectedVaccinesPet.name}
            </h3>
            <div className="mt-4">
              <h4 className="font-bold">Vacunas aplicadas:</h4>
              <ul>
                {selectedVaccinesPet.appliedVaccines.map((vac, index) => (
                  <li key={index}>{vac}</li>
                ))}
              </ul>
              <h4 className="font-bold mt-2">Vacunas pendientes:</h4>
              <ul>
                {selectedVaccinesPet.pendingVaccines.map((vac, index) => (
                  <li key={index}>{vac}</li>
                ))}
              </ul>
              <h4 className="font-bold mt-2">Vacunas próximas:</h4>
              <ul>
                {selectedVaccinesPet.upcomingVaccines.map((vac, index) => (
                  <li key={index}>
                    {vac.name} - {vac.date}
                  </li>
                ))}
              </ul>
            </div>
            <button
              onClick={() => setSelectedVaccinesPet(null)}
              className="mt-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 w-full"
            >
              Cerrar
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default VaccineControl;




