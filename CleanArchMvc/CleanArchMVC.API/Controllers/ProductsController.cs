using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Return all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProduct()
        {
            var product = await _productService.GetAllProductsAsync();
            if (product == null) return NotFound("Product not found");

            return Ok(product);
        }

        /// <summary>
        /// Returns a product by id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductsByIdAsync(id);
            if (product == null) return NotFound("Product not found");

            return Ok(product);
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <remarks>
        /// Requisição
        ///     { 
        ///         "name": "Product teste", 
        ///         "description": "Description product test", 
        ///         "price": 25.00, 
        ///         "stok": 10, 
        ///         "image": "productTeste.jpg", 
        ///         "categoryId": 1
        ///     }
        /// </remarks>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddProduct(ProductDTO productDTO)
        {
            if (productDTO == null) return BadRequest("Invalid Data");
            productDTO = await _productService.AddProductAsync(productDTO);
            return CreatedAtAction(nameof(GetProductById), new { id = productDTO.Id }, productDTO);
        }

       /// <summary>
       ///  Update product
       /// </summary>
       /// <param name="id">Product id</param>
       /// <param name="productDTO"></param>
       /// <returns></returns>
        [HttpPut("id:int")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id || productDTO == null) return BadRequest("Invalid Data");

            await _productService.UpdateProductAsync(productDTO);

            return Ok(productDTO);
        }

        /// <summary>
        /// Delete product 
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
        {
            var prodct = await _productService.GetProductsByIdAsync(id);
            if (prodct == null) return NotFound("Product not found");

            await _productService.RemoveProductAsync(id);

            return Ok(prodct);
        }
    }
}
