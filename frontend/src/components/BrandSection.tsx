import React from 'react'
import logo from '../assets/logod.png'
import AddToCartButton from './AddToCartButton'

const BrandSection: React.FC = () => {
  // Datos de ejemplo para las marcas
  const brands = [
    {
      id: 1,
      name: 'Viyo',
      description: 'Viyo Recuperation, el gran avance en recuperación',
      image:
        'data:image/jpeg;base64,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'
    },
    {
      id: 2,
      name: 'Advantix',
      description: 'Pipeta antiparasitaria para perros Advantix',
      image:
        'https://cdn.pymesenlared.es/img/8/114/81778/pipetas-advantix-4.JPG'
    },
    {
      id: 3,
      name: 'Animal Planet',
      description: 'Juguete para gato Animal Planet',
      image:
        'https://www.todomascotascr.com/1317-home_default/juguete-para-gato-animal-planet.jpg'
    },
    {
      id: 4,
      name: 'Kong Tikr',
      description: 'Juguete para perro Kong Tikr',
      image:
        'https://static1.lucasylola.es/16309-large_default/juguete-para-perros-kong-tikr.jpg'
    }
  ]

  return (
    <section className="bg-gray-100 p-8">
      <h2 className="text-center text-4xl font-extrabold text-blue-900 sm:text-4xl sm:tracking-tight lg:text-4xl">
        Mejores <span className="text-orange-500"> Marcas</span>
      </h2>
      <br />
      <div className="flex justify-center">
        <div className="grid max-w-6xl grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-4">
          {brands.map((brand) => (
            <div
              key={brand.id}
              className="relative flex h-96 flex-col justify-between rounded-lg bg-white p-6 shadow-md"
            >
              <div className="absolute left-4 top-4">
                <img src={logo} alt="Logo" className="size-8" />
              </div>
              <div>
                <h3 className="mt-12 text-xl font-bold">{brand.name}</h3>
                <div className="my-4 flex justify-center">
                  <img
                    src={brand.image}
                    alt={brand.name}
                    className="size-32 rounded-lg object-cover"
                  />
                </div>
                <p className="mt-2 text-gray-600">{brand.description}</p>
              </div>
              <AddToCartButton />
            </div>
          ))}
        </div>
      </div>
    </section>
  )
}

export default BrandSection
