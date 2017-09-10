using DDit.Component.Tools;
using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.FormEntity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class FormController : BaseController
    {
        //
        // GET: /SystemSchema/Form/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetFormInfos(FormInfo model)
        {
            var result = this.FormInfoService.GetFormInfoItems(model);

            return JsonResult<FormInfo>(result);
        }
       
        [HttpPost]
        public ActionResult AddORUpdateFormInfo(FormInfo model) {
            if (model.FormId == 0)
            {
                this.FormInfoService.AddFormInfo(model);
            }
            else {
                this.FormInfoService.ModifyFormInfo(model);
            }
            return Json(new ResultEntity() { result=true});
        }

        [HttpPost]
        public ActionResult DeleteFormInfo(FormInfo model) {
            var result = this.FormInfoService.RemoveFormInfo(model);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetElementInfoList() {

            var result = this.ElementInfoService.GetElementInfoList();

            return Json(result);
        }

        [HttpPost]
        public ActionResult FormOperate(FormInfo model,bool isTask=false)
        {
            var OperateResult=new ResultEntity();

            OperateResult.result = this.FormInfoService.IsCompleteConfig(model.FormId);

            OperateResult.Data = this.FormInfoService.GetTabFields(model.FormId);

            if (isTask) {
                OperateResult.dataList = this.FormInfoService.GetElementInfo(model.FormId);
            }

            return Json(OperateResult);
        }

        [HttpPost]
        public ActionResult CreateFormELement(List<FormElement> model) {

            this.FormInfoService.InsertFormElemenet(model);

            return Json(new ResultEntity() { result=true });
        }

        [HttpPost]
        public ActionResult GetFormInfoByid(int formId, int forminfoId=0)
        { 
           //this.FormInfoService

            var result = this.FormInfoService.GetForminfoSingle(formId, forminfoId);

            return JsonResult(result);
        }


        [HttpPost]
        public ActionResult AutoCompleteDBName(string q) {

            var result = this.FormInfoService.GetDBName(q);

            return Json(result);
        }

        [HttpPost]
        public ActionResult GetDBFieldsByDBName(string DBName) {

            var result = this.FormInfoService.GetDBKeys(DBName);

            return Json(result);
        }

        [HttpPost]
        public ActionResult SelectBindForm() {

            var result = this.FormInfoService.GetFormInfoBind();

            return JsonResult(result);
        }

    }
}
