using SimpleOA.IOADAL;
using SimpleOA.IErpDAL;
using SimpleOA.IBaseDAL;

namespace SimpleOA.DalFactory
{
    public interface IDbSession
    {
        IOADAL.IUserInfoDal UserInfoDal { get; }
        IErpDAL.IUserManagerDal UserManagerDal { get; }
        int SaveChanges(IBaseDal currentDal);
        IOADAL.IActionInfoDal ActionInfoDal { get; }
        IOADAL.IRoleInfoDal RoleInfoDal { get; }
        IOADAL.IR_UserInfo_TreeMenuDal R_UserInfo_TreeMenuDal { get; }
        IOADAL.ITreeMenuDal TreeMenuDal { get; }
        IOADAL.ICommonWFInstanceDal CommonWFInstanceDal { get; }
        IOADAL.ICommonWFStepDal CommonWFStepDal { get; }
        IOADAL.ICommonWFTempDal CommonWFTempDal { get; }
        IOADAL.IWFInstanceDal WFInstanceDal { get; }
        IOADAL.IWFStepDal WFStepDal { get; }
        IOADAL.IWFTempDal WFTempDal { get; }
        int SaveChangesByCallContext();
    }
}