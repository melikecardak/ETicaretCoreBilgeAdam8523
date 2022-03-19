using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCore.DataAccess.EntityFramework.Bases
{
    public abstract class RepoBase<TEntity, TDbContext> : IRepoBase<TEntity, TDbContext> where TEntity : class, new() where TDbContext : DbContext, new()
    {
        public TDbContext DbContext { get; set; }

        protected RepoBase()
        {
            DbContext = new TDbContext();
        }

        protected RepoBase(TDbContext dbContext)
        {
            DbContext = dbContext; 
        }

        public void Add(TEntity entity, bool save = true)
        {
            DbContext.Set<TEntity>().Add(entity);
            if (save)
                Save();
        }

        public void Delete(TEntity entity, bool save = true)
        {
            DbContext.Set<TEntity>().Remove(entity);
            if (save)
                Save();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
        {
            var entities = DbContext.Set<TEntity>().Where(predicate).ToList();
            foreach (var entity in entities)
            {
                Delete(entity, save);
            }
        }

        public void Dispose()
        {
            DbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> Query(params string[] entitiesToInclude)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            try
            {
                return DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(TEntity entity, bool save = true)
        {
            throw new NotImplementedException();
        }
    }
}
