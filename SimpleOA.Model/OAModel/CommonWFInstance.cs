namespace SimpleOA.Model.OAModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommonWFInstance")]
    public partial class CommonWFInstance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CommonWFInstance()
        {
            CommonWFStep = new HashSet<CommonWFStep>();
        }

        public int Id { get; set; }

        public int TempId { get; set; }

        public int UserId { get; set; }

        public DateTime? SubTime { get; set; }

        public int? MaxValue { get; set; }

        public Guid WFInstanceId { get; set; }

        public short? status { get; set; }

        public virtual CommonWFTemp CommonWFTemp { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<CommonWFStep> CommonWFStep { get; set; }
    }
}
