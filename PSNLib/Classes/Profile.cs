using PSNLib.Models;
using TheHttp;

namespace PSNLib
{
    public class Profile : PSProfile
    {
        private readonly PSAPI _context;
        private dynamic _profile;
        private Profile() { }
        private void Info()
        {
            var response = Http.Get($"https://us-prof.np.community.playstation.net/userProfile/v1/users/me/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s", null, null, new { Authorization = "Bearer " + Variables.AccessToken }).AsJson();
            _profile = (response)["profile"];
        }
        internal Profile(PSAPI context)
        {
            _context = context;
            Info();
        }
        public override string OnlineId
        {
            get { return _profile["onlineId"].ToString(); }
        }
        public override string AvatarUrl
        {
            get { return _profile?["avatarUrls"][1]["avatarUrl"].ToString(); }
        }

        public override string AboutMe
        {
            get { return _profile?["aboutMe"]; }
        }
    }

}
