using DDit.Core.Data.IRepositories.ISystemRepositories;
using DDit.Component.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using DDit.Core.Data.Entity.SystemEntity;
using Autofac;
using Newtonsoft.Json.Converters;
using DDit.Core.Data.Entity;
using DDit.Core.Data.IRepositories.ICoreRepositories;


namespace DDitApplicationFrame.Common
{
    public class BaseController : Controller
    {
      
        public User UserInfo { get {

            var cc = User.Identity.IsAuthenticated;

            var userName = User.Identity.Name;

            if (CacheHelp.GetCache(userName) != null)
                return CacheHelp.GetCache(userName) as User;
               
            HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
 
            if( cookie == null || string.IsNullOrEmpty(cookie.Value) )
                return null;
    
            User userData = null;
            // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

            if( ticket != null && string.IsNullOrEmpty(ticket.UserData) == false )
                userData = JsonConvert.DeserializeObject<User>(ticket.UserData);

            CacheHelp.SetCache(userName, userData,DateTime.Now.AddHours(2));

            return userData;
          
           } 
        }

        public string GetClientIp()
        {
            //可以透过代理服务器
            string userIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //判断是否有代理服务器
            if (string.IsNullOrEmpty(userIP))
            {
                //没有代理服务器,如果有代理服务器获取的是代理服务器的IP
                userIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return userIP;
        }

        #region systemService

        public IUserRepository UserRepository { get; set; }

        public IRoleRepository RoleRepository { get; set; }

        public IMenuRepository MenuRepository { get; set; }

        public IButtonRepository ButtonRepository { get; set; }

        public IMenuAndBtnRepository MPRepository { get; set; }

        public ILoginLogRepository LoginLogRepository { get; set; }

        public IRoleAndBtnRepository RBRepository { get; set; }

        public IDictionaryCategoryRepository DicCategory { get; set; }

        public IDictionaryRepository DicValue { get; set; }

        public IMessageRepository MessageServer { get; set; }

        public IUserAndMessageRepository UserAndMeServer { get; set; }

        #endregion

        #region coreService

        public INewsRepository NewService { get; set; }

        #endregion

        public ContentResult JsonResult<T>(Tuple<int, List<T>> data) where T : class
        {

            var jsonresult = SerializeObject(new ResultEntity { recordsFiltered = data.Item1, recordsTotal = data.Item1, dataList = data.Item2 });

            return Content(jsonresult);
        }

        public static string SerializeObject(object obj)
        {
            IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm" };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                //设置转换时间类型
                Converters = new List<JsonConverter> { dtConverter },
                //忽略深度循环
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

    }


}