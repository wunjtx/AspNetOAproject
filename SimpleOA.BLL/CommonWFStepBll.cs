using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.BLL
{
    public class CommonWFStepBll : BaseBll<CommonWFStep>, IBLL.ICommonWFStepBll
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal = base.DbSession.CommonWFStepDal;
        }
    }
}
