using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Complainatron.Core.DTOs;
using System.Web.Mvc;

namespace Complainatron.Models
{
    public class CreateComplaintViewModel
    {
        [Required(ErrorMessage = "We need your complaint.")]
        [Display(Name = "Your Complaint")]
        [StringLength(255, ErrorMessage = "Your complaint may only be 255 characters in length.")]
        public string ComplaintText { get; set; }

        public string IPAddress { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        [Display(Name = "Publish to your wall?")]
        public bool PublishToWall { get; set; }

        [Display(Name = "Complaint Severity")]
        [Required(ErrorMessage = "Please select a severity.")]
        public Guid? SelectedComplaintSeverityId { get; set; }

        public IEnumerable<SelectListItem> Severities { get; set; }

        public IEnumerable<TagViewModel> ExistingTags { get; set; }

        public IEnumerable<string> TagList
        {
            get
            {
                if (!String.IsNullOrEmpty(Tags))
                {
                    return Tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                }

                return Enumerable.Empty<string>();
            }
        }

        public CreateComplaintViewModel()
        {
            Severities = Enumerable.Empty<SelectListItem>();
            ExistingTags = Enumerable.Empty<TagViewModel>();
        }
    }
}