using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleOA.IBLL;

namespace SimpleOA.BLL
{
    public class TreeMenuBll : BaseBll<TreeMenu>, IBLL.ITreeMenuBll
    {
        public override void SetCurrentDal()
        {
            base.CurrentDal = base.DbSession.TreeMenuDal;
        }
    }
}
