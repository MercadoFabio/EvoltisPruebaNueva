using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Service.Dtos;

namespace Service.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, TCategory>().ReverseMap();
            CreateMap<Product, TProduct>().ReverseMap();
            CreateMap<ProductDto, TProduct>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryDto, TCategory>().ReverseMap();
        }
    }
}
