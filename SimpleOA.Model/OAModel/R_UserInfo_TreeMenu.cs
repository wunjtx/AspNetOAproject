namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class R_UserInfo_TreeMenu
    {
        public int ID { get; set; }

        public bool HasPermission { get; set; }

        public int UserInfoID { get; set; }

        public int TreeMenuID { get; set; }

        public short DelFlag { get; set; }

        public virtual TreeMenu TreeMenu { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
