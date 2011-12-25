using System;
using System.Web.Mvc;
using Complainatron.Core.Extensions;
using Complainatron.Core.Services;
using Complainatron.Models;

namespace Complainatron.Controllers
{
    public class ChartController : AbstractFacebookController
    {
        private readonly ITagService _tagService;

        public ChartController(IFacebookService facebookService, ILogService loggingService, ITagService tagService)
            : base(facebookService, loggingService)
        {
            if (tagService == null)
            {
                throw new ArgumentNullException("tagService");
            }

            _tagService = tagService;
        }

        public ActionResult Index()
        {
            var tagsWithCount = _tagService.GetTagsWithCount();
            var json = tagsWithCount.ToChartJson(t => t.Name, t => t.Count);
            return View(new ChartModel() { ChartData = json});
        }
    }
}
