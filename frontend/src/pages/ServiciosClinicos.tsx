import React, { useState, useEffect } from 'react'
import {
  FaClinicMedical,
  FaPlus,
  FaSearch,
  FaEdit,
  FaTrash,
  FaMoneyBillWave,
  FaClock,
  FaNotesMedical
} from 'react-icons/fa'
import Header from '../components/Header'
import Footer from 'components/Footer'

interface ClinicalService {
  id: string
  name: string
  description: string
  duration: number // en minutos
  price: number
  category: string
  isActive: boolean
}

const ClinicalServicesPage = () => {
  const [services, setServices] = useState<ClinicalService[]>([])
  const [searchTerm, setSearchTerm] = useState('')
  const [isModalOpen, setIsModalOpen] = useState(false)
  const [currentService, setCurrentService] = useState<ClinicalService | null>(null)
  const [filter, setFilter] = useState<'all' | 'active' | 'inactive'>('all')

  // Datos de ejemplo (en producción vendrían de una API)
  useEffect(() => {
    const mockServices: ClinicalService[] = [
      {
        id: '1',
        name: 'Consulta General',
        description: 'Revisión médica completa de la mascota',
        duration: 30,
        price: 15000,
        category: 'Consultas',
        isActive: true
      },
      {
        id: '2',
        name: 'Vacunación Básica',
        description: 'Aplicación de vacunas esenciales',
        duration: 20,
        price: 25000,
        category: 'Prevención',
        isActive: true
      },
      {
        id: '3',
        name: 'Cirugía de Esterilización',
        description: 'Procedimiento quirúrgico para esterilización',
        duration: 120,
        price: 80000,
        category: 'Cirugías',
        isActive: true
      },
      {
        id: '4',
        name: 'Limpieza Dental',
        description: 'Limpieza profesional de dientes (temporalmente no disponible)',
        duration: 45,
        price: 35000,
        category: 'Odontología',
        isActive: false
      }
    ]
    setServices(mockServices)
  }, [])

  const filteredServices = services.filter((service) => {
    const matchesSearch =
      service.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      service.description.toLowerCase().includes(searchTerm.toLowerCase())
    const matchesFilter = filter === 'all' ||
      (filter === 'active' && service.isActive) ||
      (filter === 'inactive' && !service.isActive)
    return matchesSearch && matchesFilter
  })

  const handleAddService = () => {
    setCurrentService(null)
    setIsModalOpen(true)
  }

  const handleEditService = (service: ClinicalService) => {
    setCurrentService(service)
    setIsModalOpen(true)
  }

  const handleToggleStatus = (id: string) => {
    setServices(
      services.map((service) =>
        service.id === id
          ? { ...service, isActive: !service.isActive }
          : service
    ))
  }

  const handleDeleteService = (id: string) => {
    if (window.confirm('¿Estás seguro de eliminar este servicio?')) {
      setServices(services.filter((service) => service.id !== id))
    }
  }

  const handleSaveService = (service: ClinicalService) => {
    if (currentService) {
      // Editar servicio existente
      setServices(services.map((s) => (s.id === service.id ? service : s)))
    } else {
      // Agregar nuevo servicio
      const newService = {
        ...service,
        id: Date.now().toString()
      }
      setServices([...services, newService])
    }
    setIsModalOpen(false)
  }

  const categories = [...new Set(services.map(service => service.category))]

  return (
    <div className="min-h-screen bg-gray-50">
      <Header />

      <main className="p-4 md:p-8">
        <div className="mb-6 flex flex-col justify-between md:flex-row md:items-center">
          <h1 className="text-2xl font-bold text-gray-800">
            <FaClinicMedical className="inline mr-2" />
            Servicios Clínicos
          </h1>
          <button
            onClick={handleAddService}
            className="mt-4 md:mt-0 flex items-center justify-center rounded-lg bg-blue-600 px-4 py-2 text-white hover:bg-blue-700"
          >
            <FaPlus className="mr-2" />
            Nuevo Servicio
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
              placeholder="Buscar servicios..."
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
            <option value="all">Todos los servicios</option>
            <option value="active">Activos</option>
            <option value="inactive">Inactivos</option>
          </select>
        </div>

        {/* Lista de servicios */}
        <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
          {filteredServices.length > 0 ? (
            filteredServices.map((service) => (
              <div
                key={service.id}
                className="rounded-lg bg-white shadow-md overflow-hidden"
              >
                <div className={`p-4 ${service.isActive ? 'bg-blue-600' : 'bg-gray-500'} text-white`}>
                  <h3 className="text-xl font-semibold">{service.name}</h3>
                  <p className="text-sm opacity-90">{service.category}</p>
                </div>

                <div className="p-4">
                  <p className="text-gray-600 mb-4">{service.description}</p>

                  <div className="flex items-center mb-2 text-gray-700">
                    <FaClock className="mr-2 text-blue-500" />
                    <span>Duración: {service.duration} minutos</span>
                  </div>

                  <div className="flex items-center mb-4 text-gray-700">
                    <FaMoneyBillWave className="mr-2 text-green-500" />
                    <span>Precio: ₡{service.price.toLocaleString()}</span>
                  </div>

                  <div className="flex justify-between">
                    <button
                      onClick={() => handleToggleStatus(service.id)}
                      className={`px-3 py-1 rounded text-sm ${
                        service.isActive
                          ? 'bg-yellow-100 text-yellow-800 hover:bg-yellow-200'
                          : 'bg-green-100 text-green-800 hover:bg-green-200'
                      }`}
                    >
                      {service.isActive ? 'Desactivar' : 'Activar'}
                    </button>

                    <div>
                      <button
                        onClick={() => handleEditService(service)}
                        className="mr-2 px-3 py-1 rounded bg-blue-100 text-blue-800 text-sm hover:bg-blue-200"
                      >
                        <FaEdit className="inline mr-1" /> Editar
                      </button>
                      <button
                        onClick={() => handleDeleteService(service.id)}
                        className="px-3 py-1 rounded bg-red-100 text-red-800 text-sm hover:bg-red-200"
                      >
                        <FaTrash className="inline mr-1" /> Eliminar
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            ))
          ) : (
            <div className="col-span-full text-center py-8 text-gray-500">
              No se encontraron servicios
            </div>
          )}
        </div>

        {/* Modal para agregar/editar servicios */}
        {isModalOpen && (
          <ServiceModal
            service={currentService}
            categories={categories}
            onClose={() => setIsModalOpen(false)}
            onSave={handleSaveService}
          />
        )}
      </main>
      <Footer />
    </div>
  )
}

