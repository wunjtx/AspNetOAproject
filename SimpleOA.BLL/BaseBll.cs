using SimpleOA.DalFactory;
using SimpleOA.IBaseDAL;
using SimpleOA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.BLL
{
    public abstract class BaseBll<T> where T:class, new()
    { 
        private IDbSession _dbSession;
        public IDbSession DbSession
        {
            get
            {
                if (_dbSession==null)
                {
                    _dbSession = DbSessionFactory.GetDbSession();
                }
                return _dbSession;
            }
        }
        public IBaseDal<T> CurrentDal
        {
            get;
            set;
        }
        public BaseBll()
        {
            SetCurrentDal();
        }
        public abstract void SetCurrentDal();
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);
        }
        public IQueryable<T> GetEntitiesIndex<S>(int pageIndex, int pageCount, out int Total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc)
        {
            return CurrentDal.GetEntitiesIndex(pageIndex, pageCount, out Total, whereLambda, orderbyLambda, isAsc);
        }
        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            IBaseDal currentDal = CurrentDal as IBaseDal;
            this.DbSession.SaveChanges(currentDal);
            return entity;
        }
        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            IBaseDal currentDal = CurrentDal as IBaseDal;
            return this.DbSession.SaveChanges(currentDal) >0;
        }
        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            IBaseDal currentDal = CurrentDal as IBaseDal;
            return this.DbSession.SaveChanges(currentDal) >0;
        }
        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            IBaseDal currentDal = CurrentDal as IBaseDal;
            return this.DbSession.SaveChanges(currentDal) > 0;
        }
        public bool DeleteByLogical(int id)
        {
            CurrentDal.DeleteByLogical(id);
            return true;
        }
        public void Update(T entity, Expression<Func<T, object>>[] entityPropertities)
        {
            CurrentDal.Update(entity, entityPropertities);
        }
    }
}
