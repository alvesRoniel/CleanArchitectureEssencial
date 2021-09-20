using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.ProductsCQRS.Commands;
using CleanArchMvc.Application.ProductsCQRS.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
                throw new Exception($"Entity could not be loaded.");

            return _mapper.Map<IEnumerable<ProductDTO>>(await _mediator.Send(productsQuery));
        }

        
        public async Task<ProductDTO> GetProductsByIdAsync(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity could not be loaded.");

            return _mapper.Map<ProductDTO>(await _mediator.Send(productByIdQuery));
        }

        public async Task<ProductDTO> GetProductsCategoryByIdAsync(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity could not be loaded.");

            return _mapper.Map<ProductDTO>(await _mediator.Send(productByIdQuery));
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            await _mediator.Send(_mapper.Map<ProductCreatCommand>(productDTO));
        }

        public async Task RemoveProductAsync(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);

            if (productRemoveCommand == null)
                throw new Exception($"Entity could not be loaded.");

            await _mediator.Send(productRemoveCommand);
        }

        public async Task UpdateProductAsync(ProductDTO productDTO)
        {
            await _mediator.Send(_mapper.Map<ProductUpdateCommand>(productDTO));
        }
    }
}
