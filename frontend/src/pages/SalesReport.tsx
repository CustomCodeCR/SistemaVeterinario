import React, { useState, useEffect } from 'react'
import {
  FaCalendarAlt,
  FaFilter,
  FaFileExport,
  FaMoneyBillWave,
  FaShoppingCart,
  FaUsers,
  FaSort,
  FaSortUp,
  FaSortDown
} from 'react-icons/fa'
import Header from '../components/Header'
import Footer from 'components/Footer'

interface Sale {
  id: string
  date: string
  customer: string
  products: {
    id: string
    name: string
    quantity: number
    price: number
  }[]
  total: number
  paymentMethod: string
}

const SalesReportPage = () => {
  const [sales, setSales] = useState<Sale[]>([])
  const [filteredSales, setFilteredSales] = useState<Sale[]>([])
  const [dateRange, setDateRange] = useState({
    start: new Date(new Date().setMonth(new Date().getMonth() - 1)).toISOString().split('T')[0],
    end: new Date().toISOString().split('T')[0]
  })
  const [sortConfig, setSortConfig] = useState<{ key: keyof Sale; direction: 'ascending' | 'descending' } | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  // Datos de ejemplo
  useEffect(() => {
    const fetchSalesData = async () => {
      setIsLoading(true)
      // Simulación de llamada a API
      setTimeout(() => {
        const mockSales: Sale[] = [
          {
            id: '1',
            date: '2023-06-01',
            customer: 'Juan Pérez',
            products: [
              { id: '101', name: 'Alimento para perros Premium', quantity: 2, price: 15000 },
              { id: '102', name: 'Shampoo para mascotas', quantity: 1, price: 8000 }
            ],
            total: 38000,
            paymentMethod: 'tarjeta'
          },
          {
            id: '2',
            date: '2023-06-05',
            customer: 'María Gómez',
            products: [
              { id: '103', name: 'Juguete para gato', quantity: 3, price: 5000 }
            ],
            total: 15000,
            paymentMethod: 'efectivo'
          },
          {
            id: '3',
            date: '2023-06-10',
            customer: 'Carlos Rodríguez',
            products: [
              { id: '101', name: 'Alimento para perros Premium', quantity: 1, price: 15000 },
              { id: '104', name: 'Correa ajustable', quantity: 2, price: 7000 }
            ],
            total: 29000,
            paymentMethod: 'transferencia'
          }
        ]
        setSales(mockSales)
        setFilteredSales(mockSales)
        setIsLoading(false)
      }, 1000)
    }

    fetchSalesData()
  }, [])

  // Aplicar filtros
  useEffect(() => {
    const filtered = sales.filter(sale => {
      const saleDate = new Date(sale.date)
      const startDate = new Date(dateRange.start)
      const endDate = new Date(dateRange.end)
      return saleDate >= startDate && saleDate <= endDate
    })
    setFilteredSales(filtered)
  }, [dateRange, sales])

  // Ordenar tabla
  const requestSort = (key: keyof Sale) => {
    let direction: 'ascending' | 'descending' = 'ascending'
    if (sortConfig && sortConfig.key === key && sortConfig.direction === 'ascending') {
      direction = 'descending'
    }
    setSortConfig({ key, direction })
  }

  const getSortIcon = (key: keyof Sale) => {
    if (!sortConfig || sortConfig.key !== key) {
      return <FaSort className="ml-1 inline opacity-50" />
    }
    return sortConfig.direction === 'ascending'
      ? <FaSortUp className="ml-1 inline" />
      : <FaSortDown className="ml-1 inline" />
  }

  const sortedSales = React.useMemo(() => {
    if (!sortConfig) return filteredSales

    return [...filteredSales].sort((a, b) => {
      if (a[sortConfig.key] < b[sortConfig.key]) {
        return sortConfig.direction === 'ascending' ? -1 : 1
      }
      if (a[sortConfig.key] > b[sortConfig.key]) {
        return sortConfig.direction === 'ascending' ? 1 : -1
      }
      return 0
    })
  }, [filteredSales, sortConfig])

  // Estadísticas
  const totalSales = filteredSales.reduce((sum, sale) => sum + sale.total, 0)
  const totalProductsSold = filteredSales.reduce((sum, sale) =>
    sum + sale.products.reduce((prodSum, prod) => prodSum + prod.quantity, 0), 0)
  const uniqueCustomers = [...new Set(filteredSales.map(sale => sale.customer))].length

  const handleExport = () => {
    // Lógica para exportar a PDF o Excel
    console.log('Exportando reporte...')
    alert('Funcionalidad de exportación se implementará aquí')
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <Header />

      <main className="p-4 md:p-8">
        <div className="mb-6 flex flex-col justify-between md:flex-row md:items-center">
        <h1 className="text-2xl font-extrabold text-blue-900 sm:text-2xl sm:tracking-tight lg:text-2xl">
            Reporte de <span className="text-orange-500">Ventas</span>
        </h1>

        </div>

        {/* Filtros */}
        <div className="mb-6 rounded-lg bg-white p-4 shadow-md">
          <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
            <div>
              <label className="mb-1 block text-sm font-medium text-gray-700">
                <FaCalendarAlt className="inline mr-1" /> Fecha inicial
              </label>
              <input
                type="date"
                value={dateRange.start}
                onChange={(e) => setDateRange({...dateRange, start: e.target.value})}
                className="block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              />
            </div>
            <div>
              <label className="mb-1 block text-sm font-medium text-gray-700">
                <FaCalendarAlt className="inline mr-1" /> Fecha final
              </label>
              <input
                type="date"
                value={dateRange.end}
                onChange={(e) => setDateRange({...dateRange, end: e.target.value})}
                className="block w-full rounded-md border border-gray-300 p-2 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              />
            </div>
          </div>
        </div>

        {/* Resumen estadístico */}
        <div className="mb-6 grid grid-cols-1 gap-4 md:grid-cols-3">
          <div className="rounded-lg bg-white p-4 shadow-md">
            <div className="flex items-center">
              <div className="rounded-full bg-blue-100 p-3 mr-4">
                <FaMoneyBillWave className="text-blue-600 text-xl" />
              </div>
              <div>
                <p className="text-sm text-gray-500">Ventas totales</p>
                <p className="text-2xl font-bold">₡{totalSales.toLocaleString()}</p>
              </div>
            </div>
          </div>
          <div className="rounded-lg bg-white p-4 shadow-md">
            <div className="flex items-center">
              <div className="rounded-full bg-green-100 p-3 mr-4">
                <FaShoppingCart className="text-green-600 text-xl" />
              </div>
              <div>
                <p className="text-sm text-gray-500">Productos vendidos</p>
                <p className="text-2xl font-bold">{totalProductsSold}</p>
              </div>
            </div>
          </div>
          <div className="rounded-lg bg-white p-4 shadow-md">
            <div className="flex items-center">
              <div className="rounded-full bg-purple-100 p-3 mr-4">
                <FaUsers className="text-purple-600 text-xl" />
              </div>
              <div>
                <p className="text-sm text-gray-500">Clientes únicos</p>
                <p className="text-2xl font-bold">{uniqueCustomers}</p>
              </div>
            </div>
          </div>
        </div>

        {/* Tabla de ventas detalladas */}
        <div className="rounded-lg bg-white shadow-md overflow-hidden">
          <h2 className="p-4 text-lg font-semibold text-gray-800">
            Detalle de ventas
          </h2>
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th
                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer"
                    onClick={() => requestSort('date')}
                  >
                    Fecha {getSortIcon('date')}
                  </th>
                  <th
                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer"
                    onClick={() => requestSort('customer')}
                  >
                    Cliente {getSortIcon('customer')}
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Productos
                  </th>
                  <th
                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer"
                    onClick={() => requestSort('total')}
                  >
                    Total {getSortIcon('total')}
                  </th>
                  <th
                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer"
                    onClick={() => requestSort('paymentMethod')}
                  >
                    Pago {getSortIcon('paymentMethod')}
                  </th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-200 bg-white">
                {isLoading ? (
                  <tr>
                    <td colSpan={5} className="px-6 py-4 text-center">
                      <div className="flex justify-center">
                        <div className="animate-spin rounded-full h-8 w-8 border-t-2 border-b-2 border-blue-500"></div>
                      </div>
                    </td>
                  </tr>
                ) : sortedSales.length > 0 ? (
                  sortedSales.map((sale) => (
                    <tr key={sale.id} className="hover:bg-gray-50">
                      <td className="whitespace-nowrap px-6 py-4">
                        {new Date(sale.date).toLocaleDateString('es-ES', {
                          day: '2-digit',
                          month: '2-digit',
                          year: 'numeric'
                        })}
                      </td>
                      <td className="whitespace-nowrap px-6 py-4">
                        {sale.customer}
                      </td>
                      <td className="px-6 py-4">
                        <ul className="list-disc pl-5">
                          {sale.products.map((prod) => (
                            <li key={prod.id} className="text-sm">
                              {prod.name} (x{prod.quantity}) - ₡{prod.price.toLocaleString()}
                            </li>
                          ))}
                        </ul>
                      </td>
                      <td className="whitespace-nowrap px-6 py-4 font-medium">
                        ₡{sale.total.toLocaleString()}
                      </td>
                      <td className="whitespace-nowrap px-6 py-4 capitalize">
                        {sale.paymentMethod}
                      </td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td colSpan={5} className="px-6 py-4 text-center text-gray-500">
                      No se encontraron ventas en el período seleccionado
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </main>
      <Footer />
    </div>
  )
}

export default SalesReportPage
