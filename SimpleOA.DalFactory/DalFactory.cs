using SimpleOA.IErpDAL;
using SimpleOA.IOADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.DalFactory
{
    public class DalFactory
    {
        private static string  OAAssemblyName = System.Configuration.ConfigurationManager.AppSettings["OADalAssemblyName"];
        private static string ErpAssemblyName = System.Configuration.ConfigurationManager.AppSettings["ErpDalAssemblyName"];
        public static IOADAL.IUserInfoDal GetOAUserInfoDal()
        {
            return  Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".UserInfoDal") as IOADAL.IUserInfoDal;
        }
        public static IErpDAL.IUserManagerDal GetErpUserManagerDal()
        {
            return Assembly.Load(ErpAssemblyName).CreateInstance(ErpAssemblyName + ".UserManagerDal") as IErpDAL.IUserManagerDal;
        }
        public static IOADAL.IActionInfoDal GetOAActionInfoDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".ActionInfoDal") as IOADAL.IActionInfoDal;
        }
        public static IOADAL.ICommonWFInstanceDal GetOACommonWFInstanceDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".CommonWFInstanceDal") as IOADAL.ICommonWFInstanceDal;
        }
        public static IOADAL.ICommonWFStepDal GetOACommonWFStepDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".CommonWFStepDal") as IOADAL.ICommonWFStepDal;
        }
        public static IOADAL.ICommonWFTempDal GetOACommonWFTempDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".CommonWFTempDal") as IOADAL.ICommonWFTempDal;
        }
        public static IOADAL.IRoleInfoDal GetOARoleInfoDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".RoleInfoDal") as IOADAL.IRoleInfoDal;
        }
        public static IOADAL.IR_UserInfo_TreeMenuDal GetOAR_UserInfo_TreeMenuDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".R_UserInfo_TreeMenuDal") as IOADAL.IR_UserInfo_TreeMenuDal;
        }
        public static IOADAL.ITreeMenuDal GetOATreeMenuDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".TreeMenuDal") as IOADAL.ITreeMenuDal;
        }

        public static IOADAL.IWFInstanceDal GetOAWFInstanceDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".WFInstanceDal") as IOADAL.IWFInstanceDal;
        }
        public static IOADAL.IWFStepDal GetOAWFStepDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".WFStepDal") as IOADAL.IWFStepDal;
        }
        public static IOADAL.IWFTempDal GetOAWFTempDal()
        {
            return Assembly.Load(OAAssemblyName).CreateInstance(OAAssemblyName + ".WFTempDal") as IOADAL.IWFTempDal;
        }
    }
}
