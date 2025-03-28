import React from 'react'
import logo from '../assets/logod.png'
import AddToCartButton from './AddToCartButton'
import ViewDetailsButton from './ViewDetailsButton'
import ProductSearch from 'components/ProductSerch'
interface Product {
  id: number
  name: string
  description: string
  price: number
  image: string
  category: string
}

const ProductSection: React.FC = () => {
  // Datos de ejemplo para productos
  const products: Product[] = [
    {
      id: 1,
      name: 'Alimento para perros Premium',
      description: 'Alimento balanceado para perros adultos de todas las razas',
      price: 25.99,
      category: 'Alimentos',
      image:
        'https://m.media-amazon.com/images/I/81nGXpRzVVL._AC_UF1000,1000_QL80_.jpg'
    },
    {
      id: 2,
      name: 'Cama para gatos',
      description: 'Cama suave y cómoda para gatos de todos los tamaños',
      price: 35.5,
      category: 'Accesorios',
      image:
        'https://m.media-amazon.com/images/I/71+nTJ8m5XL._AC_UF1000,1000_QL80_.jpg'
    },
    {
      id: 3,
      name: 'Correa ajustable',
      description: 'Correa resistente para perros con ajuste de longitud',
      price: 15.75,
      category: 'Accesorios',
      image:
        'https://m.media-amazon.com/images/I/71Zb9tUvUQL._AC_UF1000,1000_QL80_.jpg'
    },
    {
      id: 4,
      name: 'Juguete interactivo',
      description: 'Juguete para gatos con plumas y sonajero',
      price: 12.99,
      category: 'Juguetes',
      image:
        'https://m.media-amazon.com/images/I/71QYq+jqURL._AC_UF1000,1000_QL80_.jpg'
    },
    {
      id: 5,
      name: 'Shampoo para mascotas',
      description: 'Shampoo hipoalergénico para perros y gatos',
      price: 8.99,
      category: 'Higiene',
      image:
        'https://m.media-amazon.com/images/I/61+Q6VdLvYL._AC_UF1000,1000_QL80_.jpg'
    },
    {
      id: 6,
      name: 'Transportadora mediana',
      description: 'Transportadora segura y ventilada para viajes',
      price: 45.0,
      category: 'Accesorios',
      image:
        'https://m.media-amazon.com/images/I/71+9y1mJQVL._AC_UF1000,1000_QL80_.jpg'
    },
    {
      id: 7,
      name: 'Snacks dentales',
      description: 'Snacks para perros que ayudan con la higiene dental',
      price: 10.25,
      category: 'Alimentos',
      image:
        'https://m.media-amazon.com/images/I/81+QY7jKzYL._AC_UF1000,1000_QL80_.jpg'
    },
    {
      id: 8,
      name: 'Rascador para gatos',
      description: 'Rascador de 3 niveles con plataformas y juguetes',
      price: 55.99,
      category: 'Juguetes',
      image:
        'https://m.media-amazon.com/images/I/81+QY7jKzYL._AC_UF1000,1000_QL80_.jpg'
    }
  ]

  return (
    <section className="w-full bg-gray-100 p-4 md:p-8">
      <h2 className="text-center text-4xl font-extrabold text-blue-900 sm:text-4xl sm:tracking-tight lg:text-4xl">
        Nuestros <span className="text-orange-500"> Productos</span>
      </h2>
      <br />
      <ProductSearch />
      <br />
      <div className="w-full px-2 md:px-4">
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-3 md:gap-6 lg:grid-cols-4">
          {products.map((product) => (
            <div
              key={product.id}
              className="flex h-full flex-col rounded-lg bg-white p-4 shadow-md transition-transform duration-300 hover:scale-105"
            >
              <div className="mb-2 flex items-start justify-between">
                <img src={logo} alt="Logo" className="size-6" />
                <span className="rounded-full bg-blue-100 px-2 py-1 text-xs font-semibold text-blue-800">
                  {product.category}
                </span>
              </div>

              <div className="grow">
                <h3 className="mb-2 line-clamp-2 h-14 text-lg font-bold">
                  {product.name}
                </h3>
                <div className="mb-3 flex justify-center">
                  <img
                    src={product.image}
                    alt={product.name}
                    className="size-32 rounded-lg object-contain"
                    onError={(e) => {
                      ;(e.target as HTMLImageElement).src =
                        'https://via.placeholder.com/150'
                    }}
                  />
                </div>
                <p className="mb-2 line-clamp-2 h-12 text-sm text-gray-600">
                  {product.description}
                </p>
                <p className="mb-4 text-lg font-bold text-blue-600">
                  ${product.price.toFixed(2)}
                </p>
              </div>

              <div className="mt-auto flex items-center justify-between">
                <AddToCartButton className="mr-2 grow" />
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
