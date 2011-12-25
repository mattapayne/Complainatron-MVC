using System;
using System.Web.Mvc;
using Complainatron.Core.Extensions;
using Complainatron.Core.Services;
using Complainatron.Models;

namespace Complainatron.Controllers
{
    public class MapController : AbstractFacebookController
    {
        private readonly IComplaintService _complaintService;

        public MapController(IFacebookService facebookService, ILogService loggingService, 
            IComplaintService complaintService)
            : base(facebookService, loggingService)
        {
            if (complaintService == null)
            {
                throw new ArgumentNullException("complaintService");
            }

            _complaintService = complaintService;
        }

        public ActionResult Index()
        {
            var json = _complaintService.GetAll().ToMapJson();
            return View(new MapModel() { MapData = json });
        }
    }
}
