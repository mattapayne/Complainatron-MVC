using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Complainatron.Core.DTOs;

namespace Complainatron.Builders.Implementations
{
    public class FacebookBuilder : IFacebookBuilder
    {
        public MeDTO BuildMe(dynamic me)
        {
            return new MeDTO()
            {
                Email = me.email,
                FacebookUserId = Int64.Parse(me.id),
                Name = me.name,
                PicureUrl = me.picture
            };
        }

        public FacebookFriendDTO BuildFacebookFriend(dynamic friend)
        {
            return new FacebookFriendDTO()
            {
                Id = Int64.Parse(friend.id),
                Name = friend.name,
                PictureUrl = friend.picture
            };
        }
    }
}