using Microsoft.EntityFrameworkCore;
using SiteMapGeneratorDal.dbContext;
using SiteMapGeneratorDal.Infrastructure.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGeneratorDal.Infrastructure.Repository
{
    public class DbRepository<T> : IGetRepository<T>, IDefaultRepository<T>, IGetInclude<T> where T : class
    {
        internal TestTaskForNetDeveloperPositionContext _testTaskForNetDeveloperPositionContext;
        internal DbSet<T> DbSeT;

        public DbRepository(TestTaskForNetDeveloperPositionContext entities)
        {
            _testTaskForNetDeveloperPositionContext = entities;
            DbSeT = entities.Set<T>();
        }

        #region IGetRepository
        public IEnumerable<T> Get()
            => DbSeT.AsEnumerable<T>().AsQueryable().AsNoTracking();

        public T GetById(int? id)
            => DbSeT.Find(id);

        #endregion

        #region IDefaultRepository
        public virtual void Insert(T entity)
        {
            DbSeT.Add(entity);
        }

        public void Insert(List<T> entity)
        {
                DbSeT.AddRange(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = DbSeT.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_testTaskForNetDeveloperPositionContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSeT.Attach(entityToDelete);
            }

            DbSeT.Remove(entityToDelete);
        }
        public virtual void Update(T entityToUpdate)
        {
            _testTaskForNetDeveloperPositionContext.Set<T>().Update(entityToUpdate);
        }
        #endregion

        #region IGetInclude
        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter)
            => DbSeT.Where(filter).AsNoTracking();

        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string children)
            => DbSeT.Include(children).Where(filter).AsNoTracking();

        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string childrenOne, string childrenTwo)
            => DbSeT.Include(childrenOne).Include(childrenTwo).Where(filter).AsNoTracking();

        public IEnumerable<T> GetInclude(string children)
            => DbSeT.Include(children).AsNoTracking().AsEnumerable<T>().AsQueryable();
        #endregion

    }
}
