using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.BLL
{
    public class CommonWFTempBll : BaseBll<CommonWFTemp>, IBLL.ICommonWFTempBll
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal = base.DbSession.CommonWFTempDal;
        }
    }
}
