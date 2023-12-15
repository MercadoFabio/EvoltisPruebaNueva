using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class CategoryRepository : Repository<TCategory, Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogContext context, IMapper mapper) : base(context, mapper, "Id")
        {
        }
    }

}
