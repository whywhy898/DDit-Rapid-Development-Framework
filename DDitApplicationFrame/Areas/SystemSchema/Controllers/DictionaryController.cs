using DDit.Core.Data.Entity;
using DDit.Core.Data.SystemEntity.Entity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class DictionaryController : BaseController
    {
        //
        // GET: /Dictionary/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDicValueList(Dictionary model) {

            var result = this.DicValue.GetDictionaryList(model);

            return this.JsonResult(result);          
        }


        public ActionResult GetGetDicategoryList() {
           return Json(this.DicCategory.DiCategoryList().Select(m=>new {
                title=m.Category,
                folder=true,
                dicid=m.ID
            }),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationDicategory(DictionaryCategory model) {
        
            if (model.ID == 0)
            {
                model.CreateTime = DateTime.Now;
                model.Enabled = true;
                this.DicCategory.AddDic(model);
            }
            else
            {
                model.UpdateTime = DateTime.Now;
                this.DicCategory.ModifyDic(model);
            }
            return Json(new ResultEntity { result = true });
        }

        [HttpPost]
        public ActionResult OperationDicValue(Dictionary model) {
            model.Enabled = Request.Form["Enabled"] != null ? true : false;

            if (model.ID == 0)
            {
                model.CreateTime = DateTime.Now;
                this.DicValue.AddDic(model);
            }
            else
            {
                model.UpdateTime = DateTime.Now;
                this.DicValue.ModifyDic(model);
            }
            return Json(new ResultEntity { result = true });

        }

    }
}
