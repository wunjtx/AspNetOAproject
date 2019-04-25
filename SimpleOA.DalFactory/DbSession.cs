using SimpleOA.IErpDAL;
using SimpleOA.IOADAL;
using SimpleOA.ErpDAL;
using SimpleOA.OADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SimpleOA.IBaseDAL;
using System.Reflection;

namespace SimpleOA.DalFactory
{
    public class DbSession:IDbSession  
    {
        private IOADAL.IUserInfoDal _UserInfoDal;
        public IOADAL.IUserInfoDal UserInfoDal
        {
            get
            {
                if (_UserInfoDal == null)
                {
                    _UserInfoDal = DalFactory.GetOAUserInfoDal();
                }
                return _UserInfoDal;
            }
            //set { _UserInfoDal = value; }
        }
        private IErpDAL.IUserManagerDal _UserManagerDal;
        public IUserManagerDal UserManagerDal
        {
            get
            {
                if (_UserManagerDal == null)
                {
                    _UserManagerDal = DalFactory.GetErpUserManagerDal();
                }
                return _UserManagerDal;
            }
        }
        private IOADAL.IActionInfoDal _ActionInfoDal;
        public IOADAL.IActionInfoDal ActionInfoDal
        {
            get
            {
                if (_ActionInfoDal == null)
                {
                    _ActionInfoDal = DalFactory.GetOAActionInfoDal();
                }
                return _ActionInfoDal;
            }
        }
        private IOADAL.ICommonWFInstanceDal _CommonWFInstanceDal;
        public IOADAL.ICommonWFInstanceDal CommonWFInstanceDal
        {
            get
            {
                if (_CommonWFInstanceDal == null)
                {
                    _CommonWFInstanceDal = DalFactory.GetOACommonWFInstanceDal();
                }
                return _CommonWFInstanceDal;
            }
        }
        private IOADAL.ICommonWFStepDal _CommonWFStepDal;
        public IOADAL.ICommonWFStepDal CommonWFStepDal
        {
            get
            {
                if (_CommonWFStepDal == null)
                {
                    _CommonWFStepDal = DalFactory.GetOACommonWFStepDal();
                }
                return _CommonWFStepDal;
            }
        }
        private IOADAL.ICommonWFTempDal _CommonWFTempDal;
        public IOADAL.ICommonWFTempDal CommonWFTempDal
        {
            get
            {
                if (_CommonWFTempDal == null)
                {
                    _CommonWFTempDal = DalFactory.GetOACommonWFTempDal();
                }
                return _CommonWFTempDal;
            }
        }
        private IOADAL.IRoleInfoDal _RoleInfoDal;
        public IOADAL.IRoleInfoDal RoleInfoDal
        {
            get
            {
                if (_RoleInfoDal == null)
                {
                    _RoleInfoDal = DalFactory.GetOARoleInfoDal();
                }
                return _RoleInfoDal;
            }
        }
        private IOADAL.IR_UserInfo_TreeMenuDal _R_UserInfo_TreeMenuDal;
        public IOADAL.IR_UserInfo_TreeMenuDal R_UserInfo_TreeMenuDal
        {
            get
            {
                if (_R_UserInfo_TreeMenuDal == null)
                {
                    _R_UserInfo_TreeMenuDal = DalFactory.GetOAR_UserInfo_TreeMenuDal();
                }
                return _R_UserInfo_TreeMenuDal;
            }
        }
        private IOADAL.ITreeMenuDal _TreeMenuDal;
        public IOADAL.ITreeMenuDal TreeMenuDal
        {
            get
            {
                if (_TreeMenuDal == null)
                {
                    _TreeMenuDal = DalFactory.GetOATreeMenuDal();
                }
                return _TreeMenuDal;
            }
        }

        private IOADAL.IWFInstanceDal _WFInstanceDal;
        public IOADAL.IWFInstanceDal WFInstanceDal
        {
            get
            {
                if (_WFInstanceDal == null)
                {
                    _WFInstanceDal = DalFactory.GetOAWFInstanceDal();
                }
                return _WFInstanceDal;
            }
        }
        private IOADAL.IWFStepDal _WFStepDal;
        public IOADAL.IWFStepDal WFStepDal
        {
            get
            {
                if (_WFStepDal == null)
                {
                    _WFStepDal = DalFactory.GetOAWFStepDal();
                }
                return _WFStepDal;
            }
        }
        private IOADAL.IWFTempDal _WFTempDal;
        public IOADAL.IWFTempDal WFTempDal
        {
            get
            {
                if (_WFTempDal == null)
                {
                    _WFTempDal = DalFactory.GetOAWFTempDal();
                }
                return _WFTempDal;
            }
        }
        public  int SaveChangesByCallContext()
        {
            return SimpleOA.OADAL.DbFactory.GetDbContextByCallContext().SaveChanges();
        }
        //get the specified DbContext by reflection, before switch different need invoke this method when CUD.
        public int SaveChanges(IBaseDal currentDal)
        {
            try {
                string baseTypeNameSpace = currentDal.GetType().BaseType.Namespace;
                string baseTypeAssemblyName = currentDal.GetType().BaseType.Assembly.GetName().Name;
                Type type =Type.GetType(baseTypeNameSpace + ".DbFactory"+","+ baseTypeAssemblyName);
                dynamic obj = type.Assembly.CreateInstance(baseTypeNameSpace + ".DbFactory");
                MethodInfo method = type.GetMethod("GetDbContext");
                return ((DbContext) method.Invoke(obj, null)).SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
