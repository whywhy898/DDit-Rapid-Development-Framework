
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;

namespace DDitApplicationFrame.Service.Imp
{
    public class LoginService : LoginContract
    {

        public ILoginLogRepository Loginlog { get; set; }

        public override Tuple<bool,string> Login(User model,string IP)
        
        {
            Tuple<bool, string,User> result = base.LoginJudge(model);

            if (result.Item1)
            {
                DateTime expiration = DateTime.Now.Add(FormsAuthentication.Timeout);

                var userData = JsonConvert.SerializeObject(result.Item3);

                FormsAuthenticationTicket ticker = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddDays(1), true, userData, FormsAuthentication.FormsCookiePath);

                string encryptedTicket = FormsAuthentication.Encrypt(ticker);

                var cookies = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                cookies.Expires = expiration;

                HttpContext.Current.Response.Cookies.Add(cookies);

                Loginlog.AddLoginlog(new LoginLog() { LoginIP = IP, LoginName = result.Item3.UserName, LoginNicker = result.Item3.UserReallyname, LoginTime = DateTime.Now });
            }

            return new Tuple<bool, string>(result.Item1, result.Item2);
        }
    }
}