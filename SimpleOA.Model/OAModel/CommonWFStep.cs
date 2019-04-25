namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommonWFStep")]
    public partial class CommonWFStep
    {
        public int Id { get; set; }

        [StringLength(64)]
        public string StepName { get; set; }

        public int? ProcessBy { get; set; }

        public DateTime? SubTime { get; set; }

        [StringLength(128)]
        public string ProcessResult { get; set; }

        public short? StepStatus { get; set; }

        public bool? IsStartStep { get; set; }

        public bool? IsEndStep { get; set; }

        public int? ParentStepId { get; set; }

        public int? CommonWFInstanceId { get; set; }

        public virtual CommonWFInstance CommonWFInstance { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
