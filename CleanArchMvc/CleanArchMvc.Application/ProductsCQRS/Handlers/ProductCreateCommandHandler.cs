using CleanArchMvc.Application.ProductsCQRS.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.ProductsCQRS.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreatCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductCreatCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(
                request.Name,
                request.Description,
                request.Price,
                request.Stok,
                request.Image);

            if (product == null)
                throw new ApplicationException($"Error creating entity");
            else
            {
                product.CategoryId = request.CategoryId;
                return await _productRepository.CreateProductAsync(product);
            }
        }
    }
}
