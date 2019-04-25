namespace SimpleOA.Model.ErpModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserManager")]
    public partial class UserManager
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        public string LoginName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int? DepartmentId { get; set; }

        public int? RoleId { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Photo { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? Sex { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? Sate { get; set; }
    }
}
