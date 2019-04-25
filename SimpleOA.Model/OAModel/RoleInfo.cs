namespace SimpleOA.Model.OAModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoleInfo")]
    public partial class RoleInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleInfo()
        {
            TreeMenu = new HashSet<TreeMenu>();
            UserInfo = new HashSet<UserInfo>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(32)]
        public string RoleName { get; set; }

        public DateTime SubTime { get; set; }

        public DateTime ModfiedOn { get; set; }

        [StringLength(64)]
        public string Remark { get; set; }

        public short DelFlag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<TreeMenu> TreeMenu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
