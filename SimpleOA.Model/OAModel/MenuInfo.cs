namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuInfo")]
    public partial class MenuInfo
    {
        public int ID { get; set; }
    }
}
