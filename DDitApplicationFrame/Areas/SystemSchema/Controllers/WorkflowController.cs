using DDit.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Dynamic;
using DDitApplicationFrame.Common;
using DDit.Core.Data.Entity.WorkFlowEntity;
using DDit.Core.Data.Entity.WorkFlowEntity.DoEntity;
using System.Web.Script.Serialization;
using DDit.Component.Tools;


namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class WorkflowController : BaseController
    {
        //
        // GET: /SystemSchema/Workflow/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CFlowView() {

            ViewBag.DepartmentUser = this.MessageServer.GetDepartmentInfo();
            return View();
        }

        [HttpPost]
        public ActionResult GetWorkFinfos(WorkFlow model) {
           
            var result=this.WorkFlowService.GetWorkFlowItem(model);

            return JsonResult<WorkFlowDo>(result,true);
        }

        [HttpPost]
        public ActionResult WorkFlowOperation(WorkFlow model)
        {
            if (model.FlowID == 0)
            {
                model.CreateTime = DateTime.Now;
                model.CreateUser = this.UserInfo.UserID;
                this.WorkFlowService.AddWorkFlow(model);
            }
            else {
                this.WorkFlowService.MoidfyWorkFlow(model);
            }

            return JsonResult(new ResultEntity() { result=true});
        }

        [HttpPost]
        public ActionResult DeleteWorkFlow(WorkFlow model) {

            var bol = this.WorkFlowService.RemoveWorkFlow(model);

            return JsonResult(new ResultEntity() { result = bol });
        }

        [HttpPost]
        public ActionResult SaveFlowView(int flowId,string jsonstr)
        { 
            
            this.WorkFlowService.SaveFlowViewInfo(flowId, jsonstr);

            return Json(new ResultEntity() { result=true});
        }

        [HttpPost]
        public ActionResult GetTabFieldsAndHasfv(int formid,int flowid)
        {
            var fields = this.FormInfoService.GetTabFields(formid);

            var has = this.WorkFlowService.HasFlowView(flowid);

            var result = new ResultEntity();

            if (has)
            {
                result.result = false;
                result.message = "该流程已经存在流程设计！";
            }
            else {
                result.result = true;
                result.dataList = fields;
            }

            return JsonResult(result);
        }

        [HttpPost]
        public ActionResult GetFlowView(int formid, int flowid)
        {
            var has = this.WorkFlowService.HasFlowView(flowid);
            var results = new ResultEntity();
            if (!has) {
                results.result = false;
                results.message = "该流程未建立流程设计！";
                return JsonResult(results);
            }

            var flowInfo = this.WorkFlowService.GetFlowViewInfo(flowid);

            var fieldInfo = this.FormInfoService.GetTabFields(formid);

            results.result = true;
            results.Data = fieldInfo;
            results.dataList = flowInfo;
     
            return JsonResult(results);
        }

  
    }
}
