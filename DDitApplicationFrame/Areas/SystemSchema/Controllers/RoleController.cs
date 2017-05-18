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
    public class RoleController : BaseController
    {
        //
        // GET: /Role/

        public ActionResult Index()
        {
            var pm = this.MenuRepository.GetParentMenu();

            ViewBag.MenuList = pm;

            return View();
        }

        [HttpPost]
        public ActionResult GetRoleList(Role rolemodel) {

           var listmodel= this.RoleRepository.GetRoleList(rolemodel);

           return JsonResult<Role>(listmodel);
        }

        [HttpPost]
        public ActionResult ValidRoleName(Role model) {
            var rolemodel = RoleRepository.Validate(model);
            if (rolemodel != null)
            {
                return Content(model.RoleID != 0 ? "true" : "false");
            }
            else {
                return Content("true");
            }          
        }

        [HttpPost]
        public ActionResult AddOrEditRoleInfo(Role rolemodel) {

            if (rolemodel.RoleID == 0)
            {
                rolemodel.CreateTime = DateTime.Now;
                this.RoleRepository.AddRole(rolemodel);
            }
            else {
                rolemodel.UpdateTime = DateTime.Now;
                this.RoleRepository.ModifyRole(rolemodel);
            }

            return Json(new ResultEntity() { result = true });
        }

        [HttpPost]
        public ActionResult RemoveRole(int roleID) {
            var result = this.RoleRepository.RemoveRole(roleID);
            return Json(result);
        }

        [HttpPost]
        public ActionResult DistributionMenuAndButton(int RoleID, List<int> menuId, List<RoleMappingButton> modelList)
        {
            this.RoleRepository.AddMenuAndBtnOfRole(RoleID, menuId, modelList);

            return Json(new ResultEntity() { result = true });
        }

    }
}
