using System.Linq;

namespace Magisterka.Database
{
    using System.Data.Entity;

    using Magisterka.Models;

    public class GenericRepository<TEntity> where TEntity : BaseModel
    {
        #region Properties

        public Entities dbContext { get; set; }

        public Repository _repository { get; set; }

        #endregion

        #region Fields

        protected IDbSet<TEntity> _dbSet;
        protected Repository _repositories;

        #endregion

        #region Constructors

        public GenericRepository(Entities context)
        {
            this.dbContext = context;
            this._dbSet = context.Set<TEntity>();
            this._repositories = new Repository(this.dbContext);
        }

        public GenericRepository(IDbSet<TEntity> iDbSet)
        {
            this._dbSet = iDbSet;
        }

        #endregion

        public virtual void Add(TEntity entity, bool isActive = true, bool saveContext = true)
        {
            this._dbSet.Add(entity);

            if (this.dbContext != null && saveContext)
            {
                this.dbContext.SaveChanges();
            }
        }

        public virtual void Delete(int id, bool saveContext = true)
        {
            TEntity entityToDelete = this._dbSet.Find(id);

            Delete(entityToDelete, saveContext);
        }

        public virtual void Delete(TEntity entityToDelete, bool saveContext = true)
        {
            if (this.dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                this._dbSet.Attach(entityToDelete);
            }
            if (this.dbContext != null && saveContext)
            {
                this.dbContext.SaveChanges();
            }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return this._dbSet.Select(x => x);
        }

        public virtual TEntity GetByID(int id)
        {
            return this._dbSet.Find(id);
        }

        public virtual void Update(TEntity entityToUpdate, bool saveContext = true)
        {
            var entry = dbContext.Entry<TEntity>(entityToUpdate);
            if (entry.State == EntityState.Detached)
            {
                var attachedEntity = dbContext.Set<TEntity>().Local.FirstOrDefault(x => x.Id == entityToUpdate.Id);

                if (attachedEntity != null)
                {
                    // Entity exists in State Manager
                    var attachedEntry = dbContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entityToUpdate);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
            else
            {
                entry.State = EntityState.Modified;
            }

            if (this.dbContext != null && saveContext)
            {
                this.dbContext.SaveChanges();
            }
        }

        public virtual void UpdateCreate(TEntity entityToUpdate, bool saveContext = true)
        {
            if (entityToUpdate.Id == 0)
            {
                Add(entityToUpdate, saveContext);
            }
            else
            {
                Update(entityToUpdate, saveContext);
            }
        }
    }
}