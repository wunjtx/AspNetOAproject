namespace SimpleOA.Model.ErpModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileList")]
    public partial class FileList
    {
        [Key]
        public int FileId { get; set; }

        [StringLength(50)]
        public string FileTitle { get; set; }

        public int? TableId { get; set; }

        public int? TableListId { get; set; }

        [Column(TypeName = "text")]
        public string FileContent { get; set; }

        [StringLength(20)]
        public string FromUserName { get; set; }

        [StringLength(20)]
        public string ToUserName { get; set; }

        [StringLength(50)]
        public string AffixFile { get; set; }

        public int? FileStatus { get; set; }

        public int? FilePass { get; set; }

        public DateTime? FirstDate { get; set; }

        public DateTime? OverDate { get; set; }
    }
}
