using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Model.OAModel
{
    public class UserQueryParam : BaseQueryParam 
    {
        //public UserQueryParam(Expression<Func<UserInfo, T>> orderBy)
        public UserQueryParam()
        {
            this.pageCount = 3;
            this.pageIndex = 0;
            //this.orderBy = orderBy;
        }
        public string UName { get; set; }
        public int ID { get; set; }
        public string ShowName { get; set; }
        public string Remark { get; set; }
        public DateTime SubTime { get; set; }
        public DateTime TimeAfter { get; set; }
        public DateTime TimeBefore { get; set; }
        public bool isAsc { get; set; }
        //public Expression<Func<UserInfo,T>> orderBy { get; set; }
    }

    public class MenuQueryParam : BaseQueryParam
    {
        //public UserQueryParam(Expression<Func<UserInfo, T>> orderBy)
        public MenuQueryParam()
        {
            this.pageCount = 5;
            this.pageIndex = 0;
            //this.orderBy = orderBy;
        }
        public int ID { get; set; }
        public int pId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public string MenuName { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string Remark { get; set; }
        //public Expression<Func<UserInfo,T>> orderBy { get; set; }
    }
}
