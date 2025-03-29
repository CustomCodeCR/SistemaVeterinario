import React, { useState, useEffect } from 'react'
import {
  FaCalendarAlt,
  FaSearch,
  FaPlus,
  FaEdit,
  FaTrash,
  FaUser,
  FaDog,
  FaClock,
  FaPhone,
  FaNotesMedical
} from 'react-icons/fa'
import Header from '../components/Header'
import Footer from '../components/Footer'

interface Appointment {
  id: string
  petName: string
  ownerName: string
  date: string
  time: string
  service: string
  phone: string
  notes: string
  status: 'pending' | 'confirmed' | 'completed' | 'cancelled'
}

const AppointmentsPage = () => {
  const [appointments, setAppointments] = useState<Appointment[]>([])
  const [searchTerm, setSearchTerm] = useState('')
  const [isModalOpen, setIsModalOpen] = useState(false)
  const [currentAppointment, setCurrentAppointment] = useState<Appointment | null>(null)
  const [filter, setFilter] = useState<'all' | 'pending' | 'confirmed' | 'completed' | 'cancelled'>('all')

  // Datos de ejemplo (en producción vendrían de una API)
  useEffect(() => {
    const mockAppointments: Appointment[] = [
      {
        id: '1',
        petName: 'Max',
        ownerName: 'Carlos Pérez',
        date: '2023-06-15',
        time: '10:00',
        service: 'Consulta general',
        phone: '8888-8888',
        notes: 'Vacuna antirrábica pendiente',
        status: 'confirmed'
      },
      {
        id: '2',
        petName: 'Luna',
        ownerName: 'María Rodríguez',
        date: '2023-06-15',
        time: '11:30',
        service: 'Vacunación',
        phone: '8888-9999',
        notes: 'Primera vacuna',
        status: 'pending'
      },
      {
        id: '3',
        petName: 'Rocky',
        ownerName: 'Juan Gómez',
        date: '2023-06-16',
        time: '09:00',
        service: 'Cirugía',
        phone: '8888-7777',
        notes: 'Castración programada',
        status: 'confirmed'
      }
    ]
    setAppointments(mockAppointments)
  }, [])

  const filteredAppointments = appointments.filter(appointment => {
    const matchesSearch = appointment.petName.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         appointment.ownerName.toLowerCase().includes(searchTerm.toLowerCase())
    const matchesFilter = filter === 'all' || appointment.status === filter
    return matchesSearch && matchesFilter
  })

  const handleAddAppointment = () => {
    setCurrentAppointment(null)
    setIsModalOpen(true)
  }

  const handleEditAppointment = (appointment: Appointment) => {
    setCurrentAppointment(appointment)
    setIsModalOpen(true)
  }

  const handleDeleteAppointment = (id: string) => {
    if (window.confirm('¿Estás seguro de eliminar esta cita?')) {
      setAppointments(appointments.filter(app => app.id !== id))
    }
  }

  const handleSaveAppointment = (appointment: Appointment) => {
    if (currentAppointment) {
      // Editar cita existente
      setAppointments(appointments.map(app =>
        app.id === appointment.id ? appointment : app
      ))
    } else {
      // Agregar nueva cita
      const newAppointment = {
        ...appointment,
        id: Date.now().toString()
      }
      setAppointments([...appointments, newAppointment])
    }
    setIsModalOpen(false)
  }

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'pending': return 'bg-yellow-100 text-yellow-800'
      case 'confirmed': return 'bg-blue-100 text-blue-800'
      case 'completed': return 'bg-green-100 text-green-800'
      case 'cancelled': return 'bg-red-100 text-red-800'
      default: return 'bg-gray-100 text-gray-800'
    }
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <Header />

      <main className="p-4 md:p-8">
        <div className="mb-6 flex flex-col justify-between md:flex-row md:items-center">
        <h1 className="text-2xl font-extrabold text-blue-900 sm:text-2xl sm:tracking-tight lg:text-2xl">
          Gestión de<span className="text-orange-500"> Citas</span>
        </h1>
          <button
            onClick={handleAddAppointment}
            className="mt-4 md:mt-0 flex items-center justify-center rounded-lg bg-blue-600 px-4 py-2 text-white hover:bg-blue-700"
          >
            <FaPlus className="mr-2" />
            Nueva Cita
          </button>
        </div>

        {/* Filtros y búsqueda */}
        <div className="mb-6 grid grid-cols-1 gap-4 md:grid-cols-2">
          <div className="relative">
            <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
              <FaSearch className="text-gray-400" />
            </div>
            <input
              type="text"
              placeholder="Buscar por mascota o dueño..."
              className="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2 pl-10 text-sm text-gray-900 focus:border-blue-500 focus:ring-blue-500"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>

          <select
            className="rounded-lg border border-gray-300 bg-gray-50 p-2 text-sm text-gray-900 focus:border-blue-500 focus:ring-blue-500"
            value={filter}
            onChange={(e) => setFilter(e.target.value as any)}
          >
            <option value="all">Todas las citas</option>
            <option value="pending">Pendientes</option>
            <option value="confirmed">Confirmadas</option>
            <option value="completed">Completadas</option>
            <option value="cancelled">Canceladas</option>
          </select>
        </div>

        {/* Lista de citas */}
        <div className="overflow-hidden rounded-lg shadow">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                  Mascota
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                  Dueño
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                  Fecha y Hora
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                  Servicio
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                  Estado
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                  Acciones
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-200 bg-white">
              {filteredAppointments.length > 0 ? (
                filteredAppointments.map((appointment) => (
                  <tr key={appointment.id} className="hover:bg-gray-50">
                    <td className="whitespace-nowrap px-6 py-4">
                      <div className="flex items-center">
                        <FaDog className="mr-2 text-gray-500" />
                        <span className="font-medium">{appointment.petName}</span>
                      </div>
                    </td>
                    <td className="whitespace-nowrap px-6 py-4">
                      <div className="flex items-center">
                        <FaUser className="mr-2 text-gray-500" />
                        {appointment.ownerName}
                      </div>
                    </td>
                    <td className="whitespace-nowrap px-6 py-4">
                      <div className="flex items-center">
                        <FaClock className="mr-2 text-gray-500" />
                        {appointment.date} - {appointment.time}
                      </div>
                    </td>
                    <td className="whitespace-nowrap px-6 py-4">
                      {appointment.service}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4">
                      <span className={`inline-flex rounded-full px-2 py-1 text-xs font-semibold leading-5 ${getStatusColor(appointment.status)}`}>
                        {appointment.status === 'pending' && 'Pendiente'}
                        {appointment.status === 'confirmed' && 'Confirmada'}
                        {appointment.status === 'completed' && 'Completada'}
                        {appointment.status === 'cancelled' && 'Cancelada'}
                      </span>
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-right text-sm font-medium">
                      <button
                        onClick={() => handleEditAppointment(appointment)}
                        className="mr-3 text-blue-600 hover:text-blue-900"
                      >
                        <FaEdit className="inline" />
                      </button>
                      <button
                        onClick={() => handleDeleteAppointment(appointment.id)}
                        className="text-red-600 hover:text-red-900"
                      >
                        <FaTrash className="inline" />
                      </button>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={6} className="px-6 py-4 text-center text-gray-500">
                    No se encontraron citas
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>

        {/* Modal para agregar/editar citas */}
        {isModalOpen && (
          <AppointmentModal
            appointment={currentAppointment}
            onClose={() => setIsModalOpen(false)}
            onSave={handleSaveAppointment}
          />
        )}
      </main>
      <Footer />
    </div>
  )
}

// Componente Modal para citas
const AppointmentModal = ({ appointment, onClose, onSave }: {
  appointment: Appointment | null
  onClose: () => void
  onSave: (appointment: Appointment) => void
}) => {
  const [formData, setFormData] = useState<Omit<Appointment, 'id'>>({
    petName: '',
    ownerName: '',
    date: new Date().toISOString().split('T')[0],
    time: '09:00',
    service: 'Consulta general',
    phone: '',
    notes: '',
    status: 'pending'
  })

  useEffect(() => {
    if (appointment) {
      const { id, ...rest } = appointment
      setFormData(rest)
    } else {
      setFormData({
        petName: '',
        ownerName: '',
        date: new Date().toISOString().split('T')[0],
        time: '09:00',
        service: 'Consulta general',
        phone: '',
        notes: '',
        status: 'pending'
      })
    }
  }, [appointment])

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target
    setFormData(prev => ({ ...prev, [name]: value }))
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    const appointmentToSave = appointment
      ? { ...formData, id: appointment.id }
      : { ...formData, id: Date.now().toString() }
    onSave(appointmentToSave as Appointment)
  }

  return (
    <div className="fixed inset-0 z-50 overflow-y-auto">
      <div className="flex min-h-screen items-center justify-center px-4 pt-4 pb-20 text-center sm:block sm:p-0">
        <div className="fixed inset-0 transition-opacity" onClick={onClose}>
          <div className="absolute inset-0 bg-gray-500 opacity-75"></div>
        </div>
        <span className="hidden sm:inline-block sm:h-screen sm:align-middle">&#8203;</span>
        <div className="inline-block transform overflow-hidden rounded-lg bg-white text-left align-bottom shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg sm:align-middle">
          <div className="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
            <h3 className="text-lg font-medium leading-6 text-gray-900">
              {appointment ? 'Editar Cita' : 'Agregar Nueva Cita'}
            </h3>
            <form onSubmit={handleSubmit} className="mt-4 space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700">Nombre de la Mascota *</label>
                <input
                  type="text"
                  name="petName"
                  value={formData.petName}
                  onChange={handleChange}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  required
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700">Nombre del Dueño *</label>
                <input
                  type="text"
                  name="ownerName"
                  value={formData.ownerName}
                  onChange={handleChange}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  required
                />
              </div>

              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">Fecha *</label>
                  <input
                    type="date"
                    name="date"
                    value={formData.date}
                    onChange={handleChange}
                    className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    required
                    min={new Date().toISOString().split('T')[0]}
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">Hora *</label>
                  <input
                    type="time"
                    name="time"
                    value={formData.time}
                    onChange={handleChange}
                    className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    required
                    min="09:00"
                    max="18:00"
                  />
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700">Servicio *</label>
                <select
                  name="service"
                  value={formData.service}
                  onChange={handleChange}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  required
                >
                  <option value="Consulta general">Consulta general</option>
                  <option value="Vacunación">Vacunación</option>
                  <option value="Cirugía">Cirugía</option>
                  <option value="Peluquería">Peluquería</option>
                  <option value="Chequeo anual">Chequeo anual</option>
                </select>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700">Teléfono *</label>
                <input
                  type="tel"
                  name="phone"
                  value={formData.phone}
                  onChange={handleChange}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  required
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700">Notas</label>
                <textarea
                  name="notes"
                  value={formData.notes}
                  onChange={handleChange}
                  rows={3}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700">Estado *</label>
                <select
                  name="status"
                  value={formData.status}
                  onChange={handleChange}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  required
                >
                  <option value="pending">Pendiente</option>
                  <option value="confirmed">Confirmada</option>
                  <option value="completed">Completada</option>
                  <option value="cancelled">Cancelada</option>
                </select>
              </div>

              <div className="flex justify-end space-x-3 pt-4">
                <button
                  type="button"
                  onClick={onClose}
                  className="rounded-md border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  className="rounded-md border border-transparent bg-blue-600 px-4 py-2 text-sm font-medium text-white shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
                >
                  {appointment ? 'Guardar Cambios' : 'Agregar Cita'}
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}

export default AppointmentsPage
