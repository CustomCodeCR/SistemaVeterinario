import React from 'react'
import Header from '../components/Header'
import Footer from '../components/Footer'
import ServicioGrooming from 'components/ServicioGrooming'
import ServicioVeterinario from 'components/ServicioVeterinario'

const Services = () => {
  return (
    <div className="relative overflow-hidden bg-white">
      <Header />
      <main className="min-h-screen p-8">
        <h2 className="text-center text-6xl font-extrabold text-blue-900 sm:text-6xl sm:tracking-tight lg:text-6xl">
          Nuestos <span className="text-orange-500"> Servcios</span>
        </h2>
        <br />
        <div className="grid gap-6 md:grid-cols-2">
          <div className="rounded-lg border p-4">
            <ServicioGrooming />
          </div>
          <div className="rounded-lg border p-4">
            <ServicioVeterinario />
          </div>
        </div>
      </main>
      <Footer />
    </div>
  )
}

export default Services
