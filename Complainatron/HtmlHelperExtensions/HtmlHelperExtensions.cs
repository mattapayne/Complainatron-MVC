using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using System.Web.Mvc.Html;
using Facebook.Web;

namespace Complainatron.HtmlHelperExtensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString FacebookApplicationId(this HtmlHelper helper)
        {
            return helper.Hidden("FacebookApplicationId", FacebookWebContext.Current.Settings.AppId);
        }
    }
}