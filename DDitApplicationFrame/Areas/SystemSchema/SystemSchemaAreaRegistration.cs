using System.Web.Mvc;

namespace DDitApplicationFrame.Areas.SystemSchema
{
    public class SystemSchemaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemSchema";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SystemSchema_default",
                "SystemSchema/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
