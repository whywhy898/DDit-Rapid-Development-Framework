using DDit.Core.Data.Entity;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DDitApplicationFrame.Service
{
    public abstract class LoginContract : ILoginService
    {

        public IUserRepository UserRepository { get; set; }

        public Tuple<bool, string, User> LoginJudge(User model)
        {
            var message = string.Empty;
            var falg=true;
            try
            {
                var User = UserRepository.GetSingle(model);

                if (User == null)
                {
                    message = "指定账号的用户不存在。";
                    falg = false;
                }
                else if (!User.UserPassword.Equals(model.UserPassword))
                {
                    message = "登录密码不正确。";
                    falg = false;
                }
                else if (!User.IsEnable)
                {
                    message = "您的账号已被禁用无法登陆。";
                    falg = false;
                }
                return new Tuple<bool, string, User>(falg, message, User);
            }catch(Exception ex){
                message = "数据库连接异常，请与管理员联系";
                falg = false;
                return new Tuple<bool, string, User>(falg, message,new User());
            }
           
        }

        public abstract Tuple<bool, string> Login(User model,string IP);

        public virtual void Logon()
        {
            FormsAuthentication.SignOut();
        }

    }
}