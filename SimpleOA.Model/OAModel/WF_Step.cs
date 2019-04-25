namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WF_Step
    {
        public int ID { get; set; }

        [StringLength(32)]
        public string StepName { get; set; }

        public int ProcessBy { get; set; }

        public DateTime SubTime { get; set; }

        public DateTime ProcessTime { get; set; }

        [Required]
        [StringLength(256)]
        public string ProcessResult { get; set; }

        [StringLength(256)]
        public string PorcessComment { get; set; }

        public short StepStatus { get; set; }

        public bool IsStartStep { get; set; }

        public bool IsEndStep { get; set; }

        public int ParentStepId { get; set; }

        public int WF_InstanceID { get; set; }

        public virtual WF_Instance WF_Instance { get; set; }
    }
}
