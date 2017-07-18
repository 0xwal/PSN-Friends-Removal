using System;
using PSNLib.Models;
using TheHttp;

namespace PSNLib
{
    public partial class PSAPI
    {
        private Profile _profile;
        private Friend _friend;
        public PSProfile Profile { get { return _profile ?? (_profile = new Profile(this)); } }
        public Friend Friend { get { return _friend ?? (_friend = new Friend(this)); } }
        public string RefreshToken { get { return Variables.RefreshToken; } }
        public PSAPI(string email, string password)
        {
            if (!getNpsso(email, password))
                throw new Exception("error login make sure the provided details are correct and try again!");
            if (!getNpGrantCodeRequest(_npsso))
                throw new Exception("error login serevr not found!");
            if (!oauthRequest(_npGrantCodeRequest))
                throw new Exception("error login serevr not found!");
        }
        public PSAPI(string refreshToken)
        {
            if (!oauthRequestWithRefreshToken(refreshToken))
                throw new Exception("error the refresh token is incorrect or expired!");
        }
        public PSProfile Search(string onlineId)
        {
            var res = Http.Get(Consts.APIEndpoints.USERS_URL + onlineId +
            $"/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,personalDetail,primaryOnlineStatus,blocking,mutualFriendsCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s", null, null, new
            {
                Authorization = "Bearer " + Variables.AccessToken
            });
            if (res.HasError)
                throw new Exception(res.ErrorMessage);
            var resAsJson = res.AsJson()["profile"];
            return new PSProfile() { OnlineId = resAsJson["onlineId"], AvatarUrl = resAsJson["avatarUrls"][0]["avatarUrl"], AboutMe = resAsJson["aboutMe"] };
        }

    }
}
