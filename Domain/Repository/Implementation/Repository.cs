using AutoMapper;
using Domain.Models;
using Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repository.Implementation
{
    public abstract class Repository<T, T1> : IRepository<T, T1> where T : class where T1 : class
    {
        private CatalogContext _context { get; set; }
        private IMapper _mapper { get; set; }
        protected string _idProperty { get; set; }


        public Repository(CatalogContext context, IMapper mapper, string idProperty)
        {
            _context = context;
            _mapper = mapper;
            _idProperty = idProperty;
        }


        public async Task<List<T1>> FindAllAsync()
        {
            return _mapper.Map<List<T1>>(await _context.Set<T>().ToListAsync());
        }


        public async Task<T> AddAsync(T1 entityDto)
        {
            T entity = _mapper.Map<T>(entityDto);
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<T> UpdateAsync(T1 entityDto)
        {
            T entity = _mapper.Map<T>(entityDto);
            T local = this._context.Set<T>().Local.FirstOrDefault(entry => ((int)entry.GetType().GetProperty(_idProperty).GetValue(entry)).Equals((int)entityDto.GetType().GetProperty(_idProperty).GetValue(entityDto)));
            if (local != null)
            {
                this._context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T1 entityDto)
        {
            T entity = _mapper.Map<T>(entityDto);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<(List<T1>, int)> FindByConditionAsyncWithPagination(Func<T, bool> expression, int? pageSize, int? page)
        {
            var entities = _context.Set<T>().Where(expression);

            int totalItems =  entities.Count();

            var paginatedEntities = entities;

            if (pageSize.HasValue && page.HasValue)
            {
                paginatedEntities = paginatedEntities.Skip((page.Value - 1) * pageSize.Value)
                                                   .Take(pageSize.Value);
            }

            var entityDtoListMapped =  paginatedEntities.ToList();

            var mappedList = _mapper.Map<List<T1>>(entityDtoListMapped);

            return (mappedList, totalItems);
        }



        public async Task<List<T1>> FindByConditionAsync(Func<T, bool> expression)
        {
            var entities = await _context.Set<T>().ToListAsync();
            var entityDtoList = _mapper.Map<List<T1>>(entities.Where(expression));
            return entityDtoList;
        }

        public List<T1> FindByCondition(Func<T, bool> expression)
        {
            List<T1> entityDtoList;

            entityDtoList = _mapper.Map<List<T1>>(this._context.Set<T>().ToList());
            entityDtoList = _mapper.Map<List<T1>>(_mapper.Map<List<T>>(entityDtoList).Where(expression).ToList());

            return entityDtoList;

        }
    }
}

