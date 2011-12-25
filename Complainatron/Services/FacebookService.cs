using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Complainatron.Core.Services;
using Facebook.Web;
using Complainatron.Core.DTOs;
using Complainatron.Helpers;
using Complainatron.Core.Utility;
using Complainatron.Builders;

namespace Complainatron.Services
{
    public class FacebookService : IFacebookService
    {
        private FacebookWebClient _facebookWebClient;
        private readonly IFacebookBuilder _builder;

        public FacebookService(IFacebookBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            _builder = builder;
        }

        public bool IsAuthenticated
        {
            get { return FacebookWebContext.Current.IsAuthenticated(); }
        }

        public long CurrentFacebookUserId
        {
            get { return FacebookWebContext.Current.UserId; }
        }

        public MeDTO GetMe()
        {
            dynamic me = FbClient.Get(FacebookHelper.MeQuery);

            if (me != null)
            {
                return _builder.BuildMe(me);
            }

            return null;
        }

        public IEnumerable<FacebookFriendDTO> GetFriends()
        {
            dynamic friends = FbClient.Get(FacebookHelper.FriendsQuery);
            var data = friends.data as IEnumerable<dynamic>;

            var results = new List<FacebookFriendDTO>();

            data.ForEach(f => {
                if (f.installed != null && f.installed)
                {
                    results.Add(_builder.BuildFacebookFriend(f));
                }
            });

            return results;
        }

        public void Post(string action, object args)
        {
            FbClient.Post(action, args);
        }

        private FacebookWebClient FbClient
        {
            get
            {
                if (_facebookWebClient == null)
                {
                    _facebookWebClient = new FacebookWebClient(FacebookWebContext.Current);
                }

                return _facebookWebClient;
            }
        }
    }
}