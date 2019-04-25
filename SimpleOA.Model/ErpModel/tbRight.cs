namespace SimpleOA.Model.ErpModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbRight")]
    public partial class tbRight
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string RightName { get; set; }

        [StringLength(50)]
        public string ReMark { get; set; }
    }
}
