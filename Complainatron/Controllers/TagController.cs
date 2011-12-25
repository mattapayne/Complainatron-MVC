using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complainatron.Core.Services;
using Complainatron.Builders;

namespace Complainatron.Controllers
{
    public class TagController : AbstractFacebookController
    {
        private readonly ITagService _tagService;
        private readonly ITagBuilder _tagBuilder;

        public TagController(IFacebookService facebookService, ILogService loggingService, ITagService tagService, ITagBuilder tagBuilder)
            : base(facebookService, loggingService)
        {
            if (tagService == null)
            {
                throw new ArgumentNullException("tagService");
            }

            if (tagBuilder == null)
            {
                throw new ArgumentNullException("tagBuilder");
            }

            _tagService = tagService;
            _tagBuilder = tagBuilder;
        }

        public ActionResult Index()
        {
            var tags = _tagService.GetAll();
            var models = tags.Select(t => _tagBuilder.BuildViewModel(t)).ToList();
            return PartialView(models);
        }
    }
}
