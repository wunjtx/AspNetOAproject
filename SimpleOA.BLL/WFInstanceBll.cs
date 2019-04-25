using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.BLL
{
    public class WFInstanceBll:WFBaseBll<CommonWFInstance>, IBLL.IWFInstanceBll
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal = base.DbSession.WFInstanceDal;
        }
    }
}
