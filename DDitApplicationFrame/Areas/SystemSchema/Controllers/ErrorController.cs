using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Areas.SystemSchema.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// 404页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Path404()
        {
            return View("NotFound");
        }
        /// <summary>
        /// 500页面
        /// </summary>
        /// <returns></returns>

        [ValidateInput(false)]
        public ActionResult Path500(ErrorMessage msg)
        {
           
            ViewData = new ViewDataDictionary<ErrorMessage>(msg);
            return View("ISE");
        }

    }
}
