using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Complainatron.Models;
using Complainatron.Domain;

namespace Complainatron.Builders.Implementations
{
    public class ComplaintSeverityBuilder : IComplaintSeverityBuilder
    {
        public ComplaintSeverityViewModel BuildViewModel(ComplaintSeverity severity)
        {
            return new ComplaintSeverityViewModel() { 
                Id = severity.Id,
                IsDefault = severity.IsDefault,
                Name = severity.Name
            };
        }
    }
}