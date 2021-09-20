using CleanArchMvc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// Retorna todas os produtos
        /// </summary>
        /// <returns>Uma lista com todos os produtos</returns>
        Task<IEnumerable<Product>> GetAllProductiesAsync();

        /// <summary>
        /// Retorna apenas um produto
        /// </summary>
        /// <returns></returns>
        Task<Product> GetProductByIdAsync(int? id);

        /// <summary>
        /// Retorna apenas uma produto pelo id informado
        /// </summary>
        /// <param name="id">Id da produto</param>
        /// <returns>Um objeto do tipo produto</returns>
        //Task<Product> GetProductCategoryByIdAsync(int? id);

        /// <summary>
        /// Cria um novo objeto do tipo produto
        /// </summary>
        /// <param name="product">Objeto a ser cirado</param>
        /// <returns>Objeto recém criado</returns>
        Task<Product> CreateProductAsync(Product product);

        /// <summary>
        /// Atualiza um novo objeto do tipo produto
        /// </summary>
        /// <param name="product">Objeto a ser atualizado</param>
        /// <returns>Objeto atualizado</returns>
        Task<Product> UpdateProductAsync(Product product);

        /// <summary>
        /// Objeto a ser deletado
        /// </summary>
        /// <param name="product">Objeto a ser deletado</param>
        /// <returns>Objeto deletado</returns>
        Task<Product> RemoveProductAsync(Product product);
    }
}
