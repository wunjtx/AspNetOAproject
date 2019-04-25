namespace SimpleOA.Model.ErpModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbRoLe")]
    public partial class tbRoLe
    {
        [Key]
        public int RoleID { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
