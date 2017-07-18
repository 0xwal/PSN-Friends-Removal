namespace PSNLib.Models
{
    internal struct Consts
    {
        internal struct UrlParamaters
        {
            public const string
                ClientId = "b7cbf451-6bb6-4a5a-8913-71e61f462787",
                AppContext = "inapp_ios",
                State = "06d7AuZpOmJAwYYOWmVU63OMY",
                Duid = "0000000d000400808F4B3AA3301B4945B2E3636E38C0DDFC",
                Scope = "capone:report_submission,psn:sceapp,user:account.get,user:account.settings.privacy.get,user:account.settings.privacy.update,user:account.realName.get,user:account.realName.update,kamaji:get_account_hash,kamaji:ugc:distributor,oauth:manage_device_usercodes",
                ResponseType = "code",
                ClientSecret = "zsISsjmCx85zgCJg",
                GrantType = "authorization_code",
                Authentication_Type = "password";
        }
        internal struct APIEndpoints
        {
            public const string OAUTH_URL = "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/token";
            public const string NP_GRANT_URL = "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/authorize";
            public const string SSO_COOKIE_URL = "https://auth.api.sonyentertainmentnetwork.com/2.0/ssocookie";
            public const string USERS_URL = "https://us-prof.np.community.playstation.net/userProfile/v1/users/";
        }
    }
}

