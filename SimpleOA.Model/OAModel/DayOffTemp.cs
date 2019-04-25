namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DayOffTemp")]
    public partial class DayOffTemp
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Remark { get; set; }

        [StringLength(128)]
        public string FormUrl { get; set; }
    }
}
