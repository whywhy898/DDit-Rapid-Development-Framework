using DDit.Core.Data.Entity.CoreEntity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Controllers
{
    public class NewsController : BaseController
    {
        //
        // GET: /News/

        //页面展示
        public ActionResult Index()
        {
            return View();
        }


        //获取数据集合
        [HttpPost]
        public ActionResult GetButtonList(News model)
        {
            //得到我们的数据集合 是一个tuple<int,list<News>>
            var result = this.NewService.GetNewsList(model);

            // JsonResult是本人自己封装的方法，
            // 解析tuple 并返回JSON格式的信息,具体看BaseController 里面的方法实现
            return this.JsonResult(result);
        } 

    }
}
