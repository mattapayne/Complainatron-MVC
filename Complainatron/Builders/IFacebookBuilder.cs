using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Core.DTOs;

namespace Complainatron.Builders
{
    public interface IFacebookBuilder
    {
        MeDTO BuildMe(dynamic me);
        FacebookFriendDTO BuildFacebookFriend(dynamic friend);
    }
}
