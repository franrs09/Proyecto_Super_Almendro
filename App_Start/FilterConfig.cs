using System.Web;
using System.Web.Mvc;

namespace Proyecto_Super_Almendro
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
