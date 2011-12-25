using System;
using System.Web.Mvc;
using Complainatron.Core.Services;
using Complainatron.Domain;
using Complainatron.Services;

namespace Complainatron.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FacebookUserFilterAttribute : ActionFilterAttribute
    {
        public IUserService UserService { get; set; }
        public IFacebookService FacebookService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (FacebookService.IsAuthenticated)
            {
                var facebookId = FacebookService.CurrentFacebookUserId;

                var user = UserService.FindByFacebookId(facebookId);

                if (user == null)
                {
                    var me = FacebookService.GetMe();

                    user = new User() { 
                        Email = me.Email,
                        FacebookId = me.FacebookUserId,
                        Name = me.Name
                    };

                    UserService.Create(user);
                }
            }
        }
    }
}