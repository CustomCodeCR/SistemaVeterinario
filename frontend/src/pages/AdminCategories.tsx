import React, { useState, useEffect } from 'react'
import axios from 'axios'
import { FaEdit, FaTrash } from 'react-icons/fa'
import Header from '../components/Header'
import Footer from '../components/Footer'
import Swal from 'sweetalert2'

interface Category {
  categoryId?: number
  categoryName: string
  description: string
  state: number
  auditCreateUser: number
}

const AdminCategories: React.FC = () => {
  const [categories, setCategories] = useState<Category[]>([])
  const [modalOpen, setModalOpen] = useState(false)
  const [search, setSearch] = useState('')
  const [editingCategory, setEditingCategory] = useState<Category | null>(null)
  const [newCategory, setNewCategory] = useState<Category>({
    categoryName: '',
    description: '',
    state: 1,
    auditCreateUser: 90 // Valor quemado aquí
  })

  useEffect(() => {
    fetchCategories()
  }, [])

  const fetchCategories = async () => {
    try {
      const res = await axios.get(
        'https://api.vetfriends.customcodecr.com/api/v1/ProductCategory'
      )
      setCategories(res.data.data || res.data)
    } catch (err) {
      console.error(err)
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Error al obtener categorías.'
      })
    }
  }

  const handleNewCategoryChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target
    setNewCategory((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    try {
      const payload = {
        ...newCategory,
        state: Number(newCategory.state),
        auditCreateUser: 90 // Valor quemado aquí también
      }

      if (editingCategory) {
        await axios.put(
          `https://api.vetfriends.customcodecr.com/api/v1/ProductCategory/Update/${editingCategory.categoryId}`,
          {
            ...payload,
            auditUpdateUser: 90 // Valor quemado para actualización
          },
          {
            headers: {
              'Content-Type': 'application/json',
              Accept: '*/*'
            }
          }
        )

        Swal.fire({
          icon: 'success',
          title: 'Categoría actualizada',
          showConfirmButton: false,
          timer: 1500
        })
      } else {
        const res = await axios.post(
          'https://api.vetfriends.customcodecr.com/api/v1/ProductCategory/Create',
          payload,
          {
            headers: {
              'Content-Type': 'application/json',
              Accept: '*/*'
            }
          }
        )

        if (res.data.isSuccess) {
          Swal.fire({
            icon: 'success',
            title: 'Categoría creada',
            text: `Nombre: ${newCategory.categoryName}`,
          })
        } else {
          throw new Error(res.data.message || 'Error al crear categoría')
        }
      }

      fetchCategories()
      setModalOpen(false)
      setNewCategory({
        categoryName: '',
        description: '',
        state: 1,
        auditCreateUser: 90 // Valor quemado al resetear
      })
      setEditingCategory(null)
    } catch (err: any) {
      console.error(err)
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: err.message || 'Error al enviar la solicitud.'
      })
    }
  }

  const handleEdit = (category: Category) => {
    setEditingCategory(category)
    setNewCategory({
      categoryName: category.categoryName,
      description: category.description,
      state: category.state,
      auditCreateUser: 90 // Valor quemado al editar
    })
    setModalOpen(true)
  }

  const handleDelete = async (id: number) => {
    const result = await Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción eliminará la categoría permanentemente.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    })

    if (!result.isConfirmed) return

    try {
      await axios.delete(`https://api.vetfriends.customcodecr.com/api/v1/ProductCategory/Delete/${id}`, {
        data: { auditDeleteUser: 90 } // Valor quemado para eliminación
      })
      fetchCategories()
      Swal.fire({
        icon: 'success',
        title: 'Categoría eliminada correctamente',
        showConfirmButton: false,
        timer: 1500
      })
    } catch (error: any) {
      console.error("Error deleting category:", error)
      if (error.response) {
        Swal.fire({
          icon: 'error',
          title: 'Error al eliminar',
          text: error.response.data.message || 'Error al eliminar la categoría'
        })
      }
    }
  }

  const filteredCategories = categories.filter(category =>
    category.categoryName.toLowerCase().includes(search.toLowerCase()) ||
    category.description.toLowerCase().includes(search.toLowerCase())
  )

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      <Header />
      <main className="flex-grow p-6 md:p-10">
        <div className="max-w-6xl mx-auto bg-white p-6 rounded-lg shadow-lg">
          <h2 className="text-3xl font-bold text-blue-600 mb-4">Gestión de Categorías</h2>

          <div className="flex items-center justify-between mb-4">
            <input
              type="text"
              placeholder="Buscar categorías..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="w-full md:w-1/2 p-2 border rounded"
            />
            <button
              className="ml-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
              onClick={() => {
                setEditingCategory(null)
                setNewCategory({ categoryName: '', description: '', state: 1, auditCreateUser: 90 })
                setModalOpen(true)
              }}
            >
              + Nueva Categoría
            </button>
          </div>

          <table className="w-full border-collapse border border-gray-300">
            <thead>
              <tr className="bg-gray-200">
                <th className="border p-2">Nombre</th>
                <th className="border p-2">Descripción</th>
                <th className="border p-2">Estado</th>
                <th className="border p-2">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {filteredCategories.map((category) => (
                <tr key={category.categoryId} className="text-center border">
                  <td className="border p-2">{category.categoryName}</td>
                  <td className="border p-2">{category.description}</td>
                  <td className="border p-2">
                    <span
                      className={`px-2 py-1 rounded-full text-white text-sm ${
                        category.state === 1 ? 'bg-green-500' : 'bg-red-500'
                      }`}
                    >
                      {category.state === 1 ? 'Activo' : 'Inactivo'}
                    </span>
                  </td>
                  <td className="border p-2 flex justify-center space-x-4">
                    <button
                      onClick={() => handleEdit(category)}
                      className="text-blue-600 hover:text-blue-800"
                    >
                      <FaEdit />
                    </button>
                    <button
                      onClick={() =>
                        category.categoryId && handleDelete(category.categoryId)
                      }
                      className="text-red-600 hover:text-red-800"
                    >
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
              {filteredCategories.length === 0 && (
                <tr>
                  <td colSpan={4} className="text-center p-4">
                    No hay categorías registradas.
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
            <h3 className="text-2xl font-bold text-center text-blue-600">
              {editingCategory ? 'Editar Categoría' : 'Nueva Categoría'}
            </h3>
            <form onSubmit={handleSubmit} className="mt-4 space-y-4">
              <input
                type="text"
                name="categoryName"
                placeholder="Nombre"
                value={newCategory.categoryName}
                onChange={handleNewCategoryChange}
                required
                className="w-full p-2 border rounded"
              />
              <input
                type="text"
                name="description"
                placeholder="Descripción"
                value={newCategory.description}
                onChange={handleNewCategoryChange}
                className="w-full p-2 border rounded"
              />
              <select
                name="state"
                value={newCategory.state}
                onChange={handleNewCategoryChange}
                className="w-full p-2 border rounded"
                required
              >
                <option value={1}>Activo</option>
                <option value={0}>Inactivo</option>
              </select>
              <input
                type="hidden"
                name="auditCreateUser"
                value={90} // Campo oculto con el valor quemado
              />
              <div className="flex justify-between">
                <button
                  type="submit"
                  className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                >
                  {editingCategory ? 'Guardar Cambios' : 'Crear'}
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setModalOpen(false)
                    setEditingCategory(null)
                    setNewCategory({
                      categoryName: '',
                      description: '',
                      state: 1,
                      auditCreateUser: 90
                    })
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
  )
}

export default AdminCategories
