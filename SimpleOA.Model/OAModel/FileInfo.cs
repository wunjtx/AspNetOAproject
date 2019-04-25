namespace SimpleOA.Model.OAModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileInfo")]
    public partial class FileInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileInfo()
        {
            WF_Instance = new HashSet<WF_Instance>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(128)]
        public string FileName { get; set; }

        [StringLength(32)]
        public string FileType { get; set; }

        public string FilePath { get; set; }

        [StringLength(32)]
        public string FileSize { get; set; }

        [StringLength(64)]
        public string Remark { get; set; }

        public short DelFlag { get; set; }

        public DateTime SubTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<WF_Instance> WF_Instance { get; set; }
    }
}
