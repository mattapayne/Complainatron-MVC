using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Complainatron.Domain;
using Complainatron.Models;

namespace Complainatron.Builders.Implementations
{
    public class ComplaintBuilder : IComplaintBuilder
    {
        private readonly IComplaintSeverityBuilder _complaintSeverityBuilder;
        private readonly ITagBuilder _tagBuilder;

        public ComplaintBuilder(IComplaintSeverityBuilder complaintSeverityBuilder, ITagBuilder tagBuilder)
        {
            if (complaintSeverityBuilder == null)
            {
                throw new ArgumentNullException("complaintSeverityBuilder");
            }

            if (tagBuilder == null)
            {
                throw new ArgumentNullException("tagBuilder");
            }

            _complaintSeverityBuilder = complaintSeverityBuilder;
            _tagBuilder = tagBuilder;
        }

        public ComplaintViewModel BuildViewModel(Complaint complaint)
        {
            return new ComplaintViewModel() { 
                ComplaintText = complaint.ComplaintText,
                DateSubmitted = complaint.DateCreated.ToShortDateString(),
                FacebookUserId = complaint.FacebookUserId,
                FacebookUserName = complaint.FacebookUserName,
                Id = complaint.Id,
                Latitude = complaint.Latitude,
                Longitude = complaint.Longitude,
                Severity = _complaintSeverityBuilder.BuildViewModel(complaint.Severity),
                Tags = complaint.Tags.Select(t => _tagBuilder.BuildViewModel(t)).ToList()
            };
        }
    }
}