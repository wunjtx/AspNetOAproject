namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInfoExt")]
    public partial class UserInfoExt
    {
        public int ID { get; set; }

        public int UserInfoId { get; set; }

        public int Age { get; set; }

        [StringLength(16)]
        public string Phone { get; set; }

        public string Email { get; set; }

        public short DelFlag { get; set; }
    }
}
