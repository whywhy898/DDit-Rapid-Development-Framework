using DDitApplicationFrame.Common;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandleAttribute());
        }
    }
}