using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));

            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetAllProductiesAsync());
        }

        public async Task<ProductDTO> GetProductsByIdAsync(int? id)
        {
            return _mapper.Map<ProductDTO>(await _productRepository.GetProductByIdAsync(id));
        }

        public async Task<ProductDTO> GetProductsCategoryByIdAsync(int? id)
        {
            return _mapper.Map<ProductDTO>(await _productRepository.GetProductCategoryByIdAsync(id));
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            await _productRepository.CreateProductAsync(_mapper.Map<Product>(productDTO));
        }

        public async Task RemoveProductAsync(int? id)
        {
            var productEntity = _productRepository.GetProductByIdAsync(id).Result;
            await _productRepository.RemoveProductAsync(productEntity);
        }

        public async Task UpdateProductAsync(ProductDTO productDTO)
        {
            await _productRepository.UpdateProductAsync(_mapper.Map<Product>(productDTO));
        }
    }
}
