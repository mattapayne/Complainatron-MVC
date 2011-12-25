using System.Web.Mvc;
using Complainatron.Core.Services;

namespace Complainatron.Controllers
{
    public class FriendsController : AbstractFacebookController
    {
        public FriendsController(IFacebookService facebookService, ILogService loggingService)
            : base(facebookService, loggingService)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            var friends = FacebookService.GetFriends();
            return PartialView("_Index", friends);
        }
    }
}
