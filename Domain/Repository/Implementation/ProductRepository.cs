using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Domain.Repository.Interfaces;

namespace Domain.Repository.Implementation
{
    public class ProductRepository : Repository<TProduct, Product>, IProductRepository
    {
        public ProductRepository(CatalogContext context, IMapper mapper) : base(context, mapper, "Id")
        {
        }
    }
}
