using System;
using System.Linq;
using Complainatron.Core.DataAccess;
using Complainatron.Core.Services;
using Complainatron.Domain;

namespace Complainatron.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IDbContext context)
            : base(context)
        {

        }

        public User FindByFacebookId(long facebookId)
        {
            return DbContext.Set<User>().Where(u => u.FacebookId == facebookId).FirstOrDefault();
        }
    }
}