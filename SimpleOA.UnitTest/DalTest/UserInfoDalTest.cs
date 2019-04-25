using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleOA.OADAL;
using SimpleOA.Model.OAModel;

namespace SimpleOA.UnitTest
{
    [TestClass]
    public class UserInfoDalTest
    {
        [TestMethod]
        public void GetUserTest()
        {
            UserInfoDal dal = new UserInfoDal();
            IQueryable<UserInfo> temp = dal.GetEntities(u => u.ID > 1);
            Assert.AreEqual(true, temp.Count() > 5);
        }
    }
}
