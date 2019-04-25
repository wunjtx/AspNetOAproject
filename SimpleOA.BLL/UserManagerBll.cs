using SimpleOA.Model.ErpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleOA.IBLL;

namespace SimpleOA.BLL
{
    public class UserManagerBll : BaseBll<UserManager>, IUserManagerBll
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal = base.DbSession.UserManagerDal;
        }
    }
}
