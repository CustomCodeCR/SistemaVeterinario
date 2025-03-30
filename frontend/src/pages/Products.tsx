import Header from '../components/Header'
import Footer from '../components/Footer'
import BrandSection from '../components/BrandSection'
import NuestrosProductos from '../components/NuestrosProductos'

const Products = () => {
  return (
    <div className="relative overflow-hidden bg-white">
      <Header />
      <BrandSection />
      <NuestrosProductos />
      <Footer />
    </div>
  )
}

export default Products
