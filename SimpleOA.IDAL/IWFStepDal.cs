using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.IOADAL
{
    public interface IWFStepDal:IBaseDAL.IBaseDal,IBaseDAL.IWFBaseDal<CommonWFStep>
    {
    }
}
