using SimpleOA.IBLL;
using SimpleOA.Model.OAModel;
using SimpleOA.DalFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.BLL
{
    public class ActionInfoBll: BaseBll<ActionInfo>, IActionInfoBll
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal = base.DbSession.ActionInfoDal;
        }
    }
}
