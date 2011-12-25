using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complainatron.Models
{
    public class ComplaintViewModel
    {
        public Guid Id { get; set; }
        public string ComplaintText { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string FacebookUserName { get; set; }
        public long FacebookUserId { get; set; }
        public string DateSubmitted { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
        public ComplaintSeverityViewModel Severity { get; set; }

        public ComplaintViewModel()
        {
            Tags = Enumerable.Empty<TagViewModel>();
        }

        public string SeverityCssClass
        {
            get
            {
                return Severity.Name.Replace(" ", String.Empty);
            }
        }

        public string FacebookUserPhoto
        {
            get
            {
                return String.Format("https://graph.facebook.com/{0}/picture", FacebookUserId);
            }
        }
    }
}