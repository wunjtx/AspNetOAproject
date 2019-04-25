using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.BLL
{
    public class WFTempBll : WFBaseBll<CommonWFTemp>, IBLL.IWFTempBll
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal = base.DbSession.WFTempDal;
        }
    }
}
