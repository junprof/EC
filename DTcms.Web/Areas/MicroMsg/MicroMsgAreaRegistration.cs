using System.Web.Mvc;

namespace DTcms.Web.Areas.MicroMsg
{
    public class MicroMsgAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MicroMsg";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MicroMsg_default",
                "MicroMsg/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}