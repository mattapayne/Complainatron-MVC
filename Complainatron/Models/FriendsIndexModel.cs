using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Complainatron.Core.DTOs;

namespace Complainatron.Models
{
    public class FriendsIndexModel
    {
        public IEnumerable<FacebookFriendDTO> Friends { get; set; }

        public FriendsIndexModel()
        {
            Friends = Enumerable.Empty<FacebookFriendDTO>();
        }
    }
}