using Indigo.Data;
using Indigo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Constructors

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            Entities = Context.Set<TEntity>();
        }

        #endregion

        #region Properties

        protected ApplicationDbContext Context { get; set; }

        protected DbSet<TEntity> Entities { get; set; }

        #endregion

        #region Public Methods

        public virtual async Task<TEntity?> FindAsync(params object[] keyValues)
        {
            return await Entities.FindAsync(keyValues);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            Entities.Add(entity);

            await Context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var addRangeAsync = entities.ToList();
            Entities.AddRange(addRangeAsync);

            await Context.SaveChangesAsync();

            return addRangeAsync;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }

            return Context.SaveChangesAsync();
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            Entities.Remove(entity);
            return Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(params object[] keyValues)
        {
            var entity = await Entities.FindAsync(keyValues);

            if (entity == null)
            {
                throw new ArgumentException("Entity not found.");
            }

            await DeleteAsync(entity);
        }

        public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
            return Context.SaveChangesAsync();
        }
        #endregion
    }
}
