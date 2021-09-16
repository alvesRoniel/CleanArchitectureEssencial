using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _categoryRepository.GetAllCategoriesAsync());
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int? id)
        {
            return _mapper.Map<CategoryDTO>(await _categoryRepository.GetCategoryByIdAsync(id));
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDto)
        {
            var categoriEntity = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.CreateCategoryAsync(categoriEntity);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            var categoriEntity = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateCategoryAsync(categoriEntity);
        }

        public async Task RemoveCategoryAsync(int? id)
        {
            var categoriEntity = _categoryRepository.GetCategoryByIdAsync(id).Result;
            await _categoryRepository.RemoveCategoryAsync(categoriEntity);
        }
    }
}
