using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.IBLL
{
    public interface IBaseBll<T> where T:class ,new ()
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetEntitiesIndex<S>(int pageIndex, int pageCount, out int Total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Delete(int id);
        bool DeleteByLogical(int id);
        void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
    }
    public interface IBaseBll
    {

    }
}
