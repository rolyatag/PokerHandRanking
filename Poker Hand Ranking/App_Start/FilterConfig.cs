using System.Web;
using System.Web.Mvc;

namespace Poker_Hand_Ranking
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
