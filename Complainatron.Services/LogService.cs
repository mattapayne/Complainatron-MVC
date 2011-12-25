using System;
using Complainatron.Core.DataAccess;
using Complainatron.Core.Services;
using Complainatron.Domain;

namespace Complainatron.Services
{
    public class LogService : BaseService<Log>, ILogService
    {
        public LogService(IDbContext context)
            : base(context)
        {

        }
    }
}