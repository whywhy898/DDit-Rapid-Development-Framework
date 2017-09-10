using DDit.Component.Tools;
using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.WorkFlowEntity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class InitiateFlowController : BaseController
    {
        //
        // GET: /SystemSchema/InitiateFlow/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SreachSelfFlowView() {

            return View();
        }

        public ActionResult HandleFlowView() {

            return View();
        }

        [HttpPost]
        public ActionResult GetSreachFlowInfo(FlowInfo model, string FlowName="")
        {

            model.UserId = this.UserInfo.UserID;
            if (FlowName != "") model.WorkFlowInfo = new WorkFlow() { FlowName = FlowName };

            var result = this.FlowTaskService.GetFlowInfoBySelf(model);

            return JsonResult(result,true);
        }

        [HttpPost]
        public ActionResult GetFlowApproveSelf(FlowApprove model)
        {
            var result = this.FlowTaskService.GetFlowApproveSelf(model);

            return JsonResult(result);
        }

        [HttpPost]
        public ActionResult GetFlowApproveInfo(FlowApprove model)
        {
            model.ApproveUser = this.UserInfo.UserID.ToString();
            var result = this.FlowTaskService.GetFlowApproveInfo(model);

            return JsonResult(result, true);
        }

        [HttpPost]
        public ActionResult ApproveFlow(FlowApprove model)
        {
            model.ReallyApproveUser = UserInfo.UserID;
            this.FlowTaskService.ApproveActive(model);

            return JsonResult(new ResultEntity() {result=true });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult StartFlow(FormCollection form)
        {
          //  var req = CommonHelper.GetStreamInfo(ControllerContext);
            var fileName = "";

            if (HttpContext.Request.Files.Count > 0)
            {
                HttpPostedFileBase Portrait = HttpContext.Request.Files[0];
                fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(Portrait.FileName).Replace("&", "");
                var sysPath = Server.MapPath("~/FlowFiles");
                if (!Directory.Exists(sysPath)) Directory.CreateDirectory(sysPath);
                var filePath = Path.Combine(sysPath, fileName);
                Portrait.SaveAs(filePath);

            }

            var req = new List<string>();

            var elementInfo = this.FormInfoService.GetElementInfo(int.Parse(form["formid"]));

            elementInfo.ForEach(a =>
            {
                if (a.element.ElementType != "upload")
                {
                    req.Add(a.FieldIden + "=" + form[a.FieldIden].ToString());
                }
                else {
                    req.Add(a.FieldIden + "=" + fileName);
                }
            });

            var flowingo = new FlowInfo();
            flowingo.FormId =int.Parse(form["formid"]);
            flowingo.FlowId =int.Parse(form["flowid"]);
            flowingo.CreateTime = DateTime.Now;
            flowingo.UserId = UserInfo.UserID;

            this.FormInfoService.ExecuteFlow(flowingo, req);

            return Json(new ResultEntity() { result = true });
        }

        public FilePathResult GetFlowFile(string filesName)
        {
            if (filesName == "" || filesName == "null")
                throw new ArgumentNullException("名字为空");

            string filePath = Path.Combine(Server.MapPath("/FlowFiles"), filesName);

            var name = Path.GetExtension(filesName);

            var contentype = "application/octet-stream";
            if (name == ".png" || name == ".jpg" || name == ".jpeg") {
                contentype = "image/jpeg";
            }
            if (name == ".pdf") {
                contentype = "application/pdf";
            }
            if (name == ".doc") {
                contentype = "application/msword";
            } if (name == ".xlsx")
            {
                contentype = "application/vnd.ms-excel";
            }

            return File(filePath, contentype);
        }
    }
}
