using System.Web.Mvc;
using Complainatron.Core.Services;
using Complainatron.Models;

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
            var model = new FriendsIndexModel() { Friends = friends };
            return PartialView("_Index", model);
        }
    }
}
