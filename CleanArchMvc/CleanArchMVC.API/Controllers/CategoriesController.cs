using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Return all categories
        /// </summary>
        /// <returns>A list of all categories</returns>
        /// <response code="404">If categories are not found</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null) return NotFound("Categories not found");

            return Ok(categories);
        }

        /// <summary>
        /// Returns a category by id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>A polluted category object</returns>
        /// <response code="404">If categories are not found</response> 
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null) return NotFound("Category not found");

            return Ok(category);

        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <remarks>
        /// Requisição
        /// {
        ///    "name": "Teste de Inclusão"
        /// }
        /// </remarks>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) return BadRequest("Invalid Data");
            categoryDTO = await _categoryService.AddCategoryAsync(categoryDTO);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDTO.Id }, categoryDTO);
        }

        /// <summary>
        /// Upadate Catgory
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id || categoryDTO == null) return BadRequest();

            await _categoryService.UpdateCategoryAsync(categoryDTO);

            return Ok(categoryDTO);
        }


        /// <summary>
        /// Remove Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return  NotFound("Category not found");

            await _categoryService.RemoveCategoryAsync(id);

            return Ok(category);
        }

    }
}
