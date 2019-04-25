using ActivityLibraryFinancial;
using FineUIMvc;
using SimpleOA.Model.OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleOA.UI.Portal.Controllers
{
    public class WorkFlowController : BaseController
    {
        IBLL.ICommonWFInstanceBll commonWFInstanceBll = new BLL.CommonWFInstanceBll();
        IBLL.ICommonWFTempBll commonWFTempBll = new BLL.CommonWFTempBll();
        IBLL.ICommonWFStepBll commonWFStepBll = new BLL.CommonWFStepBll();
        IBLL.IUserInfoBll userInfoBll = new BLL.UserInfoBll();
        // GET: WorkFlow
        public ActionResult Index()
        {
            ViewBag.TempList = commonWFTempBll.GetEntities(t => true).ToList();
            return View();
        }
        public ActionResult Detail(int id)
        {
            ViewBag.User = LoginUser;
            ViewBag.SetUpValue = 100;
            //ViewBag.Process = userInfoBll.GetEntities(u => u.ID != LoginUser.ID).ToList().Select(p=> new SelectListItem (){ Selected=false, Text = p.UName, Value =p.ID.ToString()});
            var temp = commonWFTempBll.GetEntities(t => t.Id == id).FirstOrDefault();
            ViewBag.FormTemp = id;
            return View(temp.FormUrl.Trim(' '),temp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection fm)
        {
            //int setupValue = Convert.ToInt32(fm["SetUpValue"]);
            //CommonWFInstance wfInstance = new CommonWFInstance();
            //wfInstance.WFInstanceId = Guid.Empty;
            //wfInstance.MaxValue = Convert.ToInt32(fm["MaxValue"]);
            //wfInstance.SubTime = DateTime.Now;
            //wfInstance.status = (short)Enum.WF_InstanceEnum.Processing;
            //wfInstance.TempId = Convert.ToInt32(fm["FID"]);
            //wfInstance.UserId = Convert.ToInt32(fm["ID"]);
            //commonWFInstanceBll.Add(wfInstance);

            var wfApp = WorkflowApplicationHelper.CreateWorkflowApp(new ActivityLibraryFinancial.Activity1 (), new Dictionary<string, object> {
                { "SetUpValue",Convert.ToInt32(fm["SetUpValue"]) },
                {"InputValue",Convert.ToInt32(fm["MaxValue"]) },
                {"StartBy",Convert.ToInt32(fm["ID"]) },
                {"ManagerId",8 },//暂时写成固定的 userId=8 test2
                {"SupervisorId",1 },//暂时写成固定的 userId=1 admin
                {"TempId",Convert.ToInt32(fm["FID"]) },
                {"FinacialId", 9},//暂时写成固定的 userId=9 test3
            });
            //创建wf实例
           
            //wfInstance.WFInstanceId = wfApp.Id;

            //commonWFInstanceBll.Update(wfInstance);
            ////申请人的步骤
            //CommonWFStep wfStep = new CommonWFStep();
            //wfStep.IsStartStep = true;
            //wfStep.IsEndStep = false;
            //wfStep.StepName = "提交审批";
            //wfStep.CommonWFInstanceId = wfInstance.Id;
            //wfStep.ProcessBy = Convert.ToInt32(fm["Process"]);
            //wfStep.SubTime = DateTime.Now;
            //wfStep.ProcessBy = Convert.ToInt32(fm["ID"]);
            //wfStep.StepStatus = (short)Enum.WFStepEnum.Processed;
            //wfStep.ProcessResult = "通过";
            //commonWFStepBll.Add(wfStep);
            
            return RedirectToAction("/index");
        }

        public ActionResult MyWorkFlow()
        {
            var commonWfStep = commonWFStepBll.GetEntities(c=>c.ProcessBy==LoginUser.ID && c.CommonWFInstance.status==(short)Enum.WF_InstanceEnum.Processing&&c.StepStatus==(short)SimpleOA.Enum.WFStepEnum.UnProecess).ToList();

            ViewBag.WFSteps = commonWfStep;
            return View();
        }

        public ActionResult ProcessDetail(int id, string insId)
        {
            //拿到wf实例
            int instanceId = Convert.ToInt32(insId);
            var commonWfInstance = commonWFInstanceBll.GetEntities(c => c.Id == instanceId).FirstOrDefault();
            //拿到步骤实例
            var commonWfStep = commonWFStepBll.GetEntities(s => s.Id == id).FirstOrDefault();
            ViewBag.WfStep = commonWfStep;
            ViewBag.WfInstance = commonWfInstance;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PassCheck(FormCollection fm)
        {
            //stepId:id, isPass: pass, Comment: $("#Comment").val() }

            //1、更新当前步骤
            int id = Convert.ToInt32(fm["stepId"]);
            CommonWFStep step = commonWFStepBll.GetEntities(s => s.Id == id).FirstOrDefault();

            //step.StepStatus = fm["Result"].ToLower() == "true" ? (short)SimpleOA.Enum.WFStepEnum.Processed: (short)SimpleOA.Enum.WFStepEnum.UnProecess;
            //step.SubTime = DateTime.Now;
            //commonWFStepBll.Update(step);
            ////让书签继续往下执行。
            var Value = fm["Result"].ToLower() == "true" ? 1 : 0;

            WorkflowApplicationHelper.ResumeBookMark(
                new ActivityLibraryFinancial.Activity1(),
                step.CommonWFInstance.WFInstanceId,
                step.StepName,
                Value);
            return RedirectToAction("/index");
        }
    }
}