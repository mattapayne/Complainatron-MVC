using System;
using System.Linq;
using System.Web.Mvc;
using Complainatron.Core.DTOs;
using Complainatron.Core.Paging;
using Complainatron.Core.Services;
using Complainatron.Models;
using Complainatron.Domain;
using Complainatron.Builders;
using MvcPaging;
using Facebook.Web.Mvc;

namespace Complainatron.Controllers
{
    public class ComplaintController : AbstractFacebookController
    {
        private readonly IUserService _userService;
        private readonly IComplaintService _complaintService;
        private readonly ITagService _tagService;
        private readonly IComplaintSeverityService _complaintSeverityService;
        private readonly ITagBuilder _tagBuilder;
        private readonly IComplaintBuilder _complaintBuilder;

        public ComplaintController(IFacebookService facebookService, 
            ILogService loggingService,
            ITagService tagService, 
            IComplaintSeverityService complaintSeverityService,
            IComplaintService complaintService, 
            IUserService userService,
            ITagBuilder tagBuilder, 
            IComplaintBuilder complaintBuilder)
            : base(facebookService, loggingService)
        {
            if (complaintService == null)
            {
                throw new ArgumentNullException("complaintService");
            }

            if (tagService == null)
            {
                throw new ArgumentNullException("tagService");
            }

            if (complaintSeverityService == null)
            {
                throw new ArgumentNullException("complaintSeverityService");
            }

            if (tagBuilder == null)
            {
                throw new ArgumentNullException("tagBuilder");
            }

            if (complaintBuilder == null)
            {
                throw new ArgumentNullException("complaintBuilder");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            _userService = userService;
            _tagBuilder = tagBuilder;
            _complaintBuilder = complaintBuilder;
            _complaintSeverityService = complaintSeverityService;
            _tagService = tagService;
            _complaintService = complaintService;
        }

        public ActionResult Tag(Guid id, PagingInformation pagingInformation)
        {
            var tag = _tagService.Get(id);

            if (tag == null)
            {
                return this.CanvasRedirectToAction("Index");
            }

            var me = FacebookService.GetMe();
            var complaints = _complaintService.GetComplaintsByTag(pagingInformation, id);
            var complaintViewModels = complaints.Select(c => _complaintBuilder.BuildViewModel(c)).ToList();
            var pagedResults = new PagedList<ComplaintViewModel>(complaintViewModels, complaints.PageIndex, complaints.PageSize, complaints.TotalItemCount);

            var model = new IndexViewModel()
            {
                Me = me,
                Complaints = pagedResults,
                TagListUrl = Url.Action("Index", "Tag"),
                Title = String.Format("Complaints By Tag: {0}", tag.Name)
            };

            return View("Index", model);
        }

        public ActionResult Friend(long id, PagingInformation pagingInformation)
        {
            var user = _userService.FindByFacebookId(id);

            if (user == null)
            {
                return this.CanvasRedirectToAction("Index");
            }

            var me = FacebookService.GetMe();

            var complaints = _complaintService.GetComplaintsByFacebookId(pagingInformation, id);
            var complaintViewModels = complaints.Select(c => _complaintBuilder.BuildViewModel(c)).ToList();
            var pagedResults = new PagedList<ComplaintViewModel>(complaintViewModels, complaints.PageIndex, complaints.PageSize, complaints.TotalItemCount);

            var model = new IndexViewModel()
            {
                Me = me,
                Complaints = pagedResults,
                TagListUrl = Url.Action("Index", "Tag"),
                Title = String.Format("Complaints By: {0}", user.Name)
            };

            return View("Index", model);
        }

        public ActionResult Index(PagingInformation paging)
        {
            var me = FacebookService.GetMe();

            var complaints = _complaintService.PagedGetAll(paging);
            var complaintViewModels = complaints.Select(c => _complaintBuilder.BuildViewModel(c)).ToList();
            var pagedResults = new PagedList<ComplaintViewModel>(complaintViewModels, complaints.PageIndex, complaints.PageSize, complaints.TotalItemCount);

            var model = new IndexViewModel() { 
                Me = me,
                Complaints = pagedResults,
                TagListUrl = Url.Action("Index", "Tag"),
                Title = "Recent Complaints"
            };

            return View(model);
        }

        public ActionResult Complain()
        {
            var model = new CreateComplaintViewModel();
            PopulateCreateComplaintModel(model);
            return PartialView("_Complain", model);
        }

        [HttpPost]
        public ActionResult Complain(CreateComplaintViewModel model)
        {
            if (ModelState.IsValid)
            {
                var me = FacebookService.GetMe();

                var complaint = new Complaint()
                {
                    ComplaintText = model.ComplaintText,
                    FacebookUserId = me.FacebookUserId,
                    FacebookUserName = me.Name,
                };

                if (model.SelectedComplaintSeverityId.HasValue)
                {
                    var severity = _complaintSeverityService.Get(model.SelectedComplaintSeverityId.Value);
                    complaint.Severity = severity;
                }

                if (model.TagList.Any())
                {
                    foreach (var tagName in model.TagList)
                    {
                        var tag = _tagService.FindByName(tagName);

                        if (tag == null)
                        {
                            tag = new Tag() { 
                                Name = tagName
                            };
                        }

                        complaint.Tags.Add(tag);
                    }
                }

                var errors = _complaintService.Create(complaint);

                if (!errors.Any())
                {
                    if (model.PublishToWall)
                    {
                        FacebookService.Post("feed", new
                        {
                            message = model.ComplaintText,
                            picture = String.Format("{0}/Content/Images/angry smiley.png", Constants.RootUrl),
                            link = String.Format("{0}/Complaint/View/{1}", Constants.RootUrl, complaint.Id),
                            caption = "I just complained",
                            description = "I just posted a complaint on Complainatron",
                            source = String.Format("{0}/Complaint/View/{1}", Constants.RootUrl, complaint.Id),
                        });
                    }

                    var viewModel = _complaintBuilder.BuildViewModel(complaint);
                    return PartialView("_Complaint", viewModel);
                }
                else
                {
                    SetModelStateErrors(errors);
                }
            }

            PopulateCreateComplaintModel(model);
            return PartialView("_Complain", model);
        }

        private void PopulateCreateComplaintModel(CreateComplaintViewModel model)
        {
            var severities = _complaintSeverityService.GetAll();
            var defaultSeverity = severities.Where(s => s.IsDefault).FirstOrDefault();
            var tags = _tagService.GetAll();

            model.Severities = severities.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Selected = s.IsDefault,
                Value = s.Id.ToString()
            });

            model.SelectedComplaintSeverityId = defaultSeverity == null ? (Guid?)null : defaultSeverity.Id;
            model.ExistingTags = tags.Select(t => _tagBuilder.BuildViewModel(t)).ToList();
        }
    }
}
