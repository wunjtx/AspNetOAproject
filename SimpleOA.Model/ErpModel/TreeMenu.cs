namespace SimpleOA.Model.ErpModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TreeMenu")]
    public partial class TreeMenu
    {
        [Key]
        public int NodeId { get; set; }

        public int? ParentId { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        [StringLength(50)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Duty { get; set; }
    }
}
