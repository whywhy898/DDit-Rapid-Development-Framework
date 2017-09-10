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
using DDit.Core.Data.IRepositories.IFormRepositories;
using Newtonsoft.Json.Serialization;
using DDit.Core.Data.IRepositories.IWorkFlowRepositories;


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

        #region formService

        public IFormInfoRepository FormInfoService { get; set; }

        public IElementInfoRepository ElementInfoService { get; set; }

        #endregion

        #region flowService

        public IWorkFlowRepository WorkFlowService { get; set; }

        public IFlowTaskRepository FlowTaskService { get; set; }

        #endregion

        #region coreService

        public INewsRepository NewService { get; set; }

        #endregion

        /// <summary>
        /// 返回json序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="timeFarmt">时间格式默认是false带时分，true不带</param>
        /// <returns></returns>
        public ContentResult JsonResult<T>(Tuple<int, List<T>> data, bool timeFarmt = false) where T : class
        {

            var jsonresult = SerializeObject(new ResultEntity { recordsFiltered = data.Item1, recordsTotal = data.Item1, dataList = data.Item2 }, timeFarmt);

            return Content(jsonresult);
        }

        public ContentResult JsonResult(object obj, bool timeFarmt = false)
        {
            var jsonresult = SerializeObject(obj, timeFarmt);

            return Content(jsonresult);
        }

        public static string SerializeObject(object obj,bool timeFarmt=false)
        {
            IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat =timeFarmt?"yyyy-MM-dd":"yyyy-MM-dd HH:mm" };
            var propNames = new string[] { "pageIndex", "pageSize", "draw", "order", "pageSize" };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                //设置转换时间类型
                Converters = new List<JsonConverter> { dtConverter },
                //忽略深度循环
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //忽略BaseEntity.cs中的属性，之所以在这里设置是因为【jsonIgnore】和【NotMapped】特性有时候会冲突，特性会被忽略
                ContractResolver=new LimitPropsContractResolver(propNames,false)
            });
        }

    }

    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] props = null;

        bool retain;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="props">传入的属性数组</param>
        /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        public LimitPropsContractResolver(string[] props, bool retain = true)
        {
            //指定要序列化属性的清单
            this.props = props;

            this.retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type,

        MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list =
            base.CreateProperties(type, memberSerialization);
            //只保留清单有列出的属性
            return list.Where(p =>
            {
                if (retain)
                {
                    return props.Contains(p.PropertyName);
                }
                else
                {
                    return !props.Contains(p.PropertyName);
                }
            }).ToList();
        }
    }
}