using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Complainatron.HtmlHelperExtensions
{
    public static class PagingExtensions
    {
        #region HtmlHelper extensions

        public static HtmlString FacebookPager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
        {
            return FacebookPager(htmlHelper, pageSize, currentPage, totalItemCount, null, null);
        }

        public static HtmlString FacebookPager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName)
        {
            return FacebookPager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, null);
        }

        public static HtmlString FacebookPager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, object values)
        {
            return FacebookPager(htmlHelper, pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values));
        }

        public static HtmlString FacebookPager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, object values)
        {
            return FacebookPager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, new RouteValueDictionary(values));
        }

        public static HtmlString FacebookPager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary)
        {
            return FacebookPager(htmlHelper, pageSize, currentPage, totalItemCount, null, valuesDictionary);
        }

        public static HtmlString FacebookPager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary)
        {
            if (valuesDictionary == null)
            {
                valuesDictionary = new RouteValueDictionary();
            }
            if (actionName != null)
            {
                if (valuesDictionary.ContainsKey("action"))
                {
                    throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
                }
                valuesDictionary.Add("action", actionName);
            }
            var pager = new FacebookPager(htmlHelper.ViewContext, pageSize, currentPage, totalItemCount, valuesDictionary);
            return pager.RenderHtml();
        }

        #endregion
    }
}