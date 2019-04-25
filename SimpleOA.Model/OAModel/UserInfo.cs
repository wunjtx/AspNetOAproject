namespace SimpleOA.Model.OAModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInfo")]
    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            CommonWFInstance = new HashSet<CommonWFInstance>();
            CommonWFStep = new HashSet<CommonWFStep>();
            OrderInfo = new HashSet<OrderInfo>();
            R_UserInfo_TreeMenu = new HashSet<R_UserInfo_TreeMenu>();
            RoleInfo = new HashSet<RoleInfo>();
        }

        public int ID { get; set; }

        [StringLength(32)]
        public string UName { get; set; }

        [Required]
        [StringLength(32)]
        public string Pwd { get; set; }

        [Required]
        [StringLength(64)]
        public string ShowName { get; set; }

        public short DelFlag { get; set; }

        [StringLength(64)]
        public string Remark { get; set; }

        public DateTime ModfiedOn { get; set; }

        public DateTime SubTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<CommonWFInstance> CommonWFInstance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<CommonWFStep> CommonWFStep { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<R_UserInfo_TreeMenu> R_UserInfo_TreeMenu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<RoleInfo> RoleInfo { get; set; }
    }
}
