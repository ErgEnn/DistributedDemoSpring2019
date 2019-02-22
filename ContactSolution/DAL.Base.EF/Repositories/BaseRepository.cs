using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> All()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // id - primary key or composite primary key components in right order
        public TEntity Find(params object[] id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> FindAsync(params object[] id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(params object[] id)
        {
            _dbSet.Remove(Find(id));
        }

        //TODO: these methods should not be here - UOW!! 
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}