// Componente Modal para servicios
const ServiceModal = ({
  service,
  categories,
  onClose,
  onSave
}: {
  service: ClinicalService | null
  categories: string[]
  onClose: () => void
  onSave: (service: ClinicalService) => void
}) => {
  const [formData, setFormData] = useState<Omit<ClinicalService, 'id'>>({
    name: '',
    description: '',
    duration: 30,
    price: 0,
    category: categories[0] || '',
    isActive: true
  })

  useEffect(() => {
    if (service) {
      const { id, ...rest } = service
      setFormData(rest)
    } else {
      setFormData({
        name: '',
        description: '',
        duration: 30,
        price: 0,
        category: categories[0] || '',
        isActive: true
      })
    }
  }, [service, categories])

  const handleChange = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
  ) => {
    const { name, value } = e.target
    setFormData(prev => ({
      ...prev,
      [name]: name === 'duration' || name === 'price' ? Number(value) : value
    }))
  }

  const handleToggleActive = () => {
    setFormData(prev => ({ ...prev, isActive: !prev.isActive }))
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    const serviceToSave = service
      ? { ...formData, id: service.id }
      : { ...formData, id: Date.now().toString() }
    onSave(serviceToSave as ClinicalService)
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
              {service ? 'Editar Servicio' : 'Agregar Nuevo Servicio'}
            </h3>
            <form onSubmit={handleSubmit} className="mt-4 space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700">Nombre del Servicio *</label>
                <input
                  type="text"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  required
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700">Descripción *</label>
                <textarea
                  name="description"
                  value={formData.description}
                  onChange={handleChange}
                  rows={3}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  required
                />
              </div>

              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">Duración (minutos) *</label>
                  <input
                    type="number"
                    name="duration"
                    value={formData.duration}
                    onChange={handleChange}
                    min="10"
                    max="240"
                    className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    required
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">Precio (₡) *</label>
                  <input
                    type="number"
                    name="price"
                    value={formData.price}
                    onChange={handleChange}
                    min="0"
                    step="1000"
                    className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    required
                  />
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700">Categoría *</label>
                <select
                  name="category"
                  value={formData.category}
                  onChange={handleChange}
                  className="mt-1 block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  required
                >
                  {categories.map(category => (
                    <option key={category} value={category}>{category}</option>
                  ))}
                </select>
              </div>

              <div className="flex items-center">
                <input
                  type="checkbox"
                  id="isActive"
                  checked={formData.isActive}
                  onChange={handleToggleActive}
                  className="h-4 w-4 rounded border-gray-300 text-blue-600 focus:ring-blue-500"
                />
                <label htmlFor="isActive" className="ml-2 block text-sm text-gray-700">
                  Servicio activo
                </label>
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
                  {service ? 'Guardar Cambios' : 'Agregar Servicio'}
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}

export default ClinicalServicesPage
