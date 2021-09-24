using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.ProductsCQRS.Commands;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<ProductDTO, ProductCreatCommand>();
            CreateMap<ProductDTO, ProductUpdateCommand>();
        }
    }
}
