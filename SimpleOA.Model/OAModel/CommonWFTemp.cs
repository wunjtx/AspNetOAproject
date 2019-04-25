namespace SimpleOA.Model.OAModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommonWFTemp")]
    public partial class CommonWFTemp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CommonWFTemp()
        {
            CommonWFInstance = new HashSet<CommonWFInstance>();
        }

        public int Id { get; set; }

        [StringLength(64)]
        public string FormUrl { get; set; }

        [StringLength(128)]
        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<CommonWFInstance> CommonWFInstance { get; set; }
    }
}
