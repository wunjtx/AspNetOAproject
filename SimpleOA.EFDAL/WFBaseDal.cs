using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.OADAL
{
    public class WFBaseDal<T> where T : class, new()
    {
        private DbContext dbContext
        {
            get
            {
                return DbFactory.GetDbContextByCallContext();
            }
        }
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Where<T>(whereLambda);
        }
        public IQueryable<T> GetEntitiesIndex<S>(int pageIndex, int pageCount, out int Total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc)
        {
            Total = dbContext.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                return dbContext.Set<T>().Where(whereLambda)
                .OrderBy<T, S>(orderbyLambda)
                .Skip(pageCount * (pageIndex))
                .Take(pageCount);
            }
            else
            {
                return dbContext.Set<T>().Where(whereLambda).OrderByDescending<T, S>(orderbyLambda).Skip<T>(pageIndex * (pageCount - 1)).Take(pageCount);
            }
        }
        public T Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return entity;
        }
        public bool Update(T entity)
        {
            dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return true;
        }
        public bool Delete(T entity)
        {
            dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return true;
        }
        public bool Delete(int id)
        {
            var entity = dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                dbContext.Set<T>().Remove(entity);
            }
            return true;
        }
        public bool DeleteByLogical(int id)
        {
            var entity = dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                dbContext.Entry(entity).Property("DelFlag").CurrentValue = (short)SimpleOA.Enum.DelFlagEnum.Deleted;
                dbContext.Entry(entity).Property("DelFlag").IsModified = true;
            }
            return true;
        }

        //更新部分列
        public void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            var dbEntityEntry = dbContext.Entry(entity);

            if (updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
                {
                    var original = dbEntityEntry.OriginalValues.GetValue<object>(property);
                    var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
                    if (original != null && !original.Equals(current))
                    {
                        dbEntityEntry.Property(property).IsModified = true;
                    }
                }
            }
            //return dbContext.SaveChanges();
        }
    }
    
}
