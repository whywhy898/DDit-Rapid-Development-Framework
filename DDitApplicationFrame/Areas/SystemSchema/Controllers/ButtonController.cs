using DDit.Core.Data.Entity;
using DDit.Core.Data.SystemEntity.Entity;
using DDitApplicationFrame.Common;
using DDit.Component.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class ButtonController : BaseController
    {
        //
        // GET: /Button/

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetButtonList(Button model) {
            var result = this.ButtonRepository.GetButtonList(model);
 
            return this.JsonResult(result);
        }

        [HttpPost]
        public ActionResult GetButtonItem() {
            var btnModel = this.ButtonRepository.GetButtonList();
            return Content(SerializeObject(btnModel));
        }

        [HttpPost]
        public ActionResult AddorEditButtonInfo(Button model) {
            if (model.ButtonID == 0)
            {
                model.CreateTime = DateTime.Now;
                this.ButtonRepository.AddBtn(model);
            }
            else {
                model.UpdateTime = DateTime.Now;
                this.ButtonRepository.ModifyBtn(model);
            }
            return Json(new ResultEntity {result=true });
        }




        [HttpPost]
        public ActionResult MenuMappingButton(int MenuID,List<MenuMappingButton> mplist) {
            this.MPRepository.MenuMapBtn(MenuID, mplist);

            return Json(new ResultEntity { result = true });
        }

        public ActionResult RemoveButton(int btnID) {

            var list = this.MPRepository.GetMBList(new MenuMappingButton { ButtonID = btnID });
            if (list.Count ==0)
            {
                this.ButtonRepository.DeleteBtn(btnID);
                return Json(new ResultEntity { result = true });
            }

            return Json(new ResultEntity { result=false,message="该按钮已经绑定菜单，无法删除！"});
        }

        [ChildActionOnly]
        public ActionResult CreateButtonByMuen(int menuId, string mark)
        {
            var rolelist = UserRepository.GetbyID(UserInfo.UserID).RoleList;

            var buttonList = new List<Button>();

            var mb = this.MenuRepository.GetSingleMenu(menuId).mbList.OrderBy(a=>a.OrderBy).ToList();

            foreach (var role in rolelist)
            {
                mb.ForEach(m =>
                {
                    var hasmb = role.rbList.Where(a => a.MenuID == m.MenuID && a.ButtonID == m.ButtonID).FirstOrDefault();
                    if (hasmb != null)
                    {
                        buttonList.Add(this.ButtonRepository.GetSingleBtnbyID(m.ButtonID));
                    }
                });
            }

            ViewBag.mark = mark;
            
            var newButton = buttonList.DistinctBy(a => a.ButtonID);

            buttonList = new List<Button>();
            mb.ForEach(m =>
            {
                buttonList.Add(newButton.Where(a=>a.ButtonID==m.ButtonID).FirstOrDefault());
            });

            return PartialView(buttonList);
        }

    }
}
