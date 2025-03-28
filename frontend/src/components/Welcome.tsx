import React, { useEffect, useState } from 'react'

const Welcome: React.FC = () => {
  // Lista de imágenes de ejemplo (puedes reemplazar con tus propias imágenes)
  const images = [
    'https://www.muyinteresante.com/wp-content/uploads/sites/5/2023/10/04/651cffc2141bc.jpeg',
    'https://hospitalveterinariodonostia.com/wp-content/uploads/2018/12/6-lugares-donde-puedes-ver-animales-exoticos-6.jpg',
    'https://www.fundacionaquae.org/wp-content/uploads/2018/10/proteger-a-los-animales-1024x654.jpg',
    'https://res.cloudinary.com/worldpackers/image/upload/c_fill,f_jpg,h_600,q_auto,w_900/v1/guides/article_cover/a17ybpewvfjpvggihpcg?_a=BACADKGT',
    'https://www.ecolatras.es/thumb.php?w=600&h=4:3&crop&i=./archivos/blog/f2d5ad83d8e13755c0dc13acffe96794c56e2164.jpg',
    'https://certifiedhumanelatino.org/wp-content/uploads/2019/02/CERTIFIED-HUMANE_Post-blog-1.jpg',
    'https://beorigen.com/wp-content/uploads/2023/03/pexels-pixabay-5145-1-900x500.jpg',
    'https://uvn-brightspot.s3.amazonaws.com/assets/vixes/btg/curiosidades.batanga.com/files/Los-10-animales-m%C3%A1s-grandes-del-mundo-6.jpg',
    'https://igualdadanimal.mx/app/uploads/2024/04/cropped-preview_animales-constitucion_1200x6302024.jpg'
  ]

  // Estado para almacenar las imágenes seleccionadas
  const [selectedImages, setSelectedImages] = useState<string[]>([])

  // Función para seleccionar 3 imágenes aleatorias
  const getRandomImages = () => {
    const shuffled = [...images].sort(() => 0.5 - Math.random())
    setSelectedImages(shuffled.slice(0, 3))
  }

  // Ejecutar la función al cargar el componente
  useEffect(() => {
    getRandomImages()
  }, [])

  return (
    <section className="flex h-[500px] flex-col items-center justify-between bg-gray-50 p-8 md:flex-row">
      <div className="flex flex-col items-center justify-center text-center md:w-1/2">
        <h1 className="text-4xl font-extrabold text-blue-900 sm:text-5xl sm:tracking-tight lg:text-6xl">
          Bienvenido a <span className="text-orange-500">VetsFriends</span>
        </h1>
        <br></br>
        <p className="text-lg leading-relaxed text-gray-700">
          En nuestra página, nos dedicamos a brindar el mejor cuidado para tus
          mascotas. Ofrecemos servicios de calidad, productos premium y un
          equipo de expertos que aman a los animales tanto como tú. ¡Explora
          nuestras secciones y descubre todo lo que tenemos para ofrecer!
        </p>
      </div>
      <div className="flex items-center justify-center space-x-4 md:w-1/2">
        {selectedImages.map((image, index) => (
          <div
            key={index}
            className="relative size-64 overflow-hidden rounded-lg shadow-md transition-transform hover:z-10 hover:scale-110"
          >
            <img
              src={image}
              alt={`Imagen ${index + 1}`}
              className="size-full object-cover"
            />
          </div>
        ))}
      </div>
    </section>
  )
}

export default Welcome
