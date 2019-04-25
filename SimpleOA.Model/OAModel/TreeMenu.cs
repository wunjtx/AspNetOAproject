namespace SimpleOA.Model.OAModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TreeMenu")]
    public partial class TreeMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TreeMenu()
        {
            R_UserInfo_TreeMenu = new HashSet<R_UserInfo_TreeMenu>();
            RoleInfo = new HashSet<RoleInfo>();
        }

        public int Id { get; set; }

        public int pId { get; set; }

        [Required]
        [StringLength(64)]
        public string Controller { get; set; }

        [StringLength(64)]
        public string Action { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public int? MenuId { get; set; }

        [StringLength(128)]
        public string MenuName { get; set; }

        [StringLength(128)]
        public string Url { get; set; }

        [StringLength(64)]
        public string HttpMethod { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public int? SortId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<R_UserInfo_TreeMenu> R_UserInfo_TreeMenu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<RoleInfo> RoleInfo { get; set; }
    }
}
