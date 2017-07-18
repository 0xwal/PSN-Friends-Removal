using PSNLib.Models;
using TheHttp;

namespace PSNLib
{
    public class Friend
    {
        private readonly PSAPI _context;
        private Friend() { }
        private dynamic _getFriends(int start = 0, int limit = 20, FriendsFilter filter = FriendsFilter.All)
        {
            string f = filter == FriendsFilter.All ? "" : "userFilter=online";
            return Http.Get($"https://us-prof.np.community.playstation.net/userProfile/v1/users/me/friends/profiles2?fields=onlineId,avatarUrls,plus,aboutMe,trophySummary(@default),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),primaryOnlineStatus,presences(@titleInfo,hasBroadcastData)&sort=name-onlineId&{f}&avatarSizes=m&profilePictureSizes=m&offset={start}&limit={limit}", null, null, new { Authorization = "Bearer " + Variables.AccessToken }).AsJson();
        }
        internal Friend(PSAPI context)
        {
            _context = context;
        }
        public short Count(FriendsFilter friendsFilter = FriendsFilter.All)
        {
            return (short)_getFriends(0, 0, friendsFilter)["totalResults"];
        }
        public FriendProfile[] AllFriends(int start = 0, int limit = 20, FriendsFilter filter = FriendsFilter.All)
        {
            var friends = _getFriends(start, limit, filter);
            if (friends == null)
                return new FriendProfile[0];
            FriendProfile[] prof = new FriendProfile[limit];
            int x = 0;
            foreach (var item in friends["profiles"])
            {
                prof[x++] = new FriendProfile
                {
                    OnlineId = item["onlineId"]
                };
            }
            return prof;
        }

        public bool Delete(string onlineId)
        {
            var result = Http.Delete($"{Consts.APIEndpoints.USERS_URL}{_context.Profile.OnlineId}/friendList/{onlineId}",
                null, null, new { Authorization = "Bearer " + Variables.AccessToken });
            return !result.HasError;
        }
    }
}