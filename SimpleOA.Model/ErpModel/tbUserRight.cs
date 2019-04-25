namespace SimpleOA.Model.ErpModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbUserRight")]
    public partial class tbUserRight
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? RightID { get; set; }
    }
}
