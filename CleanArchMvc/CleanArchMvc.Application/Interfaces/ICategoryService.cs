using CleanArchMvc.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        //// Retorna todas as categorias
        //// </summary>
        //// <returns>Uma lista com todoas as categorias</returns>
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();

        /// <summary>
        /// Retorna apenas uma categoria pelo id informado
        /// </summary>
        /// <param name="id">Id da categoria</param>
        /// <returns>Um objeto do tipo categoria</returns>
        Task<CategoryDTO> GetCategoryByIdAsync(int? id);

        /// <summary>
        /// Adiciona uma nova categoria no banco
        /// </summary>
        /// <param name="categoryDto">Ojeto a ser criado</param>
        /// <returns></returns>
        Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto);

        /// <summary>
        /// Atualiza os dados de uma categoria existente
        /// </summary>
        /// <param name="categoryDto">Ojeto a ser atualizado</param>
        /// <returns></returns>
        Task UpdateCategoryAsync(CategoryDTO categoryDto);

        /// <summary>
        /// Remove uma categoria do banco de dados
        /// </summary>
        /// <param name="id">Id da categoria a ser deletada</param>
        /// <returns></returns>
        Task RemoveCategoryAsync(int? id);
    }
}
