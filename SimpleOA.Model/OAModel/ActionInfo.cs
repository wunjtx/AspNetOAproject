namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActionInfo")]
    public partial class ActionInfo
    {
        public int ID { get; set; }

        public DateTime SubTime { get; set; }

        public DateTime ModfiedOn { get; set; }

        [StringLength(64)]
        public string Remark { get; set; }

        public short DelFlag { get; set; }

        [Required]
        [StringLength(512)]
        public string Url { get; set; }

        [StringLength(32)]
        public string HttpMethd { get; set; }

        [Required]
        [StringLength(32)]
        public string ActionName { get; set; }

        public bool IsMenu { get; set; }

        [StringLength(512)]
        public string MenuIcon { get; set; }

        public int Sort { get; set; }
    }
}
