import React, { useState } from 'react'
import { FiSearch } from 'react-icons/fi'

interface ProductSearchProps {
  onSearch: (searchTerm: string) => void
  placeholder?: string
  className?: string
}

const ProductSearch: React.FC<ProductSearchProps> = ({
  onSearch,
  placeholder = 'Buscar productos...',
  className = ''
}) => {
  const [searchTerm, setSearchTerm] = useState('')

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault()
    onSearch(searchTerm)
  }

  return (
    <form
      onSubmit={handleSearch}
      className={`mx-auto w-full max-w-md ${className}`}
    >
      <div className="relative">
        <input
          type="text"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          placeholder={placeholder}
          className="w-full rounded-lg border border-gray-300 px-4 py-2 pr-10 focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <button
          type="submit"
          className="absolute right-2 top-1/2 -translate-y-1/2 text-gray-500 hover:text-blue-600"
        >
          <FiSearch size={20} />
        </button>
      </div>
    </form>
  )
}

export default ProductSearch
