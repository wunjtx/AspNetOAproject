namespace SimpleOA.Model.OAModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WF_Instance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WF_Instance()
        {
            WF_Step = new HashSet<WF_Step>();
            FileInfo = new HashSet<FileInfo>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(128)]
        public string InstanceName { get; set; }

        public int StartBy { get; set; }

        public DateTime StartTime { get; set; }

        public short Level { get; set; }

        public string Content { get; set; }

        [StringLength(128)]
        public string Remark { get; set; }

        public short DelFlag { get; set; }

        [Required]
        public string FilePath { get; set; }

        public Guid WFInstanceId { get; set; }

        public int WF_TempID { get; set; }

        public short? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<WF_Step> WF_Step { get; set; }

        public virtual WF_Temp WF_Temp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<FileInfo> FileInfo { get; set; }
    }
}
