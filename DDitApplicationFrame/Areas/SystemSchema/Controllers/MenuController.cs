using DDit.Component.Tools;
using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.SystemEntity;
using DDit.Core.Data.Entity.SystemEntity.DoEntity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class MenuController : BaseController
    {
        //
        // GET: /Menu/

        public ActionResult Index()
        {
            ViewBag.Button=this.ButtonRepository.GetButtonList();
            return View();
        }

        public ActionResult ParentMenu() {

            var pm= this.MenuRepository.GetParentMenu();

            var pmjson = SerializeObject(pm);

            return Content(pmjson);
        }

        [HttpPost]
        public ActionResult GetMenuList(Menu Menumodel) {

            var menulist = this.MenuRepository.GetMenuList(Menumodel);

            return JsonResult<MenuDo>(menulist); 
        }

        [HttpPost]
        public ActionResult GetMenuBtnById(int menuId) {

            var menuBtnList = this.ButtonRepository.GetBtnByMenuId(menuId);

            return JsonResult(menuBtnList);
        }

        [HttpPost]
        public ActionResult AddOrEditMenuInfo([ModelBinder(typeof(CustomModelBind))]Menu Menumodel)
        {
            var menuModel = this.MenuRepository.OrderAssignment(Menumodel);

            if (Menumodel.MenuID == 0)
            {
                this.MenuRepository.AddMenu(menuModel);
            }
            else {
                this.MenuRepository.ModifyMenu(menuModel);
            }

            return Json(new ResultEntity() { result=true});
        }

        [HttpPost]
        public ActionResult RemoveMenu(int menuID) {
            this.MenuRepository.DeleteMenu(menuID);
            return Json(new ResultEntity() { result = true,message="信息删除成功！" });
        }

    }
}
