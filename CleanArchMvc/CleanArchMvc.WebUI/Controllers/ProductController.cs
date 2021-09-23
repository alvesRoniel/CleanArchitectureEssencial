using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }


        /// <summary>
        /// Lista todos os produtos
        /// </summary>
        /// <returns>Uma lista com todos os produtos cadastrados</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productsDTO = await _productService.GetAllProductsAsync();

            return View(productsDTO);
        }

        /// <summary>
        /// Retorna a view com o formulário para cadastrar o produto
        /// </summary>
        /// <returns>A tela com o formulario</returns>
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
                new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            return View();
        }

        /// <summary>
        /// Realiza o cadastro o Produto
        /// </summary>
        [HttpPost] 
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                await _productService.AddProductAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        /// <summary>
        /// Carrega um produto para realizar a edição.
        /// </summary>
        /// <param name="id">id do produto para ser editado</param>
        /// <returns>Um obj produto</returns>
        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var productsDto = await _productService.GetProductsByIdAsync(id);

            if (productsDto == null) return NotFound();

            var categoriesDto = await _categoryService.GetAllCategoriesAsync();

            ViewBag.CategoryId = new SelectList(categoriesDto, "Id", "Name", productsDto.CategoryId);

            return View(productsDto);
        }

        /// <summary>
        /// Realiza a edição de um produto
        /// </summary>
        /// <param name="productDTO">Obj produto a ser editdo</param>
        /// <returns>Obj Produto com os dados atualizados</returns>
        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(productDTO);
        }

        /// <summary>
        /// Metodo resposanvel por recuperar o produto pelo Id e apresentar o view.
        /// </summary>
        /// <param name="id">Id do Produto</param>
        /// <returns>O obj product </returns>
        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productDto = await _productService.GetProductsByIdAsync(id);

            if (productDto == null) return NotFound();

            return View(productDto);
        }

        /// <summary>
        /// Realiza a deleção de um produto
        /// </summary>
        /// <param name="id">id do produto a ser deletado</param>
        /// <returns></returns>
        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.RemoveProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
