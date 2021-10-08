using System.Web.Routing;

namespace Forum_v2.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Forum", "{ForumService.aspx}", "~/ajax/ForumService.aspx", true);
            routes.MapPageRoute("Authorize", "{AuthorizeService.aspx}", "~/ajax/AuthorizeService.aspx", true);
            //範例
            routes.MapPageRoute("Default", "{*anything}", "~/view/Default.aspx", false);
        }
    }
}