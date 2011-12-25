using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Core.DTOs;

namespace Complainatron.Core.Services
{
    public interface IFacebookService
    {
        bool IsAuthenticated { get; }
        long CurrentFacebookUserId { get; }
        MeDTO GetMe();
        IEnumerable<FacebookFriendDTO> GetFriends();
        void Post(string action, object args);
    }
}
