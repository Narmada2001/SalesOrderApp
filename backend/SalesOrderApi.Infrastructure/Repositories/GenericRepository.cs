using Microsoft.EntityFrameworkCore;
using SalesOrderApi.Infrastructure.Data;
using SalesOrderApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesOrderApp.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            // FindAsync doesn't support IQueryable with Include easily without key check.
            // So we use FirstOrDefaultAsync with a key check? 
            // But we don't know the key property name here easily.
            // Actually, for GetById with includes, it's better to use FirstOrDefault.
            // But we need to know the ID property.
            // For simplicity, let's assume 'Id' property exists or use FindAsync if no includes.
            
            if (includes.Length == 0)
            {
                return await _context.Set<T>().FindAsync(id);
            }

            // This is a bit hacky for a generic repo without knowing the key.
            // Let's assume the entity has an 'Id' property of type int.
            // Or we can rely on the caller to use ListAsync with predicate.
            
            // Let's revert GetByIdAsync change in Interface and use ListAsync for complex queries?
            // Or just implement it assuming 'Id' property.
            
            var keyName = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(x => x.Name).Single();
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
