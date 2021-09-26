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
    }
}
