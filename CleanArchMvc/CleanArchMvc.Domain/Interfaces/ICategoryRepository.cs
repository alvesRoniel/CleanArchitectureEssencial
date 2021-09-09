using CleanArchMvc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Retorna todas as categorias
        /// </summary>
        /// <returns>Uma lista com todoas as categorias</returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        /// <summary>
        /// Retorna apenas uma categoria pelo id informado
        /// </summary>
        /// <param name="id">Id da categoria</param>
        /// <returns>Um objeto do tipo categoria</returns>
        Task<Category> GetCategoryByIdAsync(int? id);

        /// <summary>
        /// Cria um novo objeto do tipo categoria
        /// </summary>
        /// <param name="category">Objeto a ser cirado</param>
        /// <returns>Objeto recém criado</returns>
        Task<Category> CreateCategoryAsync(Category category);

        /// <summary>
        /// Atualiza um novo objeto do tipo categoria
        /// </summary>
        /// <param name="category">Objeto a ser atualizado</param>
        /// <returns>Objeto atualizado</returns>
        Task<Category> UpdateCategoryAsync(Category category);

        /// <summary>
        /// Objeto a ser deletado
        /// </summary>
        /// <param name="category">Objeto a ser deletado</param>
        /// <returns>Objeto deletado</returns>
        Task<Category> RemoveCategoryAsync(Category category);
    }
}
