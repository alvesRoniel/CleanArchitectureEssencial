using CleanArchMvc.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Retorna uma lista de todos os produtos cadastrados
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        
        /// <summary>
        /// Retorna um único produto cadastrado
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns></returns>
        Task<ProductDTO> GetProductsByIdAsync(int? id);

        /// <summary>
        /// Retorno um produto e sua categoria
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns></returns>
        Task<ProductDTO> GetProductsCategoryByIdAsync(int? id);

        /// <summary>
        /// Adicionar um novo produto
        /// </summary>
        /// <param name="productDTO">Produto a ser adicionado</param>
        /// <returns></returns>
        Task AddProductAsync(ProductDTO productDTO);

        /// <summary>
        /// Atualiza um produto existente
        /// </summary>
        /// <param name="productDTO">Produto a ser atualizado</param>
        /// <returns></returns>
        Task UpdateProductAsync(ProductDTO productDTO);

        /// <summary>
        /// Remove um produto da base de dados
        /// </summary>
        /// <param name="id">id do produto a ser removido</param>
        /// <returns></returns>
        Task RemoveProductAsync(int? id);
    }
}
