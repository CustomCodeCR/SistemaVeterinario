import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import {
  FaPlus,
  FaUserShield,
  FaSyringe,
  FaCalendarAlt,
  FaChartLine,
  FaBoxes,
  FaUsers,
  FaClinicMedical,
  FaFileInvoiceDollar,
  FaTags
} from 'react-icons/fa'
import Header from '../components/Header'
import Footer from 'components/Footer'
import axios from 'axios'

const AdminPage = () => {
  const [stats, setStats] = useState([
    { title: 'Productos', value: 0, icon: <FaBoxes className="text-2xl" /> },
    { title: 'Usuarios', value: 0, icon: <FaUsers className="text-2xl" /> },
    { title: 'Citas hoy', value: 0, icon: <FaCalendarAlt className="text-2xl" /> },
    { title: 'Ventas', value: '$0', icon: <FaFileInvoiceDollar className="text-2xl" /> }
  ])

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [productsRes, usersRes, salesRes, appointmentsRes] = await Promise.all([
          axios.get('https://api.vetfriends.customcodecr.com/api/v1/Product'),
          axios.get('https://api.vetfriends.customcodecr.com/api/v1/User'),
          axios.get('https://api.vetfriends.customcodecr.com/api/v1/Sale'),
          axios.get('https://api.vetfriends.customcodecr.com/api/v1/Appointment')
        ])

        const today = new Date().toISOString().split('T')[0]
        const citasHoy = appointmentsRes.data.data.filter((a: any) =>
          a.appointmentDate?.startsWith(today)
        )

        const totalVentas = salesRes.data.data.reduce(
          (acc: number, sale: any) => acc + (sale.total || 0),
          0
        )

        setStats([
          { title: 'Productos', value: productsRes.data.data.length, icon: <FaBoxes className="text-2xl" /> },
          { title: 'Usuarios', value: usersRes.data.data.length, icon: <FaUsers className="text-2xl" /> },
          { title: 'Citas hoy', value: citasHoy.length, icon: <FaCalendarAlt className="text-2xl" /> },
          { title: 'Ventas', value: `$${totalVentas.toLocaleString()}`, icon: <FaFileInvoiceDollar className="text-2xl" /> }
        ])
      } catch (error) {
        console.error('Error al cargar estadísticas:', error)
      }
    }

    fetchData()
  }, [])

  const adminCards = [
    {
      title: 'Agregar Productos',
      icon: <FaPlus className="mb-4 text-4xl text-blue-600" />,
      link: '/admin/add-product',
      description: 'Añade nuevos productos al inventario'
    },
    {
      title: 'Gestionar Usuarios',
      icon: <FaUserShield className="mb-4 text-4xl text-green-600" />,
      link: '/admin/users',
      description: 'Administra usuarios y permisos'
    },
    {
      title: 'Gestionar Clientes',
      icon: <FaUserShield className="mb-4 text-4xl text-green-600" />,
      link: '/AddClient',
      description: 'Administra clientes'
    },
    {
      title: 'Control de Vacunas',
      icon: <FaSyringe className="mb-4 text-4xl text-purple-600" />,
      link: '/control-vacunas',
      description: 'Registro y seguimiento de vacunas'
    },
    {
      title: 'Gestión de Citas',
      icon: <FaCalendarAlt className="mb-4 text-4xl text-orange-600" />,
      link: '/Appointments',
      description: 'Administra las citas programadas'
    },
    {
      title: 'Reportes de Ventas',
      icon: <FaChartLine className="mb-4 text-4xl text-red-600" />,
      link: '/SalesReport',
      description: 'Analiza el desempeño de ventas'
    },
    {
      title: 'Servicios Clínicos',
      icon: <FaClinicMedical className="mb-4 text-4xl text-teal-600" />,
      link: '/ServiciosClinicos',
      description: 'Gestiona servicios veterinarios'
    },
    {
      title: 'Agregar Categorías',
      icon: <FaTags className="mb-4 text-4xl text-indigo-600" />,
      link: '/AdminCategories',
      description: 'Administra las categorías de productos'
    },
    {
      title: 'Agregar Vacunas',
      icon: <FaTags className="mb-4 text-4xl text-indigo-600" />,
      link: '/AdminVaccines',
      description: 'Administra las vacunas de la tienda'
    }
  ]

  return (
    <div className="min-h-screen bg-gray-50">
      <Header />

      <main className="p-4 md:p-8">
        <div className="mb-8 rounded-xl bg-gradient-to-r from-blue-600 to-blue-800 p-6 text-white shadow-lg">
          <h1 className="mb-2 text-3xl font-bold">Panel de Administración</h1>
          <p className="opacity-90">
            Bienvenido al centro de control de VetsFriends
          </p>
        </div>

        {/* Estadísticas rápidas actualizadas */}
        <div className="mb-8 grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
          {stats.map((stat, index) => (
            <div
              key={index}
              className="flex items-center rounded-lg bg-white p-6 shadow-md"
            >
              <div className="mr-4 rounded-full bg-blue-50 p-3">
                {stat.icon}
              </div>
              <div>
                <p className="text-sm text-gray-500">{stat.title}</p>
                <p className="text-2xl font-bold">{stat.value}</p>
              </div>
            </div>
          ))}
        </div>

        {/* Tarjetas de administración */}
        <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
          {adminCards.map((card, index) => (
            <Link
              key={index}
              to={card.link}
              className="overflow-hidden rounded-xl bg-white shadow-md transition-shadow duration-300 hover:-translate-y-1 hover:shadow-lg"
            >
              <div className="p-6 text-center">
                <div className="flex justify-center">{card.icon}</div>
                <h3 className="mb-2 text-xl font-semibold text-gray-800">
                  {card.title}
                </h3>
                <p className="text-gray-600">{card.description}</p>
                <button className="mt-4 rounded-lg bg-blue-600 px-4 py-2 text-white transition-colors hover:bg-blue-700">
                  Acceder
                </button>
              </div>
            </Link>
          ))}
        </div>
      </main>

      <Footer />
    </div>
  )
}

export default AdminPage
