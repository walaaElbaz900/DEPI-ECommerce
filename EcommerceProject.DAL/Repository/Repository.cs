using ECommerce.Data;
using EcommerceProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.DAL.Repository
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private readonly EcommerceContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(EcommerceContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            _dbSet.Remove(GetById(id));
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> QuerableGetAll()
        {
            return _context.Set<T>();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
