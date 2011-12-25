using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using Facebook.Web;
using Complainatron.Filters;
using Complainatron.Helpers;
using Complainatron.Core.Services;
using Complainatron.Domain.Validation;
using Complainatron.Core.Utility;

namespace Complainatron.Controllers
{
    [CanvasAuthorize(Permissions = PERMISSIONS)]
    [FacebookUserFilter(Order = 2)]
    public abstract class AbstractFacebookController : Controller
    {
        private const string PERMISSIONS = "user_about_me,email,publish_stream,user_photos";

        private readonly IFacebookService _facebookService;
        private readonly ILogService _loggingService;

        public AbstractFacebookController(IFacebookService facebookService, ILogService loggingService)
        {
            if (facebookService == null)
            {
                throw new ArgumentNullException("facebookService");
            }

            if (loggingService == null)
            {
                throw new ArgumentNullException("loggingService");
            }

            _loggingService = loggingService;
            _facebookService = facebookService;
        }

        protected static string Permissions
        {
            get
            {
                return PERMISSIONS;
            }
        }

        protected IFacebookService FacebookService
        {
            get
            {
                return _facebookService;
            }
        }

        protected ILogService LoggingService
        {
            get
            {
                return _loggingService;
            }
        }

        protected void SetModelStateErrors(IEnumerable<IValidationError> errors)
        {
            if (errors != null)
            {
                errors.ForEach(e => { ModelState.AddModelError(e.Property, e.ErrorMessage); });
            }
        }
    }
}
