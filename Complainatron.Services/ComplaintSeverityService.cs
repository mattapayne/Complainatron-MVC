using System;
using Complainatron.Core.DataAccess;
using Complainatron.Core.Services;
using Complainatron.Domain;

namespace Complainatron.Services
{
    public class ComplaintSeverityService : BaseService<ComplaintSeverity>, IComplaintSeverityService
    {
        public ComplaintSeverityService(IDbContext context)
            : base(context)
        {

        }
    }
}
