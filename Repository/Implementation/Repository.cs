using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Caching.Distributed;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
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
        public async Task<T> AddAsync(T entityDto)
        {
            T entity = _mapper.Map<T>(entityDto);
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entityDto)
        {
            T entity = _mapper.Map<T>(entityDto);
            this._context.Set<T>().Remove(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> FindByConditionAsyncWithPagination(Expression<Func<T, bool>> expression, int? pageSize, int? page)
        {
            List<T> entityDtoList;
            entityDtoList = _mapper.Map<List<T>>(await _context.Set<T>().Where(expression)
                .Skip(page.HasValue && pageSize.HasValue ? (page.Value - 1) * pageSize.Value : 0)
                .Take(pageSize.Value)
                .ToListAsync());

            return entityDtoList;

        }

        public async Task<List<T>> FindAllAsync()
        {
            return _mapper.Map<List<T>>(await _context.Set<T>().ToListAsync());
        }

        public async Task<List<T>> FindByConditionAsync(Func<T, bool> expression)
        {
            var entities = await _context.Set<T>().ToListAsync();
            var entityDtoList = _mapper.Map<List<T>>(entities.Where(expression));
            return entityDtoList;
        }

        public async Task<T> UpdateAsync(T entityDto)
        {
            T entity = _mapper.Map<T>(entityDto);

            if (_context.Set<T>().Local.Any(entry => ((int)entry.GetType().GetProperty(_idProperty).GetValue(entry)).Equals((int)entityDto.GetType().GetProperty(_idProperty).GetValue(entityDto))))
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return entity;
        }

     
    }
}

