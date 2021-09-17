using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoruyService;

        public CategoriesController(ICategoryService categoruyService)
        {
            _categoruyService = categoruyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoruyService.GetAllCategoriesAsync();
            return View(categories);
        }
    }
}
