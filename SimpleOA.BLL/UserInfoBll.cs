using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleOA.IOADAL;
using SimpleOA.Model.ErpModel;
using SimpleOA.Model.OAModel;
using SimpleOA.DalFactory;
using SimpleOA.IBLL;
using System.Linq.Expressions;

namespace SimpleOA.BLL
{
    public class UserInfoBll : BaseBll<UserInfo>,IUserInfoBll 
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal = base.DbSession.UserInfoDal;
        }
        //another way to set field in base class , no test----------
            //public UserInfoBll()
            //{
            //    base.CurrentDal = DbSessionFactory.GetDbSession().UserInfoDal;
            //}
        //----------------------------------------------------------
        public IQueryable<UserInfo> GetSearchIndex<S>(UserQueryParam qm, Expression<Func<UserInfo, S>> orderByLambada,out int totalCount)
        {
            IQueryable<UserInfo> users = CurrentDal.GetEntities(u => true);
            if (qm.ID!=0)
            {
                users = users.Where(u => u.ID == qm.ID);
            }
            if (!string.IsNullOrEmpty(qm.UName))
            {
                users = users.Where(u => u.UName.Contains(qm.UName));
            }
            if (!string.IsNullOrEmpty(qm.ShowName))
            {
                users = users.Where(u => u.ShowName.Contains(qm.ShowName));
            }
            if (!string.IsNullOrEmpty(qm.Remark)) 
            {
                users = users.Where(u => u.Remark.Contains(qm.Remark));
            }
            if (qm.TimeAfter!=DateTime.MinValue )
            {
                users = users.Where(u => DateTime.Compare(u.SubTime, qm.TimeAfter) > 0);
            }
            if (qm.TimeBefore != DateTime.MinValue)
            {
                users = users.Where(u => DateTime.Compare(qm.TimeBefore, u.SubTime) > 0);
            }

            totalCount = users.Count();

            users = users.OrderBy(orderByLambada).Skip(qm.pageIndex * qm.pageCount).Take(qm.pageCount);
            return users;
        }

    }
}
