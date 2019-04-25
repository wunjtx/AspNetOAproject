namespace SimpleOA.Model.OAModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WF_Temp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WF_Temp()
        {
            WF_Instance = new HashSet<WF_Instance>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(32)]
        public string TepName { get; set; }

        public string Description { get; set; }

        public string TempForm { get; set; }

        [StringLength(64)]
        public string Remark { get; set; }

        public short DelFlag { get; set; }

        public DateTime SubTime { get; set; }

        [Required]
        public string ActityType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<WF_Instance> WF_Instance { get; set; }
    }
}
