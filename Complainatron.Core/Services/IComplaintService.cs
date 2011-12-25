using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Domain;
using MvcPaging;
using Complainatron.Core.Paging;

namespace Complainatron.Core.Services
{
    public interface IComplaintService : IService<Complaint>
    {
        IPagedList<Complaint> GetComplaintsByTag(IPagingInformation pagingInformation, Guid tagId);
        IPagedList<Complaint> GetComplaintsByFacebookId(IPagingInformation pagingInformation, long facebookId);
    }
}
