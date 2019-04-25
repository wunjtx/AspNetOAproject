using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.BLL
{
    public class R_UserInfo_TreeMenuBll : BaseBll<R_UserInfo_TreeMenu>, IBLL.IR_UserInfo_TreeMenuBll
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal=base.DbSession.R_UserInfo_TreeMenuDal;
        }
    }
}
