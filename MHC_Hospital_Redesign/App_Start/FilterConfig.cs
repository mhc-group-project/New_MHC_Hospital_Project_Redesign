using System.Web;
using System.Web.Mvc;

namespace MHC_Hospital_Redesign
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
