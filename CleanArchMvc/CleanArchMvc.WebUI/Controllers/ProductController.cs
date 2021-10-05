using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 IWebHostEnvironment environment
        )
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
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
            if (ModelState.IsValid)
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file.Length > 0)
                    {
                        productDTO.Image = await SaveImage(file);
                    }
                }

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
        public async Task<IActionResult> Edit(ProductDTO productDTO, string image)
        {
            if (ModelState.IsValid)
            {

                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file.Length > 0)
                    {
                        DeleteImage(image);
                        productDTO.Image = await SaveImage(file);
                    }
                }

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
        [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> DeleteConfirmed(int id, string image)
        {
            await _productService.RemoveProductAsync(id);
            DeleteImage(image);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetProductsByIdAsync(id);

            if (productDto == null) return NotFound();

            var wweroot = _environment.WebRootPath;
            var image = Path.Combine(wweroot, "Resources\\Images\\" + productDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDto);
        }

        [NonAction]
        private void DeleteImage(string imageName)
        {
            var wweroot = _environment.WebRootPath;
            var imagePath = Path.Combine(wweroot, "Resources\\Images\\" + imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

        private async Task<string> SaveImage(Microsoft.AspNetCore.Http.IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_environment.ContentRootPath, @"wwwroot/Resources/Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
