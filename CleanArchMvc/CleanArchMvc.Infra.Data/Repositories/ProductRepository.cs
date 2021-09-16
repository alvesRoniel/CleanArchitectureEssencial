using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;

        public ProductRepository(ApplicationDbContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductiesAsync() 
        {
            return await _productContext.DbProducts.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            return await _productContext.DbProducts.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetProductCategoryByIdAsync(int? id)
        {

            //eager loading
            return await _productContext.DbProducts
                .Include(c => c.Category)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Product> RemoveProductAsync(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _productContext.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }
    }
}
