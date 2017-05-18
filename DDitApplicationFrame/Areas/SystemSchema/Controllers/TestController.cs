using DDit.Core.Data.Repository;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DDit.Core.Data.Entity;
using System.Reflection;
using DDit.Component.Tools;

namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            var jsonString=JsonConvert.SerializeObject(new {
                 item="frist",
                 type="str"      
            });

            dynamic json = DynamicJson.Parse(jsonString);

            var cc=json.item;

            return View();
        }

        [HttpPost]
        public ActionResult Calendar(string start,string end) {

            return Json("");
        }

    }
}
