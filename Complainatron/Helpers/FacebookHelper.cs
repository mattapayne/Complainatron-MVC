using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complainatron.Helpers
{
    public static class FacebookHelper
    {
        private static readonly IEnumerable<string> _meFields = new[] { "name", "email", "picture" };
        private static readonly IEnumerable<string> _friendsFields = new[] { "installed", "name", "picture" };

        public static string MeQuery
        {
            get
            {
                return String.Format("me?fields={0}", String.Join(",", _meFields.ToArray()));
            }
        }

        public static string FriendsQuery
        {
            get
            {
                return String.Format("me/friends?fields={0}", String.Join(",", _friendsFields.ToArray()));
            }
        }
    }
}