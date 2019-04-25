namespace SimpleOA.Model.ErpModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public int DepartmentId { get; set; }

        [StringLength(50)]
        public string DepartmentName { get; set; }

        [StringLength(20)]
        public string Tel1 { get; set; }

        [StringLength(20)]
        public string Tel2 { get; set; }

        public int? Displayorder { get; set; }
    }
}
