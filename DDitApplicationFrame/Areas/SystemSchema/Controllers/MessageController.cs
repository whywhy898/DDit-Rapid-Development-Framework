using DDit.Component.Tools;
using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.SystemEntity;
using DDit.Core.Data.Entity.SystemEntity.DoEntity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class MessageController : BaseController
    {
        //
        // GET: /SystemSchema/Message/

        public ActionResult Index()
        {
            //得到部门人
            ViewBag.DepartmentUser = this.MessageServer.GetDepartmentInfo();

            return View();
        } 

        [HttpPost]
        public ActionResult GetMessagesInfo([ModelBinder(typeof(CustomModelBind))]Message model)
        {
            var result = this.MessageServer.GetMessageList(model);
            return JsonResult<MessageDo>(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAndEditMessage([ModelBinder(typeof(CustomModelBind))]Message model)
        {

            if (model.MessageID == 0)
            {
                if (model.IsSendEmail) model.SendEmailState = 1;
                model.SendTime = DateTime.Now;
                model.SendUser = UserInfo.UserID;
                this.MessageServer.InsertMessage(model);
                if (model.IsSendEmail) SendEmailAsync(model);
            }
            else
            {
                this.MessageServer.ModifyMessage(model);
            }
            return Json(new ResultEntity() { result=true });
        }

        [HttpPost]
        public ActionResult GetSingle(int mesId)
        { 
          var message=this.MessageServer.GetMessageSingle(mesId);
          return Json(new ResultEntity() { result = true, Data = message });
        }
       
        [HttpPost]
        public ActionResult DeleteMessageInfo(int mesId)
        {
            this.MessageServer.RemoveMessage(mesId);
            return Json(new ResultEntity() { result = true,message="消息删除成功！" });
        }

        [HttpPost]
        public ActionResult GetMesByUser(bool isAll) {
            var id = UserInfo.UserID;

            var result = this.UserAndMeServer.GetMesByUser(id,isAll);
            
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetMessagebyUser(UserMappingMessage model)
        {
            model.UserID = UserInfo.UserID;
            var result = this.UserAndMeServer.GetMessagebyUser(model);

            return JsonResult<UserMappingMessage>(result);
        }

        [HttpPost]
        public ActionResult SetRead(UserMappingMessage model)
        {
            this.UserAndMeServer.SetMessageRead(model);

            return Json(new ResultEntity() { result = true });
        }

        [HttpPost]
        public ActionResult AgainSendEmail(int messageId) {
            var mesinfo= this.MessageServer.GetMessageSingle(messageId);

            mesinfo.SendEmailState = 1;

            this.MessageServer.SetSendState(mesinfo);

            SendEmailAsync(mesinfo);

            return Json(new ResultEntity() { result = true });
        }

        public ActionResult UserMessagePage() {

            return View();
        }

        public void SendEmailAsync(Message model)
        {
            Task.Run(() =>
            {
                try
                {
                    var reclist = string.Empty;
                    foreach (var item in model.RecUser.Split(','))
                    {
                        var userinfo = this.UserRepository.GetbyID(int.Parse(item));
                        if (!string.IsNullOrEmpty(userinfo.Email))
                        {
                            reclist += userinfo.Email + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(reclist))
                    {
                        reclist = reclist.Substring(0, reclist.Length - 1);
                        EmailHelper email = new EmailHelper(reclist, model.MessageTitle, model.MessageText);
                        email.Send();
                    }
                    model.SendEmailState = 2;
                    this.MessageServer.SetSendState(model);
                }catch(Exception ex){
                    new LogHelper().LogError("发送邮件异常" + ex);
                    model.SendEmailState = 3;
                    this.MessageServer.SetSendState(model);    
                }
            });
        }

    }
}
