namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderInfo")]
    public partial class OrderInfo
    {
        public int ID { get; set; }

        [StringLength(64)]
        public string Content { get; set; }

        public int UserInfoID { get; set; }

        public short DelFlag { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
