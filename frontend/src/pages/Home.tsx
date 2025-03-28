import React from 'react'
import Header from '../components/Header'
import Welcome from '../components/Welcome'
import BrandsSection from '../components/BrandSection'
import ServicioGrooming from '../components/ServicioGrooming'
import ServicioVeterinario from '../components/ServicioVeterinario'
import Footer from '../components/Footer'

const Home = () => {
  return (
    <div className="relative overflow-hidden bg-white">
      <Header />
      <Welcome />
      <BrandsSection />
      <ServicioGrooming />
      <ServicioVeterinario />
      <Footer />
    </div>
  )
}

export default Home
