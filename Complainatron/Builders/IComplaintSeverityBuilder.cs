using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Models;
using Complainatron.Domain;

namespace Complainatron.Builders
{
    public interface IComplaintSeverityBuilder
    {
        ComplaintSeverityViewModel BuildViewModel(ComplaintSeverity severity);
    }
}
