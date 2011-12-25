using System;
using System.Linq;
using Complainatron.Core.DataAccess;
using Complainatron.Core.Paging;
using Complainatron.Core.Services;
using Complainatron.Domain;
using MvcPaging;

namespace Complainatron.Services
{
    public class ComplaintService : BaseService<Complaint>, IComplaintService
    {
        public ComplaintService(IDbContext context)
            : base(context)
        {

        }

        public IPagedList<Complaint> GetComplaintsByTag(IPagingInformation pagingInformation, Guid tagId)
        {
            return base.PagedGetAll(pagingInformation, c => c.Tags.Any(t => t.Id == tagId));
        }

        public IPagedList<Complaint> GetComplaintsByFacebookId(IPagingInformation pagingInformation, long facebookId)
        {
            return base.PagedGetAll(pagingInformation, c => c.FacebookUserId == facebookId);
        }
    }
}