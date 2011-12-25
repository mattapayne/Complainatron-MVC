using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Domain;
using Complainatron.Models;

namespace Complainatron.Builders
{
    public interface IComplaintBuilder
    {
        ComplaintViewModel BuildViewModel(Complaint complaint);
    }
}
