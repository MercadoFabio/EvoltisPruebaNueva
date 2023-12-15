using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class ProductRepository : Repository<TProduct, Product>, IProductRepository
    {
        public ProductRepository(CatalogContext context, IMapper mapper) : base(context, mapper, "Id")
        {
        }
    }
}
