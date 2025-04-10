import React, { useEffect, useState } from 'react'
import logo from '../assets/logod.png'
import AddToCartButton from './AddToCartButton'
import ViewDetailsButton from './ViewDetailsButton'
import ProductSearch from 'components/ProductSerch'
import Swal from 'sweetalert2'

export interface Product {
  id: number
  name: string
  description: string
  price: number
  image: string
  category: string
  state?: number
  auditCreateUser?: number
}

interface User {
  name: string
  email: string
  role: string
  id: number
}

const ProductSection: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([])
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<string | null>(null)
  const [currentUser, setCurrentUser] = useState<User | null>(null)

  // Obtener productos del API
  const fetchProducts = async () => {
    setIsLoading(true)
    try {
      const response = await fetch(
        'https://api.vetfriends.customcodecr.com/api/v1/Product'
      )
      if (!response.ok) {
        throw new Error('Error al obtener los productos')
      }
      const data = await response.json()
      setProducts(data.data)
    } catch (err) {
      const errorMessage =
        err instanceof Error ? err.message : 'Error desconocido'
      setError(errorMessage)
      showErrorAlert(errorMessage)
    } finally {
      setIsLoading(false)
    }
  }

  useEffect(() => {
    // Obtener usuario del localStorage
    const userData = JSON.parse(localStorage.getItem('userData') || 'null')
    setCurrentUser(userData)

    // Obtener productos
    fetchProducts()
  }, [])

  // Mostrar alerta de error con SweetAlert2
  const showErrorAlert = (message: string) => {
    Swal.fire({
      icon: 'error',
      title: 'Error',
      text: message,
      confirmButtonColor: '#3085d6'
    })
  }

  // Mostrar alerta de éxito con SweetAlert2
  const showSuccessAlert = (message: string) => {
    Swal.fire({
      icon: 'success',
      title: 'Éxito',
      text: message,
      confirmButtonColor: '#3085d6'
    })
  }

  const handleSearch = (term: string) => {
    console.log('Buscando:', term)
    // Aquí podrías aplicar un filtro a `products`
  }

  // Función para manejar agregar al carrito
  const handleAddToCart = (product: Product) => {
    const cartItems = JSON.parse(localStorage.getItem('cartItems') || '[]')

    // Verificar si el producto ya está en el carrito
    const existingItem = cartItems.find((item: any) => item.id === product.id)

    if (existingItem) {
      // Incrementar cantidad si ya existe
      const updatedCart = cartItems.map((item: any) =>
        item.id === product.id ? { ...item, quantity: item.quantity + 1 } : item
      )
      localStorage.setItem('cartItems', JSON.stringify(updatedCart))
    } else {
      // Agregar nuevo producto al carrito
      const newCartItem = {
        id: product.id,
        productId: product.id,
        name: product.name,
        price: product.price,
        quantity: 1,
        image: product.image,
        category: product.category
      }
      localStorage.setItem(
        'cartItems',
        JSON.stringify([...cartItems, newCartItem])
      )
    }

    showSuccessAlert(`${product.name} agregado al carrito`)
  }

  if (isLoading)
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
      </div>
    )

  if (error)
    return (
      <div className="text-center py-8">
        <h3 className="text-red-500 text-xl">Error al cargar productos</h3>
        <p className="text-gray-600">{error}</p>
        <button
          onClick={fetchProducts}
          className="mt-4 bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
        >
          Reintentar
        </button>
      </div>
    )

  return (
    <section className="w-full bg-gray-100 p-4 md:p-8">
      <h2 className="text-center text-4xl font-extrabold text-blue-900 sm:text-4xl sm:tracking-tight lg:text-4xl">
        Nuestros <span className="text-orange-500"> Productos</span>
      </h2>
      <br />
      <ProductSearch onSearch={handleSearch} />
      <br />
      <div className="w-full px-2 md:px-4">
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-3 md:gap-6 lg:grid-cols-4">
          {products
            .filter((p) => p.state !== 0)
            .map((product) => (
              <div
                key={product.id}
                className="flex h-full flex-col rounded-lg bg-white p-4 shadow-md transition-transform duration-300 hover:scale-105"
              >
                <div className="mb-2 flex items-start justify-between">
                  <img src={logo} alt="Logo" className="size-6" />
                  <span className="rounded-full bg-blue-100 px-2 py-1 text-xs font-semibold text-blue-800">
                    {product.category || 'Sin categoría'}
                  </span>
                </div>

                <div className="grow">
                  <h3 className="mb-2 line-clamp-2 h-14 text-lg font-bold">
                    {product.name}
                  </h3>
                  <div className="mb-3 flex justify-center">
                    <img
                      src={product.image || 'https://via.placeholder.com/150'}
                      alt={product.name}
                      className="size-32 rounded-lg object-contain"
                      onError={(e) => {
                        ;(e.target as HTMLImageElement).src =
                          'https://via.placeholder.com/150'
                      }}
                    />
                  </div>
                  <p className="mb-2 line-clamp-2 h-12 text-sm text-gray-600">
                    {product.description || 'Descripción no disponible'}
                  </p>
                  <p className="mb-4 text-lg font-bold text-blue-600">
                    ${product.price?.toFixed(2) || '0.00'}
                  </p>
                </div>

                <div className="mt-auto flex items-center justify-between">
                  <AddToCartButton
                    product={product}
                    className="mr-2 grow"
                    onAddToCart={handleAddToCart}
                  />
                  <ViewDetailsButton className="mr-2 flex grid-rows-1" />
                </div>
              </div>
            ))}
        </div>
      </div>
    </section>
  )
}

export default ProductSection
