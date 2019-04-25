using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using SimpleOA.BLL;
using SimpleOA.IBLL;
using SimpleOA.Model.OAModel;
using SimpleOA.IOADAL;
using SimpleOA.DalFactory;

namespace ActivityLibraryFinancial
{

    public sealed class SetStepActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> StepName { get; set; }
        public InOutArgument<int> NextProcessBy { get; set; }
        public InArgument<short> IsEnd { get; set; }
        public InArgument<short> StepStatus { get; set; }
        public InArgument<int> InPutValue { get; set; }
        public InArgument<int> TempId { get; set; }
        public InArgument<int> StartBy { get; set; }
        public InOutArgument<int> StepId { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            short stepStatus = context.GetValue(this.StepStatus);
            string stepName = context.GetValue(this.StepName);
            short isEnd = context.GetValue(this.IsEnd);
            int nextProcessBy = context.GetValue(this.NextProcessBy);
            int inputValue = context.GetValue(this.InPutValue);
            int tempId = context.GetValue(this.TempId);
            int startBy = context.GetValue(this.StartBy);
            int stepId = context.GetValue(this.StepId);

            Guid insId = context.WorkflowInstanceId;

            //string insIdStr = insId.ToString();
            IWFInstanceBll commonWFInstanceBll = new WFInstanceBll();
            IWFStepBll commonWFStepBll = new WFStepBll();

            CommonWFInstance inst = commonWFInstanceBll.GetEntities(F=>F.WFInstanceId== insId).FirstOrDefault();

            if (inst == null)
            {

                inst = new CommonWFInstance();
                inst.WFInstanceId = insId;
                inst.MaxValue = inputValue;
                inst.SubTime = DateTime.Now;
                inst.status = isEnd;
                inst.TempId = tempId;
                inst.UserId = startBy;
                commonWFInstanceBll.Add(inst);
            }

            //CommonWFStep step = commonWFStepBll.GetEntities(s => s.CommonWFInstanceId == inst.Id && s.ProcessBy == nextProcessBy && s.StepName == stepName).FirstOrDefault();
            //if (step==null)
            //{
            //    step = new CommonWFStep();
            //    step.StepName=stepName;
            //    step.ProcessBy=nextProcessBy;
            //    step.SubTime = DateTime.Now;
            //    step.StepStatus=stepStatus;
            //    step.CommonWFInstanceId = inst.Id;
            //    commonWFStepBll.Add(step);
            //}
            //else
            //{
            //    step.SubTime = DateTime.Now;
            //    step.StepStatus = stepStatus;
            //    commonWFStepBll.Update(step);
            //}

            if (stepId==0)
            {
                CommonWFStep step = new CommonWFStep();
                step.StepName = stepName;
                step.ProcessBy = nextProcessBy;
                step.SubTime = DateTime.Now;
                step.StepStatus = stepStatus;
                step.CommonWFInstanceId = inst.Id;
                commonWFStepBll.Add(step);
                context.SetValue(StepId, step.Id);
            }
            else
            {
                CommonWFStep step = commonWFStepBll.GetEntities(s=>s.Id==stepId).FirstOrDefault();
                step.SubTime = DateTime.Now;
                step.StepStatus = stepStatus;
                commonWFStepBll.Update(step);
            }
            if (isEnd==(short)SimpleOA.Enum.WF_InstanceEnum.Processed)
            {
                inst.status = isEnd;
                commonWFInstanceBll.Update(inst);
            }
            //var step = commonWFStepBll.GetEntities(s => s.CommonWFInstance.Id == inst.Id).FirstOrDefault();
            ////Common.LogHelper.WriteLog("SetSetpActivity:" + text);
            //step.StepName = stepName;
            //step.StepStatus = stepStatus;
            //step.ProcessBy = nextProcessBy;
            //step.IsEndStep = end;
            //if (end)
            //{
            //    step.ProcessResult = "审批结束";
            //    step.StepStatus = (short)SimpleOA.Enum.WFStepEnum.Processed;
            //    inst.status = (short)SimpleOA.Enum.WF_InstanceEnum.Processed;
            //    commonWFInstanceBll.Update(inst);
            //}
            //commonWFStepBll.Update(step);
        }
    }
}
