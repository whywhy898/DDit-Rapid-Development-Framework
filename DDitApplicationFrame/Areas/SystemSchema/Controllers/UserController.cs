using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.SystemEntity;
using DDitApplicationFrame.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            var roleList = RoleRepository.GetRoleItem();
            var bumen = this.DicCategory.DiCategoryList().Where(a=>a.Category=="部门").FirstOrDefault();
            var bumenlist = bumen.DicValueList.Where(a=>a.Enabled==true).ToList();
            ViewBag.bumen = bumenlist;
            ViewBag.jsonbumen = SerializeObject(bumenlist);

            ViewBag.role = roleList;
            return View();
        }

        public ActionResult ValidUserNameOrMobile(User model)
        {
            var userModel = UserRepository.GetSingle(model);
            if (userModel != null)
            {
                return Content(model.UserID > 0 && model.UserID == userModel.UserID ? "true" : "false");
            }
            else {
                return Content("true");
            }
        }

        public ActionResult ValidUserPassword(string oldPassword)
        {
            return Content(this.UserInfo.UserPassword == oldPassword ? "true" : "false");
        }

        [HttpPost]
        public ActionResult ModifyPassword(string password) { 
            var  user=this.UserInfo;
            user.UserPassword=password;
            this.UserRepository.ModifyUser(user);
            return Json(new ResultEntity { result = true });
        }

        [HttpPost]
        public ActionResult GetUserList(User usermodel = null)
        {
            var ListModel = this.UserRepository.GetList(usermodel);
            return this.JsonResult(ListModel);
        }

        [HttpPost]
        public ActionResult AddorEditUserInfo(User userModel, HttpPostedFileBase Portrait)
        {
            string fileName = string.Empty;
            if (Portrait != null && Portrait.ContentLength > 0)
            {
                fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(Portrait.FileName).Replace("&","");
                var sysPath=Server.MapPath("~/heardImg");
                if (!Directory.Exists(sysPath)) Directory.CreateDirectory(sysPath);
                var filePath = Path.Combine(sysPath, fileName);
                Portrait.SaveAs(filePath);
            }
            if (userModel.UserID == 0)
            {
                userModel.IsEnable = true;
                userModel.CreateTime = DateTime.Now;
                userModel.HeadPortrait = fileName; 
                this.UserRepository.AddUser(userModel);
            }
            else
            {
                userModel.HeadPortrait = fileName == "" ? userModel.HeadPortrait : fileName;
                userModel.UpdateTime = DateTime.Now;
                this.UserRepository.ModifyUser(userModel);
            }

            return Json(new ResultEntity{result=true});
        }

        [HttpPost]
        public ActionResult DisenableUser(User model) {

            this.UserRepository.DeleteUser(model);

            return Json(new ResultEntity { result = true });
        }

        [HttpPost]
        public ActionResult DistributionRole(int UserId,List<int> RoleIDlist) {

            this.UserRepository.SetUserInfoRole(UserId, RoleIDlist);

            return Json(new ResultEntity { result = true });
        }

        public FilePathResult GetPortrait(string imgName)
        {
            if (imgName == "" || imgName == "null") imgName = "moren.jpg";
             
            string filePath = Path.Combine(Server.MapPath("/heardImg"), imgName);

            return File(filePath, "image/jpeg");
        }

        [HttpPost]
        public ActionResult AutoCompleteUser(string q) {

            var result= this.UserRepository.GetUserInfobyName(q);

            return Json(result); 
        }

        [HttpPost]
        public ActionResult ResetUserPWD(int userId) {

            this.UserRepository.ResetUserPWDbyID(userId);
            return Json(new ResultEntity() { result = true });
        }

    }
}
